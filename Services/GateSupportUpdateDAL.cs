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

        public GateSupportUpdateDAL(TMSDBContext context, IConfiguration config, IMapper mapper)
        {
            _context = context;
            _configuration = config;
            _conStr = _configuration.GetConnectionString("DefaultConnection");
            _mapper = mapper;
            _stgConnectionString = _configuration.GetValue<string>("BlobConnectionString");
            _stgContainerName = _configuration.GetValue<string>("BlobContainerName");
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
            try
            {
                ICD_InBoundCheck? data = await _context.ICD_InBoundCheck.FromSqlRaw("SELECT * FROM ICD_InBoundCheck WHERE  InRegNo=@id", new SqlParameter("@id", info.InRegNo)).SingleOrDefaultAsync();
                if (data == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found!";
                }
                else
                {                  
                    data.UpdatedUser = info.UpdatedUser;
                    data.UpdatedDate = GetLocalStdDT();
                    if (info.UploadPhoto != null && info.UploadPhoto.Length > 0)
                    {
                        BlobContainerClient container = new BlobContainerClient(_stgConnectionString, _stgContainerName);
                        await container.CreateIfNotExistsAsync();

                        string fileExtension = Path.GetExtension(info.UploadPhoto.FileName);
                        string newFileName = data.TruckVehicleRegNo+"-InNo-"+info.InRegNo+ fileExtension;

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
                        process.UpdatedUser = info.UpdatedUser;
                        process.UpdatedDate = GetLocalStdDT();
                        //WeightBridgeQueue wbQ = new WeightBridgeQueue();
                        //if(process.InWeightBridgeID!=null && process.InWeightBridgeID != "None")
                        //{
                        //    wbQ.InRegNo = data.InRegNo;
                        //    wbQ.YardID = data.InYardID;
                        //    wbQ.GateID = data.InGateID;
                        //    wbQ.DriverLicenseNo = data.DriverLicenseNo;
                        //    wbQ.DriverName = data.DriverName;
                        //    wbQ.DriverContactNo = data.DriverContactNo;
                        //    wbQ.Type = "In";
                        //    wbQ.CargoType = data.InCargoType;
                        //    wbQ.CargoInfo = data.InCargoInfo;
                        //    wbQ.CardNo = data.CardNo;
                        //    wbQ.TruckVehicleRegNo = data.TruckVehicleRegNo;
                        //    wbQ.TrailerVehicleRegNo = data.TrailerVehicleRegNo;
                        //    wbQ.WeightBridgeID = data.InWeightBridgeID;
                        //    wbQ.Customer = data.Customer;
                        //    wbQ.Status = "Queue";
                        //    wbQ.CreatedDate = GetLocalStdDT();
                        //    wbQ.CreatedUser = data.InYardID;
                        //    _context.WeightBridgeQueue.Add(wbQ);
                        //}
                        //if (process.OutWeightBridgeID != null && process.OutWeightBridgeID != "None")
                        //{
                        //    wbQ.InRegNo = data.InRegNo;
                        //    wbQ.YardID = data.InYardID;
                        //    wbQ.GateID = data.InGateID;
                        //    wbQ.DriverLicenseNo = data.DriverLicenseNo;
                        //    wbQ.DriverName = data.DriverName;
                        //    wbQ.DriverContactNo = data.DriverContactNo;
                        //    wbQ.Type = "Out";
                        //    wbQ.CargoType = data.InCargoType;
                        //    wbQ.CargoInfo = data.InCargoInfo;
                        //    wbQ.CardNo = data.CardNo;
                        //    wbQ.TruckVehicleRegNo = data.TruckVehicleRegNo;
                        //    wbQ.TrailerVehicleRegNo = data.TrailerVehicleRegNo;
                        //    wbQ.WeightBridgeID = data.OutWeightBridgeID;
                        //    wbQ.Customer = data.Customer;
                        //    wbQ.Status = "Queue";
                        //    wbQ.CreatedDate = GetLocalStdDT();
                        //    wbQ.CreatedUser = data.InYardID;
                        //    _context.WeightBridgeQueue.Add(wbQ);
                        //}
                    }
                    Truck? truck = await _context.Truck.FromSqlRaw("SELECT * FROM Truck WHERE VehicleRegNo=@id", new SqlParameter("@id", data.TruckVehicleRegNo)).SingleOrDefaultAsync();
                    if (truck == null)
                    {
                        msg.Status = false;
                        msg.MessageContent = "Truck Data not found!";
                        return msg;
                    }
                    truck.LastPassedDate = GetLocalStdDT();
                    truck.UpdatedDate = GetLocalStdDT();
                    truck.UpdatedUser = data.InYardID;
                    if (!string.IsNullOrEmpty(data.TrailerVehicleRegNo))
                    {
                        Trailer? trailer = await _context.Trailer.FromSqlRaw("SELECT * FROM Trailer WHERE VehicleRegNo=@id", new SqlParameter("@id", data.TrailerVehicleRegNo)).SingleOrDefaultAsync();
                        if (trailer == null)
                        {
                            msg.Status = false;
                            msg.MessageContent = "Trailer Data not found!";
                            return msg;
                        }
                        trailer.LastPassedDate = GetLocalStdDT();
                        trailer.UpdatedDate = GetLocalStdDT();
                        trailer.UpdatedUser = data.InYardID;
                    }                   
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully saved!";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        #endregion
    }
}
