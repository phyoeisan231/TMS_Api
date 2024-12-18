using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TMS_Api.DBModels;
using TMS_Api.DTOs;

namespace TMS_Api.Services
{
    public class GateSupportUpdateDAL
    {
        private readonly TMSDBContext _context;
        private readonly IConfiguration _configuration;
        public string _conStr { get; }
        private readonly IMapper _mapper;
        private readonly string _stgConnectionString;
        private readonly string _stgContainerName;
        private readonly string _stgOutContainerName;

        public GateSupportUpdateDAL(TMSDBContext context, IConfiguration config, IMapper mapper)
        {
            _context = context;
            _configuration = config;
            _conStr = _configuration.GetConnectionString("DefaultConnection");
            _mapper = mapper;
            _stgConnectionString = _configuration.GetValue<string>("BlobConnectionString");
            _stgContainerName = _configuration.GetValue<string>("BlobContainerName");
            _stgOutContainerName = _configuration.GetValue<string>("BlobOutContainerName");
        }

        public bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }
        public DateTime GetLocalStdDT()
        {
            if (!IsLinux)
            {
                DateTime localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Myanmar Standard Time");
                return localTime;
            }
            else
            {
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Asia/Yangon");
                DateTime pacific = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);
                return pacific;
            }
        }

        public Task<DataTable> GetDataTableAsync(string sSQL, params SqlParameter[] para)
        {
            return Task.Run(() =>
            {
                using (var newCon = new SqlConnection(_conStr))
                using (var adapt = new SqlDataAdapter(sSQL, newCon))
                {
                    newCon.Open();
                    adapt.SelectCommand.CommandType = CommandType.Text;
                    if (para != null)
                        adapt.SelectCommand.Parameters.AddRange(para);

                    DataTable dt = new DataTable();
                    adapt.Fill(dt);
                    newCon.Close();
                    return dt;
                }
            });
        }

        #region Gate In Dec_3_2024
        public async Task<ResponseMessage> SaveGateIn([FromForm] ICD_TruckProcessDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ICD_InBoundCheck? data = await _context.ICD_InBoundCheck.FromSqlRaw("SELECT * FROM ICD_InBoundCheck WHERE  InRegNo=@id", new SqlParameter("@id", info.InRegNo)).SingleOrDefaultAsync();
                    if (data == null)
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data not found!";
                        return msg;
                    }
                    else
                    {
                        data.UpdatedUser = data.InYardID;
                        data.UpdatedDate = GetLocalStdDT();
                        if (info.UploadPhoto != null && info.UploadPhoto.Length > 0)
                        {
                            BlobContainerClient container = new BlobContainerClient(_stgConnectionString, _stgContainerName);
                            await container.CreateIfNotExistsAsync();

                            string fileExtension = Path.GetExtension(info.UploadPhoto.FileName);
                            string newFileName = data.TruckVehicleRegNo + "-InNo-" + info.InRegNo + fileExtension;

                            BlobClient blob = container.GetBlobClient(newFileName);
                            using (Stream stream = info.UploadPhoto.OpenReadStream())
                            {
                                await blob.UploadAsync(stream, overwrite: true);
                            }

                        }
                        ICD_TruckProcess? process = await _context.ICD_TruckProcess.FromSqlRaw("SELECT * FROM ICD_TruckProcess WHERE InRegNo=@id And InYard=0 And Status='In(Check)' ", new SqlParameter("@id", info.InRegNo)).SingleOrDefaultAsync();
                        if (process == null)
                        {
                            msg.Status = false;
                            msg.MessageContent = "Data not found!";
                            return msg;
                        }
                        else
                        {
                            process.Status = "In";
                            process.InGatePassTime = GetLocalStdDT();
                            process.UpdatedUser = data.InYardID;
                            process.UpdatedDate = GetLocalStdDT();
                            process.InYard = true;
                            int? queueNo = 1;
                            if (process.IsUseWB==true)
                            {
                                //In
                                WeightBridgeQueue wbQ = new WeightBridgeQueue();
                                WeightBridgeQueue? inwbQ = await _context.WeightBridgeQueue.FromSqlRaw("SELECT Top 1* FROM WeightBridgeQueue WHERE InRegNo=@id And WeightBridgeID=@wbId Order By QueueNo Desc", new SqlParameter("@id", info.InRegNo), new SqlParameter("@wbId", data.InWeightBridgeID)).SingleOrDefaultAsync();
                                if (inwbQ != null)
                                {
                                    queueNo += inwbQ.QueueNo;
                                }
                                wbQ.QueueNo = queueNo;
                                wbQ.InRegNo = data.InRegNo;
                                wbQ.YardID = data.InYardID;
                                wbQ.GateID = data.InGateID;
                                wbQ.DriverLicenseNo = data.DriverLicenseNo;
                                wbQ.DriverName = data.DriverName;
                                wbQ.DriverContactNo = data.DriverContactNo;
                                wbQ.Type = "In";
                                wbQ.CargoType = data.InCargoType;
                                wbQ.CargoInfo = data.InCargoInfo;
                                wbQ.CardNo = data.CardNo;
                                wbQ.TruckVehicleRegNo = data.TruckVehicleRegNo;
                                wbQ.TrailerVehicleRegNo = data.TrailerVehicleRegNo;
                                wbQ.WeightBridgeID = data.InWeightBridgeID;
                                wbQ.BillOption = data.InWBBillOption;
                                wbQ.Customer = data.Customer;
                                wbQ.Status = "Queue";
                                wbQ.CreatedDate = GetLocalStdDT();
                                wbQ.CreatedUser = data.InYardID;
                                _context.WeightBridgeQueue.Add(wbQ);
                                await _context.SaveChangesAsync();
                                //Out
                                WeightBridgeQueue outWbQ = new WeightBridgeQueue();
                                WeightBridgeQueue? wbQData = await _context.WeightBridgeQueue.FromSqlRaw("SELECT Top 1* FROM WeightBridgeQueue WHERE InRegNo=@id And WeightBridgeID=@wbId Order By QueueNo Desc", new SqlParameter("@id", info.InRegNo), new SqlParameter("@wbId", data.OutWeightBridgeID)).SingleOrDefaultAsync();
                                if (wbQData != null)
                                {
                                    queueNo += wbQData.QueueNo;
                                }
                                outWbQ.QueueNo = queueNo;
                                outWbQ.InRegNo = data.InRegNo;
                                outWbQ.YardID = data.InYardID;
                                outWbQ.GateID = data.InGateID;
                                outWbQ.DriverLicenseNo = data.DriverLicenseNo;
                                outWbQ.DriverName = data.DriverName;
                                outWbQ.DriverContactNo = data.DriverContactNo;
                                outWbQ.Type = "Out";
                                outWbQ.CargoType = data.InCargoType;
                                outWbQ.CargoInfo = data.InCargoInfo;
                                outWbQ.BillOption = data.OutWBBillOption;
                                outWbQ.CardNo = data.CardNo;
                                outWbQ.TruckVehicleRegNo = data.TruckVehicleRegNo;
                                outWbQ.TrailerVehicleRegNo = data.TrailerVehicleRegNo;
                                outWbQ.WeightBridgeID = data.OutWeightBridgeID;
                                outWbQ.Customer = data.Customer;
                                outWbQ.Status = "Queue";
                                outWbQ.CreatedDate = GetLocalStdDT();
                                outWbQ.CreatedUser = data.InYardID;
                                _context.WeightBridgeQueue.Add(outWbQ);
                                await _context.SaveChangesAsync();
                            }                                                  
                        }
                        if (data.TruckType != "RGL")
                        {
                            Truck? truck = await _context.Truck.FromSqlRaw("SELECT * FROM Truck WHERE VehicleRegNo=@id", new SqlParameter("@id", data.TruckVehicleRegNo)).SingleOrDefaultAsync();
                            if (truck == null)
                            {
                                // Rollback the transaction if any exception occurs
                                await transaction.RollbackAsync();
                                msg.Status = false;
                                msg.MessageContent = "Truck Data not found!";
                                return msg;
                            }
                            truck.LastPassedDate = GetLocalStdDT();
                            truck.UpdatedDate = GetLocalStdDT();
                            truck.UpdatedUser = data.InYardID;
                        }
                        
                        if (!string.IsNullOrEmpty(data.TrailerVehicleRegNo))
                        {
                            Trailer? trailer = await _context.Trailer.FromSqlRaw("SELECT * FROM Trailer WHERE VehicleRegNo=@id", new SqlParameter("@id", data.TrailerVehicleRegNo)).SingleOrDefaultAsync();
                            if (trailer == null)
                            {
                                // Rollback the transaction if any exception occurs
                                await transaction.RollbackAsync();
                                msg.Status = false;
                                msg.MessageContent = "Trailer Data not found!";
                                return msg;
                            }
                            trailer.LastPassedDate = GetLocalStdDT();
                            trailer.UpdatedDate = GetLocalStdDT();
                            trailer.UpdatedUser = data.InYardID;
                        }
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully saved!";
                        return msg;
                    }
                }
                catch (DbUpdateException e)
                {
                    // Rollback the transaction if any exception occurs
                    await transaction.RollbackAsync();
                    msg.MessageContent += e.Message;
                    return msg;
                }
                
            }           
        }
        #endregion

        #region Gate Out Dec_9_2024
        public async Task<ResponseMessage> SaveGateOut([FromForm] ICD_TruckProcessDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ICD_OutBoundCheck? data = await _context.ICD_OutBoundCheck.FromSqlRaw("SELECT * FROM ICD_OutBoundCheck WHERE  OutRegNo=@id", new SqlParameter("@id", info.OutRegNo)).SingleOrDefaultAsync();
                    if (data == null)
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data not found!";
                        return msg;
                    }
                    else
                    {
                        data.UpdatedUser = data.OutYardID;
                        data.UpdatedDate = GetLocalStdDT();
                        if (info.UploadPhoto != null && info.UploadPhoto.Length > 0)
                        {
                            BlobContainerClient container = new BlobContainerClient(_stgConnectionString, _stgOutContainerName);
                            await container.CreateIfNotExistsAsync();

                            string fileExtension = Path.GetExtension(info.UploadPhoto.FileName);
                            string newFileName = data.TruckVehicleRegNo + "-OutNo-" + info.OutRegNo + fileExtension;

                            BlobClient blob = container.GetBlobClient(newFileName);
                            using (Stream stream = info.UploadPhoto.OpenReadStream())
                            {
                                await blob.UploadAsync(stream, overwrite: true);
                            }
                        }
                        ICD_TruckProcess? process = await _context.ICD_TruckProcess.FromSqlRaw("SELECT * FROM ICD_TruckProcess WHERE InRegNo=@id And InYard=1 And Status='Out(Check)' ", new SqlParameter("@id", info.InRegNo)).SingleOrDefaultAsync();
                        if (process == null)
                        {
                            msg.Status = false;
                            msg.MessageContent = "Data not found!";
                            return msg;
                        }
                        else
                        {
                            process.InYard = false;
                            process.Status = "Out";
                            process.OutGatePassTime = GetLocalStdDT();
                            process.UpdatedUser = data.OutYardID;
                            process.UpdatedDate = GetLocalStdDT();
                            if (data.TruckType != "RGL")
                            {
                                Truck? truck = await _context.Truck.FromSqlRaw("SELECT * FROM Truck WHERE VehicleRegNo=@id", new SqlParameter("@id", data.TruckVehicleRegNo)).SingleOrDefaultAsync();
                                if (truck == null)
                                {
                                    // Rollback the transaction if any exception occurs
                                    await transaction.RollbackAsync();
                                    msg.Status = false;
                                    msg.MessageContent = "Truck Data not found!";
                                    return msg;
                                }
                                truck.LastPassedDate = GetLocalStdDT();
                                truck.UpdatedDate = GetLocalStdDT();
                                truck.UpdatedUser = data.OutYardID;
                            }                               

                            PCard? card = await _context.PCard.FromSqlRaw("SELECT * FROM PCard WHERE CardNo=@id", new SqlParameter("@id", data.CardNo)).SingleOrDefaultAsync();
                            if (card == null)
                            {
                                // Rollback the transaction if any exception occurs
                                await transaction.RollbackAsync();
                                msg.Status = false;
                                msg.MessageContent = "Truck Data not found!";
                                return msg;
                            }
                            card.IsUse=false;
                            card.UpdatedDate = GetLocalStdDT();
                            card.UpdatedUser = data.OutYardID;
                            if (!string.IsNullOrEmpty(data.TrailerVehicleRegNo))
                            {
                                Trailer? trailer = await _context.Trailer.FromSqlRaw("SELECT * FROM Trailer WHERE VehicleRegNo=@id", new SqlParameter("@id", data.TrailerVehicleRegNo)).SingleOrDefaultAsync();
                                if (trailer == null)
                                {
                                    // Rollback the transaction if any exception occurs
                                    await transaction.RollbackAsync();
                                    msg.Status = false;
                                    msg.MessageContent = "Trailer Data not found!";
                                    return msg;
                                }
                                trailer.LastPassedDate = GetLocalStdDT();
                                trailer.UpdatedDate = GetLocalStdDT();
                                trailer.UpdatedUser = data.OutYardID;
                            }
                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync();
                            msg.Status = true;
                            msg.MessageContent = "Successfully saved!";
                            return msg;
                        }
                    }
                }
                catch (DbUpdateException e)
                {
                    // Rollback the transaction if any exception occurs
                    await transaction.RollbackAsync();
                    msg.MessageContent += e.Message;
                    return msg;
                }

            }
        }
        #endregion
    }
}
