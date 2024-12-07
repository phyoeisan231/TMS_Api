using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using TMS_Api.DBModels;
using TMS_Api.DTOs;

namespace TMS_Api.Services
{
    public class MasterUpdateDAL
    {
        private readonly TMSDBContext _context;
        private readonly IConfiguration _configuration;
        public string _conStr { get; }
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public MasterUpdateDAL(TMSDBContext context, IConfiguration config, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = config;
            _conStr = _configuration.GetConnectionString("DefaultConnection");
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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

        #region Truck Type Nov_12_2024
        public async Task<ResponseMessage> SaveTruckType(TruckTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TruckType? data = await _context.TruckType.FromSqlRaw("SELECT TOP 1* FROM TruckType WHERE TypeID=@tID OR Description=@name Order By TypeID",
                    new SqlParameter("@tID", info.TypeID), new SqlParameter("@name", info.Description)).SingleOrDefaultAsync();
                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Truck-Type Duplicate!!";
                }
                else
                {
                    TruckType type = _mapper.Map<TruckType>(info);
                    type.CreatedDate = GetLocalStdDT();
                    
                    _context.TruckType.Add(type);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Added!";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }

            return msg;
        }
        public async Task<ResponseMessage> UpdateTruckType(TruckTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TruckType type = await _context.TruckType.FromSqlRaw("SELECT * FROM TruckType WHERE REPLACE(Description,' ','')=REPLACE(@name,' ','') ", new SqlParameter("@name", info.Description)).SingleOrDefaultAsync();
                if (type == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                }
                else
                {
                    type.Description = info.Description;
                    type.Active = info.Active;
                    type.UpdatedDate = GetLocalStdDT();
                    type.UpdatedUser = info.UpdatedUser;
                    _context.TruckType.Update(type);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Updated!";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }

            return msg;
        }
        public async Task<ResponseMessage> DeleteTruckType(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TruckType type = await _context.TruckType.FromSqlRaw("SELECT * FROM TruckType WHERE TypeID=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (type == null)
                {
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    Truck truck = await _context.Truck.FromSqlRaw("SELECT Top 1 * FROM Truck WHERE TypeID=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                    if (truck == null)
                    {
                        _context.TruckType.Remove(type);

                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Removed Successfully!";
                    }
                    else
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data Exists in Another Table!!";
                    }
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

        #region Trailer Type Nov_12_2024
        //public async Task<ResponseMessage> SaveTrailerType(TrailerTypeDto info)
        //{
        //    ResponseMessage msg = new ResponseMessage { Status = false };
        //    try
        //    {
        //        TrailerType trailerType = await _context.TrailerType.FromSqlRaw("SELECT Top 1 * FROM TrailerType WHERE REPLACE(Description,'','')=REPLACE(@name,'','')", new SqlParameter("@name", info.Description)).SingleOrDefaultAsync();
        //        if (trailerType != null)
        //        {
        //            msg.Status = false;
        //            msg.MessageContent = "Name duplicate!";
        //        }
        //        else
        //        {
        //            TrailerType type = _mapper.Map<TrailerType>(info);
        //            type.UpdatedDate = GetLocalStdDT();
        //            _context.TrailerType.Add(type);
        //            await _context.SaveChangesAsync();
        //            msg.Status = true;
        //            msg.MessageContent = "Successfully added";
        //        }
        //    }
        //    catch (DbUpdateException e)
        //    {
        //        msg.MessageContent += e.Message;
        //        return msg;
        //    }
        //    return msg;
        //}
        //public async Task<ResponseMessage> UpdateTrailerType(TrailerTypeDto info)
        //{
        //    ResponseMessage msg = new ResponseMessage { Status = false };
        //    try
        //    {
        //        TrailerType type = await _context.TrailerType.FromSqlRaw("SELECT * FROM TrailerType WHERE REPLACE(Description,' ','')=REPLACE(@name,' ','') ", new SqlParameter("@name", info.Description)).SingleOrDefaultAsync();
        //        if (type == null)
        //        {
        //            msg.Status = false;
        //            msg.MessageContent = "Data Not Found";
        //        }
        //        else
        //        {
        //            type.Description = info.Description;
        //            type.Active = info.Active;
        //            type.UpdatedDate = GetLocalStdDT();
        //            type.UpdatedUser = info.UpdatedUser;
        //            _context.TrailerType.Update(type);
        //            await _context.SaveChangesAsync();
        //            msg.Status = true;
        //            msg.MessageContent = "Successfully updated";
        //        }
        //    }
        //    catch (DbUpdateException e)
        //    {
        //        msg.MessageContent += e.Message;
        //        return msg;
        //    }
        //    return msg;
        //}
        //public async Task<ResponseMessage> DeleteTrailerType(string id)
        //{
        //    ResponseMessage msg = new ResponseMessage { Status = false };
        //    try
        //    {
        //        TrailerType type = await _context.TrailerType.FromSqlRaw("SELECT * FROM TrailerType WHERE  REPLACE(Description,' ','')=REPLACE(@id,' ','')", new SqlParameter("@id", id)).SingleOrDefaultAsync();
        //        if (type == null)
        //        {
        //            msg.MessageContent = "Data not found.";
        //            return msg;
        //        }
        //        else
        //        {
        //            Trailer trailer = await _context.Trailer.FromSqlRaw("SELECT * FROM Trailer WHERE  REPLACE(TrailerType,' ','')=REPLACE(@typeCode,' ','')", new SqlParameter("@typeCode", type.Description)).SingleOrDefaultAsync();
        //            if (trailer == null)
        //            {
        //                _context.TrailerType.Remove(type);

        //                await _context.SaveChangesAsync();
        //                msg.Status = true;
        //                msg.MessageContent = "Removed successfully!";
        //            }
        //            else
        //            {
        //                msg.Status = false;
        //                msg.MessageContent = "Data exists in another table!";
        //            }
        //        }
        //    }

        //    catch (DbUpdateException e)
        //    {
        //        msg.MessageContent += e.Message;
        //        return msg;
        //    }

        //    return msg;
        //}

        #endregion

        #region Transporter Type Nov_12_2024
        public async Task<ResponseMessage> SaveTransporterType(TransporterTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TransporterType data = await _context.TransporterType.FromSqlRaw("SELECT Top 1 * FROM TransporterType WHERE REPLACE(Description,'','')=REPLACE(@name,'','')",
                    new SqlParameter("@name", info.Description)).SingleOrDefaultAsync();
                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Name duplicate!";
                }
                else
                {
                    TransporterType type = _mapper.Map<TransporterType>(info);
                    type.UpdatedDate = GetLocalStdDT();
                    _context.TransporterType.Add(type);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully added";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> UpdateTransporterType(TransporterTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TransporterType type = await _context.TransporterType.FromSqlRaw("SELECT * FROM TransporterType WHERE REPLACE(Description,' ','')=REPLACE(@name,' ','') ", new SqlParameter("@name", info.Description)).SingleOrDefaultAsync();
                if (type == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    type.Description = info.Description;
                    type.Active = info.Active;
                    type.UpdatedDate = GetLocalStdDT();
                    type.UpdatedUser = info.UpdatedUser;
                    _context.TransporterType.Update(type);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully updated";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> DeleteTransporterType(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TransporterType type = await _context.TransporterType.FromSqlRaw("SELECT * FROM TransporterType WHERE TypeID=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (type == null)
                {
                    msg.MessageContent = "Data not found.";
                    return msg;
                }
                else
                {
                    Transporter transporter = await _context.Transporter.FromSqlRaw("SELECT Top 1* FROM Transporter WHERE  REPLACE(TypeID,' ','')=REPLACE(@id,' ','')", new SqlParameter("@id", type.TypeID)).SingleOrDefaultAsync();
                    if (transporter == null)
                    {
                        _context.TransporterType.Remove(type);

                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Removed successfully!";
                    }
                    else
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data exists in another table!";
                    }
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

        #region Transporter Nov_12_2024
        public async Task<ResponseMessage> SaveTransporter(TransporterDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Transporter duplicate = await _context.Transporter.FromSqlRaw("SELECT Top 1* FROM Transporter WHERE REPLACE(TransporterName,'','')=REPLACE(@name,'','')", new SqlParameter("@name", info.TransporterName)).SingleOrDefaultAsync();
                if (duplicate != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Transporter Name Duplicate!1";
                    return msg;
                }
                else
                {
                    Transporter transporter = await _context.Transporter.FromSqlRaw("SELECT Top 1 * FROM Transporter Order By TransporterID Desc", new SqlParameter("@name", info.TransporterID)).SingleOrDefaultAsync();
                    if (transporter != null)
                    {
                        info.SrNo = transporter.SrNo + 1;
                        info.TransporterID = "TC" + info.SrNo?.ToString("-0000");
                    }
                    else
                    {
                        info.TransporterID = "TC" + "-0001";
                        info.SrNo = 1;
                    }
                }
                Transporter data = _mapper.Map<Transporter>(info);
                data.CreatedDate = GetLocalStdDT();
                data.CreatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";

                _context.Transporter.Add(data);
                await _context.SaveChangesAsync();
                msg.Status = true;
                msg.MessageContent = "Successfully Added!";
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> UpdateTransporter(TransporterDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Transporter transporter = await _context.Transporter.SingleOrDefaultAsync(t => t.TransporterID == info.TransporterID);

                if (transporter == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    transporter.TransporterID = info.TransporterID;
                    transporter.TransporterName = info.TransporterName;
                    transporter.Address = info.Address;
                    transporter.ContactNo = info.ContactNo;
                    transporter.Email = info.Email;
                    transporter.Remarks = info.Remarks;
                    transporter.Active = info.Active;
                    transporter.ContactPerson = info.ContactPerson;
                    transporter.TypeID = info.TypeID;
                    transporter.SAPID = info.SAPID;
                    transporter.IsBlack = info.IsBlack;
                    
                    transporter.UpdatedDate = GetLocalStdDT();
                    transporter.UpdatedUser = info.UpdatedUser;
                    transporter.UpdatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";

                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Successfully Updated!";
                    msg.Status = true; // Set status to true for a successful operation
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
        }
        public async Task<ResponseMessage> BlackFormForTransporter([FromBody] TransporterDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Transporter transporter = await _context.Transporter.SingleOrDefaultAsync(t => t.TransporterID == info.TransporterID);

                if (transporter == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    transporter.IsBlack = info.IsBlack;
                    if (transporter.IsBlack == true)
                    {
                        transporter.BlackDate = info.BlackDate;
                        transporter.BlackReason = info.BlackReason;
                    }
                    else
                    {
                        transporter.BlackRemovedDate = info.BlackRemovedDate;
                        transporter.BlackRemovedReason = info.BlackRemovedReason;
                    }
                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Success!";
                    msg.Status = true;
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
        }
        public async Task<ResponseMessage> DeleteTransporter(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Transporter? transporter = await _context.Transporter.FromSqlRaw("SELECT * FROM Transporter Where TransporterID=@tID", new SqlParameter("@tID", id)).SingleOrDefaultAsync();
                if (transporter == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                }
                else
                {
                    ICD_InBoundCheck? ibCheck = await _context.ICD_InBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_InBoundCheck WHERE TransporterID=@tID", new SqlParameter("@tID", id)).SingleOrDefaultAsync();
                    ICD_OutBoundCheck? obCheck = await _context.ICD_OutBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_OutBoundCheck WHERE TransporterID=@tID", new SqlParameter("@tID", id)).SingleOrDefaultAsync();
                    ICD_TruckProcess? tProcess = await _context.ICD_TruckProcess.FromSqlRaw("SELECT TOP 1* FROM ICD_TruckProcess WHERE TransporterID=@tID", new SqlParameter("@tID", id)).SingleOrDefaultAsync();
                    Truck? truck = await _context.Truck.FromSqlRaw("SELECT TOP 1* FROM Truck WHERE TransporterID=@tID", new SqlParameter("@tID", id)).SingleOrDefaultAsync();
                    Trailer? trailer = await _context.Trailer.FromSqlRaw("SELECT TOP 1* FROM Trailer WHERE TransporterID=@tID", new SqlParameter("@tID", id)).SingleOrDefaultAsync();

                    if(ibCheck==null && obCheck==null && tProcess==null && truck==null && trailer == null)
                    {
                        _context.Transporter.Remove(transporter);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully Removed!";
                    }
                    else
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data Exists in Another Table!!";
                    }
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

        #region Gate Nov_12_2024
        public async Task<ResponseMessage> SaveGate(GateDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Gate? data = await _context.Gate.FromSqlRaw("SELECT Top 1 * FROM Gate WHERE GateID=@gID OR Name=@name Order By GateID",
                    new SqlParameter("@gID",info.GateID),new SqlParameter("@name", info.Name)).SingleOrDefaultAsync();
                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Duplicate!1";
                    return msg;
                }
                else
                {
                    Gate gate = _mapper.Map<Gate>(info);
                    gate.UpdatedDate = GetLocalStdDT();
                    _context.Gate.Add(gate);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Added!";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> UpdateGate(GateDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Gate gate = await _context.Gate.FromSqlRaw("SELECT * FROM Gate WHERE REPLACE(GateID,' ','')=REPLACE(@gID,' ','') ", new SqlParameter("@gID", info.GateID)).SingleOrDefaultAsync();
                if (gate == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                }
                else
                {
                    gate.GateID = info.GateID;
                    gate.Name = info.Name;
                    gate.YardID = info.YardID;
                    gate.Type = info.Type;
                    gate.Active = info.Active;
                    gate.UpdatedDate = GetLocalStdDT();
                    gate.UpdatedUser = info.UpdatedUser;
                    _context.Gate.Update(gate);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Updated!";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> DeleteGate(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Gate gate = await _context.Gate.FromSqlRaw("SELECT * FROM Gate WHERE  REPLACE(GateID,'','')=REPLACE(@id,'','')", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (gate == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                }
                else
                {
                    ICD_InBoundCheck? ibCheck = await _context.ICD_InBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_InBoundCheck WHERE InGateID=@gateID", new SqlParameter("@gateID", id)).SingleOrDefaultAsync();
                    ICD_OutBoundCheck? obCheck = await _context.ICD_OutBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_OutBoundCheck WHERE OutGateID=@gateID", new SqlParameter("@gateID", id)).SingleOrDefaultAsync();
                    ICD_TruckProcess? tProcess = await _context.ICD_TruckProcess.FromSqlRaw("SELECT TOP 1* FROM ICD_TruckProcess WHERE InGateID=@gateID", new SqlParameter("@gateID", id)).SingleOrDefaultAsync();
                    WeightBridgeQueue? wbQueue = await _context.WeightBridgeQueue.FromSqlRaw("SELECT TOP 1* FROM WeightBridgeQueue WHERE GateID=@gateID", new SqlParameter("@gateID", id)).SingleOrDefaultAsync();

                    if (ibCheck==null && obCheck==null && tProcess == null && wbQueue==null)
                    {
                        _context.Gate.Remove(gate);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully Removed!";
                    }
                    else
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data Exists in Another Table!!";
                    }
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

        #region Truck Nov_12_2024
        public async Task<ResponseMessage> SaveTruck(TruckDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Truck truck = await _context.Truck.FromSqlRaw("SELECT Top 1 * FROM Truck WHERE REPLACE(VehicleRegNo,' ','')=REPLACE(@name,' ','')", new SqlParameter("@name", info.VehicleRegNo)).SingleOrDefaultAsync();
                if (truck != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!1";
                    return msg;
                }
                else
                {
                    Truck data = _mapper.Map<Truck>(info);
                    data.CreatedDate = GetLocalStdDT();
                    data.CreatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";
                    _context.Truck.Add(data);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Added!";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> UpdateTruck(TruckDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Truck truck = await _context.Truck.SingleOrDefaultAsync(t => t.VehicleRegNo == info.VehicleRegNo);

                if (truck == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    truck.VehicleRegNo = info.VehicleRegNo;
                    truck.TypeID = info.TypeID;
                    truck.TransporterID = info.TransporterID;
                    truck.Remarks = info.Remarks;
                    truck.Active = info.Active;
                    truck.IsRGL= info.IsRGL;
                    truck.IsBlack = info.IsBlack;
                    truck.VehicleBackRegNo = info.VehicleBackRegNo;
                    truck.TruckWeight = info.TruckWeight;
                    truck.DriverLicenseNo = info.DriverLicenseNo;
                    truck.LastPassedDate = info.LastPassedDate;
                    truck.ContainerType = info.ContainerType;
                    truck.ContainerSize = info.ContainerSize;
                    truck.UpdatedDate = GetLocalStdDT();
                    truck.UpdatedUser=info.UpdatedUser;
                    truck.UpdatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";

                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Successfully Updated!";
                    msg.Status = true; 
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
        }
        public async Task<ResponseMessage> BlackFormForTruck([FromBody] TruckDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Truck truck = await _context.Truck.SingleOrDefaultAsync(t => t.VehicleRegNo == info.VehicleRegNo);

                if (truck == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    truck.IsBlack = info.IsBlack;
                    if (truck.IsBlack == true)
                    {
                        truck.BlackDate = info.BlackDate;
                        truck.BlackReason = info.BlackReason;
                    }
                    else
                    {
                        truck.BlackRemovedDate = info.BlackRemovedDate;
                        truck.BlackRemovedReason = info.BlackRemovedReason;
                    }
                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Success!";
                    msg.Status = true;
                    return msg;

                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
        }
        public async Task<ResponseMessage> DeleteTruck(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Truck truck = await _context.Truck.FromSqlRaw("SELECT * FROM Truck Where VehicleRegNo=@regNo", new SqlParameter("@regNo", id)).SingleOrDefaultAsync();
                if (truck == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                }
                else
                {
                    ICD_InBoundCheck? ibCheck = await _context.ICD_InBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_InBoundCheck WHERE TruckVehicleRegNo=@vegNo", new SqlParameter("@vegNo", id)).SingleOrDefaultAsync();
                    ICD_OutBoundCheck? obCheck = await _context.ICD_OutBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_OutBoundCheck WHERE TruckVehicleRegNo=@vegNo", new SqlParameter("@vegNo", id)).SingleOrDefaultAsync();
                    ICD_TruckProcess? tProcess = await _context.ICD_TruckProcess.FromSqlRaw("SELECT TOP 1* FROM ICD_TruckProcess WHERE TruckVehicleRegNo=@vegNo", new SqlParameter("@vegNo", id)).SingleOrDefaultAsync();
                    PCard? pCard = await _context.PCard.FromSqlRaw("SELECT TOP 1* FROM PCard WHERE VehicleRegNo=@vegNo", new SqlParameter("@vegNo", id)).SingleOrDefaultAsync();
                    WeightBridgeQueue? wbQueue= await _context.WeightBridgeQueue.FromSqlRaw("SELECT TOP 1* FROM WeightBridgeQueue WHERE TruckVehicleRegNo=@vegNo", new SqlParameter("@vegNo", id)).SingleOrDefaultAsync();

                    if(ibCheck==null && obCheck==null && tProcess==null && pCard == null && wbQueue==null)
                    {
                        _context.Truck.Remove(truck);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully Removed!";
                    }
                    else
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data Exists in Another Table!!";
                    }
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

        #region Trailer Nov_12_2024
        public async Task<ResponseMessage> SaveTrailer(TrailerDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Trailer trailer = await _context.Trailer.FromSqlRaw("SELECT Top 1 * FROM Trailer WHERE REPLACE(VehicleRegNo,' ','')=REPLACE(@name,' ','')", new SqlParameter("@name", info.VehicleRegNo)).SingleOrDefaultAsync();
                if (trailer != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    Trailer data = _mapper.Map<Trailer>(info);
                    data.CreatedDate = GetLocalStdDT();
                    data.CreatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";

                    _context.Trailer.Add(data);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Added!";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> UpdateTrailer(TrailerDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Trailer trailer = await _context.Trailer.SingleOrDefaultAsync(t => t.VehicleRegNo == info.VehicleRegNo);

                if (trailer == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    trailer.VehicleRegNo = info.VehicleRegNo;
                    trailer.ContainerType = info.ContainerType;
                    trailer.ContainerSize = info.ContainerSize;
                    trailer.TrailerWeight = info.TrailerWeight;
                    trailer.TransporterID = info.TransporterID;
                    trailer.Remarks = info.Remarks;
                    trailer.Active = info.Active;
                    trailer.IsBlack = info.IsBlack;
                    trailer.VehicleBackRegNo = info.VehicleBackRegNo;
                    trailer.DriverLicenseNo = info.DriverLicenseNo;
                   
                    trailer.LastPassedDate = info.LastPassedDate;
                    trailer.UpdatedDate = GetLocalStdDT();
                    trailer.UpdatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";

                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Successfully Updated!";
                    msg.Status = true; // Set status to true for a successful operation
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
        }
        public async Task<ResponseMessage> BlackFormForTrailer([FromBody] TrailerDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Trailer trailer = await _context.Trailer.SingleOrDefaultAsync(t => t.VehicleRegNo == info.VehicleRegNo);

                if (trailer == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    trailer.IsBlack = info.IsBlack;
                    if (trailer.IsBlack == true)
                    {
                        trailer.BlackDate = info.BlackDate;
                        trailer.BlackReason = info.BlackReason;
                    }
                    else
                    {
                        trailer.BlackRemovedDate = info.BlackRemovedDate;
                        trailer.BlackRemovedReason = info.BlackRemovedReason;
                    }
                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Success!";
                    msg.Status = true;
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
        }
        public async Task<ResponseMessage> DeleteTrailer(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Trailer trailer = await _context.Trailer.FromSqlRaw("SELECT * FROM Trailer Where VehicleRegNo=@regNo", new SqlParameter("@regNo", id)).SingleOrDefaultAsync();
                if (trailer == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!";
                }
                else
                {
                    ICD_InBoundCheck? ibCheck = await _context.ICD_InBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_InBoundCheck WHERE TrailerVehicleRegNo=@vegNo", new SqlParameter("@vegNo", id)).SingleOrDefaultAsync();
                    ICD_OutBoundCheck? obCheck = await _context.ICD_OutBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_OutBoundCheck WHERE TrailerVehicleRegNo=@vegNo", new SqlParameter("@vegNo", id)).SingleOrDefaultAsync();
                    ICD_TruckProcess? tProcess = await _context.ICD_TruckProcess.FromSqlRaw("SELECT TOP 1* FROM ICD_TruckProcess WHERE TrailerVehicleRegNo=@vegNo", new SqlParameter("@vegNo", id)).SingleOrDefaultAsync();
                    WeightBridgeQueue? wbQueue = await _context.WeightBridgeQueue.FromSqlRaw("SELECT TOP 1* FROM WeightBridgeQueue WHERE TrailerVehicleRegNo=@vegNo", new SqlParameter("@vegNo", id)).SingleOrDefaultAsync();
                    if(ibCheck==null && obCheck==null && tProcess==null && wbQueue == null)
                    {
                        _context.Trailer.Remove(trailer);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully Removed!";
                    }
                    else
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data Exitsts in Another Table!";
                    }
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

        #region Driver Nov_12_2024
        public async Task<ResponseMessage> SaveDriver(DriverDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Driver driver = await _context.Driver.FromSqlRaw("SELECT Top 1 * FROM Driver WHERE REPLACE(LicenseNo,' ','')=REPLACE(@name,' ','')", new SqlParameter("@name", info.LicenseNo)).SingleOrDefaultAsync();
                if (driver != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    Driver data = _mapper.Map<Driver>(info);
                    data.CreatedDate = GetLocalStdDT();
                    data.CreatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";

                    _context.Driver.Add(data);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Added!";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> UpdateDriver(DriverDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Driver driver = await _context.Driver.SingleOrDefaultAsync(tt => tt.LicenseNo == info.LicenseNo);
                if (driver == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    driver.LicenseNo = info.LicenseNo;
                    driver.NRC = info.NRC;
                    driver.Name = info.Name;
                    driver.Address = info.Address;
                    driver.Active = info.Active;
                    driver.LicenseClass = info.LicenseClass;
                    driver.LicenseExpiration = info.LicenseExpiration;
                    driver.ContactNo = info.ContactNo;
                    driver.Email = info.Email;
                    driver.Remarks = info.Remarks;
                    driver.IsBlack = info.IsBlack;
                    
                    driver.UpdatedDate = GetLocalStdDT();
                    driver.UpdatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";

                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Successfully Updated!";
                    msg.Status = true;
                    return msg;

                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;

            }
        }
        public async Task<ResponseMessage> BlackFormForDriver([FromBody] DriverDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Driver driver = await _context.Driver.SingleOrDefaultAsync(t => t.LicenseNo== info.LicenseNo);

                if (driver == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    driver.IsBlack = info.IsBlack;
                    if (driver.IsBlack == true)
                    {
                        driver.BlackDate = info.BlackDate;
                        driver.BlackReason = info.BlackReason;
                    }
                    else
                    {
                        driver.BlackRemovedDate = info.BlackRemovedDate;
                        driver.BlackRemovedReason = info.BlackRemovedReason;
                    }
                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Success!";
                    msg.Status = true;
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
        }
        public async Task<ResponseMessage> DeleteDriver(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                // Decode the ID
                string decodedId = Uri.UnescapeDataString(id);
                Console.WriteLine($"Received ID: {id}, Decoded ID: {decodedId}");

                Driver? driver = await _context.Driver.FromSqlRaw("SELECT * FROM Driver WHERE LicenseNo = @id", new SqlParameter("@id", decodedId)).SingleOrDefaultAsync();

                if (driver == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                }
                else
                {
                    ICD_InBoundCheck? ibCheck = await _context.ICD_InBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_InBoundCheck WHERE DriverLicenseNo=@licenseNo", new SqlParameter("@licenseNo", decodedId)).SingleOrDefaultAsync();
                    ICD_OutBoundCheck? obCheck = await _context.ICD_OutBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_OutBoundCheck WHERE DriverLicenseNo=@licenseNo", new SqlParameter("@licenseNo", decodedId)).SingleOrDefaultAsync();
                    ICD_TruckProcess? tProcess = await _context.ICD_TruckProcess.FromSqlRaw("SELECT TOP 1* FROM ICD_TruckProcess WHERE DriverLicenseNo=@licenseNo", new SqlParameter("@licenseNo", decodedId)).SingleOrDefaultAsync();
                    Truck? truck = await _context.Truck.FromSqlRaw("SELECT TOP 1* FROM Truck WHERE DriverLicenseNo=@licenseNo", new SqlParameter("@licenseNo", decodedId)).SingleOrDefaultAsync();
                    Trailer? trailer = await _context.Trailer.FromSqlRaw("SELECT TOP 1* FROM Trailer WHERE DriverLicenseNo=@licenseNo", new SqlParameter("@licenseNo", decodedId)).SingleOrDefaultAsync();

                    if(ibCheck==null && obCheck==null && tProcess==null && truck==null && trailer == null)
                    {
                        _context.Driver.Remove(driver);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully Removed!";
                    }
                    else
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data Exists in Another Table!!";
                    }
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
            }
            return msg;
        }

        #endregion

        #region Yard Nov_26_2024

        //public async Task<ResponseMessage> SaveYard(YardDto info)
        //{
        //    ResponseMessage msg = new ResponseMessage { Status = false };
        //    try
        //    {

        //        Yard data = await _context.Yard.FromSqlRaw("SELECT Top 1 * FROM Yard WHERE REPLACE(Name,'','')=REPLACE(@name,'','')", new SqlParameter("@name", info.Name)).SingleOrDefaultAsync();
        //        if (data != null)
        //        {
        //            msg.Status = false;
        //            msg.MessageContent = "Name Duplicate!";
        //        }
        //        else
        //        {
        //            Yard yardIDTest = await _context.Yard.FromSqlRaw("SELECT Top 1* FROM Yard WHERE REPLACE(YardID,'','')=REPLACE(@yID,'','')", new SqlParameter("@yID", info.YardID)).SingleOrDefaultAsync();
        //            if (yardIDTest != null)
        //            {
        //                msg.Status = false;
        //                msg.MessageContent = "YardID Duplicate!";//For User Input ID Testing
        //            }
        //            else
        //            {
        //                Yard yard = _mapper.Map<Yard>(info);
        //                yard.CreatedDate = GetLocalStdDT();
        //                _context.Yard.Add(yard);
        //                await _context.SaveChangesAsync();
        //                msg.Status = true;
        //                msg.MessageContent = "Successfully Added";
        //            }
        //        }

        //    }
        //    catch (DbUpdateException e)
        //    {
        //        msg.MessageContent += e.Message;
        //        return msg;
        //    }
        //    return msg;
        //}
        public async Task<ResponseMessage> SaveYard(YardDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {

                Yard data = await _context.Yard.FromSqlRaw("SELECT TOP 1* FROM Yard WHERE YardID=@yId OR Name=@name ORDER By YardID", new SqlParameter("@yId", info.YardID), new SqlParameter("@name", info.Name)).SingleOrDefaultAsync();
                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Duplicate!!";
                    return msg;
                }
                else
                {
                    Yard yard = _mapper.Map<Yard>(info);
                    yard.CreatedDate = GetLocalStdDT();
                    _context.Yard.Add(yard);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Added!";
                }
            }
            catch(DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> UpdateYard(YardDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Yard yard = await _context.Yard.FromSqlRaw("SELECT * FROM Yard WHERE REPLACE(YardID,' ','')=REPLACE(@yID,' ','') ", new SqlParameter("@yID", info.YardID)).SingleOrDefaultAsync();
                if (yard == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                }
                else
                {
                    yard.Name = info.Name;
                    yard.YardID = info.YardID;
                    yard.Active = info.Active;
                    yard.UpdatedDate = GetLocalStdDT();
                    yard.UpdatedUser = info.UpdatedUser;
                    _context.Yard.Update(yard);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Updated!";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> DeleteYard(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Yard? data = await _context.Yard.FromSqlRaw("SELECT Top 1* FROM Yard WHERE REPLACE(YardID,'','')=REPLACE(@yID,'','')", new SqlParameter("@yID", id)).SingleOrDefaultAsync();
                if (data == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                }
                else
                {
                    //Gate? gate = await _context.Gate.FromSqlRaw("SELECT Top 1* FROM Gate WHERE REPLACE(YardID,'','')=REPLACE(@yID,'','')", new SqlParameter("@yID", id)).SingleOrDefaultAsync();
                    Gate? gate = await _context.Gate.FromSqlRaw("SELECT TOP 1* FROM Gate WHERE YardID=@yID", new SqlParameter("@yID", id)).SingleOrDefaultAsync();
                    WeightBridge? weightBridge = await _context.WeightBridge.FromSqlRaw("SELECT TOP 1* FROM WeightBridge WHERE YardID=@yID", new SqlParameter("@yID", id)).SingleOrDefaultAsync();
                    OperationArea? opArea = await _context.OperationArea.FromSqlRaw("SELECT TOP 1* FROM OperationArea WHERE YardID=@yID", new SqlParameter("@yID", id)).SingleOrDefaultAsync();
                    PCard? pCard = await _context.PCard.FromSqlRaw("SELECT TOP 1* FROM PCard WHERE YardID=@yID", new SqlParameter("@yID", id)).SingleOrDefaultAsync();
                    ICD_InBoundCheck? ibCheck = await _context.ICD_InBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_InBoundCheck WHERE InYardID=@yID", new SqlParameter("@yID", id)).SingleOrDefaultAsync();
                    ICD_OutBoundCheck? obCheck = await _context.ICD_OutBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_OutBoundCheck WHERE OutYardID=@yID", new SqlParameter("@yID", id)).SingleOrDefaultAsync();
                    ICD_TruckProcess? tProcess = await _context.ICD_TruckProcess.FromSqlRaw("SELECT TOP 1* FROM ICD_TruckProcess WHERE InYardID=@yID", new SqlParameter("@yID", id)).SingleOrDefaultAsync();
                    WeightBridgeQueue? wbQueue = await _context.WeightBridgeQueue.FromSqlRaw("SELECT TOP 1* FROM WeightBridgeQueue WHERE YardID=@yID", new SqlParameter("@yID", id)).SingleOrDefaultAsync();


                    if (gate == null && weightBridge==null && opArea==null && pCard==null && ibCheck==null && obCheck==null && tProcess==null && wbQueue==null)
                    {
                        _context.Yard.Remove(data);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully Removed!";
                    }
                    else
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data exists in Another Table!!";
                    }
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

        #region WeightBridge Nov_22_2024
        public async Task<ResponseMessage> SaveWeightBridge(WeightBridgeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                WeightBridge? weightBridge = await _context.WeightBridge.FromSqlRaw("SELECT Top 1 * FROM WeightBridge WHERE REPLACE(Name,' ','')=REPLACE(@name,' ','') OR REPLACE(WeightBridgeID,'','')=REPLACE(@wbID,'','')"
                    ,new SqlParameter("@wbID",info.WeightBridgeID), new SqlParameter("@name", info.Name)).SingleOrDefaultAsync();
                if (weightBridge != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Duplicate!!";
                    return msg;
                }
                else
                {
                    WeightBridge data = _mapper.Map<WeightBridge>(info);
                    data.CreatedDate = GetLocalStdDT();
                    _context.WeightBridge.Add(data);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Added!";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> UpdateWeightBridge(WeightBridgeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                WeightBridge weightBridge = await _context.WeightBridge.SingleOrDefaultAsync(tt => tt.WeightBridgeID == info.WeightBridgeID);
                if (weightBridge == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    weightBridge.WeightBridgeID = info.WeightBridgeID;
                    weightBridge.Name = info.Name;
                    weightBridge.YardID = info.YardID;
                    weightBridge.Active = info.Active;
                    weightBridge.UpdatedDate = GetLocalStdDT();
                    weightBridge.UpdatedUser = info.UpdatedUser;
                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Successfully Updated!";
                    msg.Status = true;
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
        }
        public async Task<ResponseMessage> DeleteWeightBridge(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                WeightBridge weightBridge = await _context.WeightBridge.FromSqlRaw("SELECT * FROM WeightBridge Where WeightBridgeID=@wID", new SqlParameter("@wID", id)).SingleOrDefaultAsync();
                if (weightBridge == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                }
                else
                {
                    WeightBridgeQueue? wbQueue = await _context.WeightBridgeQueue.FromSqlRaw("SELECT TOP 1* FROM WeightBridgeQueue WHERE WeightBridgeID=@wbID", new SqlParameter("@wbID", id)).SingleOrDefaultAsync();
                    ICD_InBoundCheck? ibCheck = await _context.ICD_InBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_InBoundCheck WHERE InWeightBridgeID=@wbID", new SqlParameter("@wbID", id)).SingleOrDefaultAsync();
                    ICD_OutBoundCheck? obCheck = await _context.ICD_OutBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_OutBoundCheck WHERE OutWeightBridgeID=@wbID", new SqlParameter("@wbID", id)).SingleOrDefaultAsync();
                    ICD_TruckProcess? tProcess = await _context.ICD_TruckProcess.FromSqlRaw("SELECT TOP 1* FROM ICD_TruckProcess WHERE InWeightBridgeID=@wbID", new SqlParameter("@wbID", id)).SingleOrDefaultAsync();

                    if (wbQueue == null && ibCheck==null && obCheck==null && tProcess==null)
                    {
                        _context.WeightBridge.Remove(weightBridge);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully Removed!";
                    }
                    else
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data Exitsts in Another Table!!";
                    }
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

        #region TruckEntryType Nov_22_2024
        public async Task<ResponseMessage> SaveTruckEntryType(TruckEntryTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {

                TruckEntryType data = await _context.TruckEntryType.FromSqlRaw("SELECT Top 1 * FROM TruckEntryType WHERE REPLACE(Description,'','')=REPLACE(@name,'','')", new SqlParameter("@name", info.Description)).SingleOrDefaultAsync();
                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Name Duplicate!";
                }
                else
                {
                    TruckEntryType tIDTest = await _context.TruckEntryType.FromSqlRaw("SELECT Top 1* FROM TruckEntryType WHERE REPLACE(TypeID,'','')=REPLACE(@tID,'','')", new SqlParameter("@tID", info.TypeID)).SingleOrDefaultAsync();
                    if (tIDTest != null)
                    {
                        msg.Status = false;
                        msg.MessageContent = "ID Duplicate!";//For User Input ID Testing
                    }
                    else
                    {
                        TruckEntryType truck = _mapper.Map<TruckEntryType>(info);
                        truck.CreatedDate = GetLocalStdDT();
                        _context.TruckEntryType.Add(truck);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully Added!";
                    }
                }

            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> UpdateTruckEntryType(TruckEntryTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TruckEntryType type = await _context.TruckEntryType.FromSqlRaw("SELECT * FROM TruckEntryType WHERE REPLACE(TypeID,' ','')=REPLACE(@name,' ','') ", new SqlParameter("@name", info.TypeID)).SingleOrDefaultAsync();
                if (type == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!";
                }
                else
                {
                    type.Description = info.Description;
                    type.Active = info.Active;
                    type.UpdatedDate = GetLocalStdDT();
                    type.UpdatedUser = info.UpdatedUser;
                    _context.TruckEntryType.Update(type);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Updated!";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> DeleteTruckEntryType(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TruckEntryType type = await _context.TruckEntryType.FromSqlRaw("SELECT * FROM TruckEntryType WHERE  REPLACE(TypeID,'','')=REPLACE(@id,'','')", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (type == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!";
                }
                else
                {
                    _context.TruckEntryType.Remove(type);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Removed!";
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }

        #endregion//Process

        #region TruckJobType Nov_26_2024
        public async Task<ResponseMessage> SaveTruckJobType(TruckJobTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {

                TruckJobType data = await _context.TruckJobType.FromSqlRaw("SELECT Top 1 * FROM TruckJobType WHERE REPLACE(Description,'','')=REPLACE(@name,'','')", new SqlParameter("@name", info.Description)).SingleOrDefaultAsync();
                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Name Duplicate!";
                }
                else
                {
                    TruckJobType tIDTest = await _context.TruckJobType.FromSqlRaw("SELECT Top 1* FROM TruckJobType WHERE REPLACE(TypeID,'','')=REPLACE(@tID,'','')", new SqlParameter("@tID", info.TypeID)).SingleOrDefaultAsync();
                    if (tIDTest != null)
                    {
                        msg.Status = false;
                        msg.MessageContent = "ID Duplicate!";//For User Input ID Testing
                    }
                    else
                    {
                        TruckJobType truck = _mapper.Map<TruckJobType>(info);
                        truck.CreatedDate = GetLocalStdDT();
                        _context.TruckJobType.Add(truck);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully Added!";
                    }
                }

            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> UpdateTruckJobType(TruckJobTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TruckJobType type = await _context.TruckJobType.FromSqlRaw("SELECT * FROM TruckJobType WHERE REPLACE(TypeID,' ','')=REPLACE(@tID,' ','') ", new SqlParameter("@tID", info.TypeID)).SingleOrDefaultAsync();
                if (type == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!";
                }
                else
                {
                    type.TypeID = info.TypeID;
                    type.Description = info.Description;
                    type.Active = info.Active;
                    type.UpdatedDate = GetLocalStdDT();
                    type.UpdatedUser = info.UpdatedUser;
                    _context.TruckJobType.Update(type);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Updated!";
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> DeleteTruckJobType(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TruckJobType type = await _context.TruckJobType.FromSqlRaw("SELECT * FROM TruckJobType WHERE  REPLACE(TypeID,'','')=REPLACE(@id,'','')", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (type == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!";
                }
                else
                {
                    _context.TruckJobType.Remove(type);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Removed!";
                    return msg;
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

        #region WaitingArea Nov_27_2024
        public async Task<ResponseMessage> SaveWaitingArea(WaitingAreaDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                bool nameExists = await _context.WaitingArea.AnyAsync(wa => wa.Name == info.Name && wa.YardID == info.YardID);

                if (nameExists)
                {
                    msg.MessageContent = "A Waiting Area with this Name already exists for this YardID.";
                    return msg;
                }
                WaitingArea waitingArea = await _context.WaitingArea.FromSqlRaw("SELECT Top 1* FROM WaitingArea WHERE YardID=@yid Order By AreaID DESC", new SqlParameter("@yid", info.YardID)).SingleOrDefaultAsync();
                int srNo = 1;
                string id = string.IsNullOrEmpty(info.YardID) ? "" : info.YardID;
                if (waitingArea != null)
                {
                    string srNoPart = waitingArea.AreaID.Substring(info.YardID.Length + 4);//Pool is 4
                    if(int.TryParse(srNoPart,out srNo)){
                        srNo++;
                    }
                }
                string newAreaID = $"{id}Pool{srNo}";
                info.AreaID = newAreaID;
                WaitingArea data = _mapper.Map<WaitingArea>(info);
                data.CreatedDate = GetLocalStdDT();
                _context.WaitingArea.Add(data);
                await _context.SaveChangesAsync();
                msg.Status = true;
                msg.MessageContent = "Successfully added";

            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> UpdateWaitingArea(WaitingAreaDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                WaitingArea waitingArea = await _context.WaitingArea.SingleOrDefaultAsync(wa => wa.AreaID == info.AreaID);
                if (waitingArea == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                    return msg;
                }
                else
                {
                    waitingArea.AreaID = info.AreaID;
                    waitingArea.Name = info.Name;
                    waitingArea.YardID = info.YardID;
                    waitingArea.Active = info.Active;
                    waitingArea.UpdatedUser = info.UpdatedUser;
                    waitingArea.UpdatedDate = GetLocalStdDT();
                    await _context.SaveChangesAsync();
                    msg.Status = true;//optional
                    msg.MessageContent = "Successfully Updated";
                    return msg;
                }
            }
            catch(DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
        }
        public async Task<ResponseMessage> DeleteWaitingArea(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                WaitingArea waitingArea = await _context.WaitingArea.FromSqlRaw("SELECT * FROM WaitingArea Where AreaID=@aID", new SqlParameter("@aID", id)).SingleOrDefaultAsync();
                if (waitingArea == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    _context.WaitingArea.Remove(waitingArea);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Removed";
                    return msg;
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

        #region PCategory Nov_27_2024
        public async Task<ResponseMessage> SavePCategory(PCategoryDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                // Check if either CategoryName or PCCode is duplicated
                PCategory? duplicateCheck = await _context.PCategory
                    .FromSqlRaw("SELECT TOP 1 * FROM PCategory WHERE REPLACE(CategoryName, ' ', '') = REPLACE(@name, ' ', '') OR REPLACE(PCCode, '', '') = REPLACE(@pCode, '', '')",
                     new SqlParameter("@name", info.CategoryName),new SqlParameter("@pCode", info.PCCode)).SingleOrDefaultAsync();

                if (duplicateCheck != null)
                {
                    if (duplicateCheck.CategoryName == info.CategoryName)
                    {
                        msg.MessageContent = "Name Duplicate!!";
                    }
                    else if (duplicateCheck.PCCode == info.PCCode)
                    {
                        msg.MessageContent = "ID Duplicate!!";
                    }

                    msg.Status = false;
                    return msg;
                }

                PCategory newCategory = _mapper.Map<PCategory>(info);
                newCategory.CreatedDate = GetLocalStdDT();

                _context.PCategory.Add(newCategory);
                await _context.SaveChangesAsync();

                msg.Status = true;
                msg.MessageContent = "Successfully Added!";
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }

            return msg;
        }
        public async Task<ResponseMessage> UpdatePCategory(PCategoryDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                PCategory? pCategory = await _context.PCategory.SingleOrDefaultAsync(tt => tt.PCCode == info.PCCode);
                if (pCategory == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    pCategory.PCCode = info.PCCode;
                    pCategory.CategoryName = info.CategoryName;
                    pCategory.InboundWeight = info.InboundWeight;
                    pCategory.OutboundWeight = info.OutboundWeight;
                    pCategory.Active = info.Active;
                    pCategory.GroupName = info.GroupName;
                    pCategory.UpdatedDate = GetLocalStdDT();
                    pCategory.UpdatedUser = info.UpdatedUser;
                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Successfully Updated!";
                    msg.Status = true;
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
        }
        public async Task<ResponseMessage> DeletePCategory(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                PCategory? pCategory = await _context.PCategory.FromSqlRaw("SELECT * FROM PCategory Where PCCode=@pCode", new SqlParameter("@pCode", id)).SingleOrDefaultAsync();
                if (pCategory == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                }
                else
                {
                    DocumentSetting? docSetting = await _context.DocumentSetting.FromSqlRaw("SELECT TOP 1* FROM DocumentSetting WHERE PCCode=@pcCode", new SqlParameter("@pcCode", id)).SingleOrDefaultAsync();
                    ICD_InBoundCheck? ibCheck = await _context.ICD_InBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_InBoundCheck WHERE InPCCode=@pcCode", new SqlParameter("@pcCode", id)).SingleOrDefaultAsync();
                    ICD_OutBoundCheck? obCheck = await _context.ICD_OutBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_OutBoundCheck WHERE OutPCCode=@pcCode", new SqlParameter("@pcCode", id)).SingleOrDefaultAsync();
                    ICD_TruckProcess? tProcess = await _context.ICD_TruckProcess.FromSqlRaw("SELECT TOP 1* FROM ICD_TruckProcess WHERE InPCCode=@pcCode", new SqlParameter("@pcCode", id)).SingleOrDefaultAsync();
                    if(docSetting==null && ibCheck==null && obCheck==null && tProcess == null)
                    {
                        _context.PCategory.Remove(pCategory);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully Removed!";
                    }
                    else
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data Exists in Another Table!!";
                    }
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

        #region PCard Nov_27_2024
        public async Task<ResponseMessage> SavePCard(PCardDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                PCard? card = await _context.PCard.FromSqlRaw("SELECT Top 1* FROM PCard WHERE YardID=@yid And GroupName=@gname Order By CardNo Desc", new SqlParameter("@yid", info.YardID), new SqlParameter("@gname", info.GroupName)).SingleOrDefaultAsync();
                int srNo = 1;
                string id = string.IsNullOrEmpty(info.YardID) ? "" : info.YardID + "-";
                string sg = string.IsNullOrEmpty(info.GroupName) ? "" : info.GroupName.Substring(0, 1) + "-";
                id += sg;
                if (card != null)
                {
                    srNo = int.Parse(card.CardNo.Substring(id.Length)) + 1;
                    id += srNo.ToString("000");
                }
                else
                {
                    id += srNo.ToString("000");
                }
                info.CardNo = id;
                PCard data = _mapper.Map<PCard>(info);
                data.CreatedDate = GetLocalStdDT();
                _context.PCard.Add(data);
                await _context.SaveChangesAsync();
                msg.Status = true;
                msg.MessageContent = "Successfully Added!";
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> UpdatePCard(PCardDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                PCard? pCard = await _context.PCard.SingleOrDefaultAsync(tt => tt.CardNo == info.CardNo);
                if (pCard == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    pCard.CardNo = info.CardNo;
                    pCard.YardID = info.YardID;
                    pCard.GroupName = info.GroupName;
                    pCard.Active = info.Active;
                    pCard.IsUse = info.IsUse;
                    pCard.VehicleRegNo = info.VehicleRegNo;
                    pCard.CardIssueDate = info.CardIssueDate;
                    pCard.CardReturnDate = info.CardReturnDate;
                    pCard.UpdatedDate = GetLocalStdDT();
                    pCard.UpdatedUser = info.UpdatedUser;
                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Successfully Updated!";
                    msg.Status = true;
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
        }
        public async Task<ResponseMessage> DeletePCard(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                PCard? pCard = await _context.PCard.FromSqlRaw("SELECT * FROM PCard Where CardNo=@cardNo", new SqlParameter("@cardNo", id)).SingleOrDefaultAsync();
                if (pCard == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                }
                else
                {
                    ICD_InBoundCheck? ibCheck=await _context.ICD_InBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_InBoundCheck WHERE CardNo=@cardNo",new SqlParameter("@cardNo",id)).SingleOrDefaultAsync();
                    ICD_OutBoundCheck? obCheck = await _context.ICD_OutBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_OutBoundCheck WHERE CardNo=@cardNo", new SqlParameter("@cardNo", id)).SingleOrDefaultAsync();
                    ICD_TruckProcess? tProcess = await _context.ICD_TruckProcess.FromSqlRaw("SELECT TOP 1* FROM ICD_TruckProcess WHERE CardNo=@cardNo", new SqlParameter("@cardNo", id)).SingleOrDefaultAsync();
                    WeightBridgeQueue? wbQueue = await _context.WeightBridgeQueue.FromSqlRaw("SELECT TOP 1* FROM WeightBridgeQueue WHERE CardNo=@cardNo", new SqlParameter("@cardNo", id)).SingleOrDefaultAsync();

                    if (ibCheck==null && obCheck==null && tProcess == null && wbQueue==null)
                    {
                        _context.PCard.Remove(pCard);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully Removed!";
                    }
                    else
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data Exists in Another table!!";
                    }
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

        #region DocumentSetting  Nov_29_2024
        public async Task<ResponseMessage> SaveDocumentSetting(DocumentSettingDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                DocumentSetting? data = await _context.DocumentSetting.FromSqlRaw("SELECT TOP 1* FROM DocumentSetting WHERE REPLACE(DocCode,'','')=REPLACE(@docCode,'','') OR REPLACE(DocName,'','')=REPLACE(@docName,'','')", new SqlParameter("@docCode", info.DocCode),new SqlParameter("@docName",info.DocName)).SingleOrDefaultAsync();
                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Document Data Duplicate!!";
                    return msg;
                }
                else
                {
                    DocumentSetting docSetting = _mapper.Map<DocumentSetting>(info);
                    docSetting.CreatedDate = GetLocalStdDT();
                    _context.DocumentSetting.Add(docSetting);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Added!";
                    return msg;
                }
            }
            catch(DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> UpdateDocumentSetting(DocumentSettingDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                DocumentSetting? docSetting = await _context.DocumentSetting.SingleOrDefaultAsync(d => d.DocCode == info.DocCode);
                if (docSetting == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    docSetting.DocCode = info.DocCode;
                    docSetting.DocName = info.DocName;
                    docSetting.PCCode = info.PCCode;
                    docSetting.AttachRequired = info.AttachRequired;
                    docSetting.IsInDoc = info.IsInDoc;
                    docSetting.IsOutDoc = info.IsOutDoc;
                    docSetting.Active = info.Active;
                    docSetting.UpdatedDate = GetLocalStdDT();
                    docSetting.UpdatedUser = info.UpdatedUser;
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Updated!";
                    return msg;
                }
            }
            catch(DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
        }
        public async Task<ResponseMessage> DeleteDocumentSetting(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                DocumentSetting? docSetting = await _context.DocumentSetting.FromSqlRaw("SELECT * FROM DocumentSetting Where DocCode=@docCode", new SqlParameter("@docCode", id)).SingleOrDefaultAsync();
                if (docSetting == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                }
                else
                {
                    ICD_InBoundCheck_Document? ibDocument = await _context.ICD_InBoundCheck_Document.FromSqlRaw("SELECT TOP 1* FROM ICD_InBoundCheck_Document WHERE DocCode=@docCode", new SqlParameter("@docCode", id)).SingleOrDefaultAsync();
                    ICD_OutBoundCheck_Document? obDocument = await _context.ICD_OutBoundCheck_Document.FromSqlRaw("SELECT TOP 1* FROM ICD_OutBoundCheck_Document WHERE DocCode=@docCode", new SqlParameter("@docCode", id)).SingleOrDefaultAsync();
                    if(ibDocument==null && obDocument == null)
                    {
                        _context.DocumentSetting.Remove(docSetting);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully Removed!";
                    }
                    else
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data Exists in Another Table!!";
                    }
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

        #region OperationArea Nov_29_2024
        public async Task<ResponseMessage> SaveOperationArea(OperationAreaDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                OperationArea? data = await _context.OperationArea.FromSqlRaw("SELECT TOP 1* FROM OperationArea WHERE REPLACE(AreaID,'','')=REPLACE(@areaID,'','') OR REPLACE(Name,'','')=REPLACE(@name,'','')",
                    new SqlParameter("@areaID", info.AreaID),new SqlParameter("@name",info.Name)).SingleOrDefaultAsync();
                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Area Duplicate!!";
                }
                else
                {
                    OperationArea area = _mapper.Map<OperationArea>(info);
                    area.CreatedDate = GetLocalStdDT();
                    _context.OperationArea.Add(area);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Added!";
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
            return msg;
        }
        public async Task<ResponseMessage> UpdateOperationArea(OperationAreaDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                OperationArea? opArea = await _context.OperationArea.SingleOrDefaultAsync(d => d.AreaID == info.AreaID);
                if (opArea == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                    return msg;
                }
                else
                {
                    opArea.AreaID = info.AreaID;
                    opArea.Name = info.Name;
                    opArea.YardID = info.YardID;
                    opArea.Active = info.Active;
                    opArea.IsWaitingArea = info.IsWaitingArea;
                    opArea.UpdatedDate = GetLocalStdDT();
                    opArea.UpdatedUser = info.UpdatedUser;
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Updated!";
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }
        }
        public async Task<ResponseMessage> DeleteOperationArea(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                OperationArea? opArea = await _context.OperationArea.FromSqlRaw("SELECT * FROM OperationArea Where AreaID=@areaID", new SqlParameter("@areaID", id)).SingleOrDefaultAsync();
                if (opArea == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found!!";
                }
                else
                {
                    ICD_InBoundCheck? ibCheck = await _context.ICD_InBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_InBoundCheck WHERE AreaID=@areaID", new SqlParameter("@areaID", id)).SingleOrDefaultAsync();
                    ICD_OutBoundCheck? obCheck = await _context.ICD_OutBoundCheck.FromSqlRaw("SELECT TOP 1* FROM ICD_OutBoundCheck WHERE AreaID=@areaID", new SqlParameter("@areaID", id)).SingleOrDefaultAsync();
                    ICD_TruckProcess? tProcess = await _context.ICD_TruckProcess.FromSqlRaw("SELECT TOP 1* FROM ICD_TruckProcess WHERE AreaID=@areaID", new SqlParameter("@areaID", id)).SingleOrDefaultAsync();
                    if(ibCheck==null && obCheck==null && tProcess == null)
                    {
                        _context.OperationArea.Remove(opArea);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully Removed!";
                    }
                    else
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data Exists in Another Table!!";
                    }
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
