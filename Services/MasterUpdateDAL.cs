using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
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
                TruckType data = await _context.TruckType.FromSqlRaw("SELECT TOP 1 * FROM TruckType WHERE REPLACE(Description, ' ', '') = REPLACE(@name, ' ', '') AND TypeID = @typeId",new SqlParameter("@name", info.Description),new SqlParameter("@typeId", info.TypeID)).SingleOrDefaultAsync();
                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data already In !";
                }
                else
                {
                    TruckType type = _mapper.Map<TruckType>(info);
                    type.UpdatedDate = GetLocalStdDT();
                    _context.TruckType.Add(type);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully added!";
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
                    msg.MessageContent = "Data not found!";
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
                    msg.MessageContent = "Successfully updated!";
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
                TruckType type = await _context.TruckType.FromSqlRaw("SELECT * FROM TruckType WHERE TypeCode=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (type == null)
                {
                    msg.MessageContent = "Data not found.";
                    return msg;
                }
                else
                {
                    Truck truck = await _context.Truck.FromSqlRaw("SELECT Top 1 * FROM Truck WHERE REPLACE(TruckType,' ','')=REPLACE(@name,' ','')", new SqlParameter("@name", type.Description)).SingleOrDefaultAsync();
                    if (truck == null)
                    {
                        _context.TruckType.Remove(type);

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
                TransporterType data = await _context.TransporterType.FromSqlRaw("SELECT Top 1 * FROM TransporterType WHERE REPLACE(TypeName,'','')=REPLACE(@name,'','')", new SqlParameter("@name", info.Description)).SingleOrDefaultAsync();
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
                TransporterType type = await _context.TransporterType.FromSqlRaw("SELECT * FROM TransporterType WHERE  REPLACE(TypeName,' ','')=REPLACE(@id,' ','')", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (type == null)
                {
                    msg.MessageContent = "Data not found.";
                    return msg;
                }
                else
                {
                    Transporter transporter = await _context.Transporter.FromSqlRaw("SELECT * FROM Transporter WHERE  REPLACE(Description,' ','')=REPLACE(@id,' ','')", new SqlParameter("@id", type.Description)).SingleOrDefaultAsync();
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
        public async Task<ResponseMessage> SaveTransporter([FromForm] TransporterDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Transporter duplicate = await _context.Transporter.FromSqlRaw("SELECT Top 1* FROM Transporter WHERE REPLACE(TransporterName,'','')=REPLACE(@name,'','')", new SqlParameter("@name", info.TransporterName)).SingleOrDefaultAsync();
                if (duplicate != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Transporter Name duplicate";
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
                _context.Transporter.Add(data);
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
        public async Task<ResponseMessage> UpdateTransporter([FromForm] TransporterDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Transporter transporter = await _context.Transporter.SingleOrDefaultAsync(t => t.TransporterID == info.TransporterID);

                if (transporter == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
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
                    transporter.IsBlack = info.IsBlack;
                    transporter.BlackDate = info.BlackDate;
                    transporter.BlackReason = info.BlackReason;
                    transporter.BlackRemovedDate = info.BlackRemovedDate;
                    transporter.BlackRemovedReason = info.BlackRemovedReason;
                    transporter.UpdatedDate = GetLocalStdDT();
                    transporter.UpdatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";

                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Successfully updated";
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
        public async Task<ResponseMessage> DeleteTransporter(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Transporter transporter = await _context.Transporter.FromSqlRaw("SELECT * FROM Transporter Where TransporterCode=@tCode", new SqlParameter("@tCode", id)).SingleOrDefaultAsync();
                if (transporter == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    _context.Transporter.Remove(transporter);
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

        #region Gate Nov_12_2024
        public async Task<ResponseMessage> SaveGate(GateDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                
                Gate data = await _context.Gate.FromSqlRaw("SELECT Top 1 * FROM Gate WHERE REPLACE(Name,'','')=REPLACE(@name,'','')", new SqlParameter("@name", info.Name)).SingleOrDefaultAsync();
                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Name duplicate!";
                }
                else
                {
                    Gate gateIdTest = await _context.Gate.FromSqlRaw("SELECT Top 1* FROM Gate Order By GateID Desc", new SqlParameter("@name", info.GateID)).SingleOrDefaultAsync();
                    if (gateIdTest != null)
                    {
                    //    msg.Status = false;
                    //    msg.MessageContent = "ID duplicate!";
                    //}
                    //else
                    //{
                        Gate gate = _mapper.Map<Gate>(info);
                        gate.UpdatedDate = GetLocalStdDT();
                        _context.Gate.Add(gate);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully added";
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
        public async Task<ResponseMessage> UpdateGate(GateDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Gate gate = await _context.Gate.FromSqlRaw("SELECT * FROM Gate WHERE REPLACE(GateID,' ','')=REPLACE(@gID,' ','') ", new SqlParameter("@gID", info.GateID)).SingleOrDefaultAsync();
                if (gate == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    gate.Name = info.Name;
                    gate.YardID = info.YardID;
                    gate.Active = info.Active;
                    gate.UpdatedDate = GetLocalStdDT();
                    gate.UpdatedUser = info.UpdatedUser;
                    _context.Gate.Update(gate);
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
        public async Task<ResponseMessage> DeleteGate(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Gate gate = await _context.Gate.FromSqlRaw("SELECT * FROM Gate WHERE  REPLACE(GateID,'','')=REPLACE(@id,'','')", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (gate == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    _context.Gate.Remove(gate);
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

        #region Truck Nov_12_2024
        public async Task<ResponseMessage> SaveTruck([FromForm] TruckDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Truck truck = await _context.Truck.FromSqlRaw("SELECT Top 1 * FROM Truck WHERE REPLACE(VehicleRegNo,' ','')=REPLACE(@name,' ','')", new SqlParameter("@name", info.VehicleRegNo)).SingleOrDefaultAsync();
                if (truck != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                    return msg;
                }
                else
                {
                    Truck data = _mapper.Map<Truck>(info);
                    data.CreatedUser = info.CreatedUser;
                    data.CreatedDate = GetLocalStdDT();
                    //data.CreatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";
                    _context.Truck.Add(data);
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
        public async Task<ResponseMessage> UpdateTruck([FromForm] TruckDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Truck truck = await _context.Truck.SingleOrDefaultAsync(t => t.VehicleRegNo == info.VehicleRegNo);

                if (truck == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                    return msg;
                }
                else
                {
                    truck.VehicleRegNo = info.VehicleRegNo;
                    truck.TypeID = info.TypeID;
                    truck.TransporterID = info.TransporterID;
                    truck.Remarks = info.Remarks;
                    truck.Active = info.Active;
                    truck.IsBlack = info.IsBlack;
                    truck.BlackDate = info.BlackDate;
                    truck.BlackRemovedDate = info.BlackRemovedDate;
                    truck.VehicleBackRegNo = info.VehicleBackRegNo;
                    truck.TruckWeight = info.TruckWeight;
                    truck.DriverLicenseNo = info.DriverLicenseNo;
                    truck.LastPassedDate = info.LastPassedDate;
                    truck.ContainerType = info.ContainerType;
                    truck.ContainerSize = info.ContainerSize;
                    truck.UpdatedDate = GetLocalStdDT();
                    truck.UpdatedUser=info.UpdatedUser;
                    //truck.UpdatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";

                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Successfully updated";
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
        public async Task<ResponseMessage> DeleteTruck(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Truck truck = await _context.Truck.FromSqlRaw("SELECT * FROM Truck Where VehicleRegNo=@regNo", new SqlParameter("@regNo", id)).SingleOrDefaultAsync();
                if (truck == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    _context.Truck.Remove(truck);
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

        #region Trailer Nov_12_2024
        public async Task<ResponseMessage> SaveTrailer([FromForm] TrailerDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Trailer trailer = await _context.Trailer.FromSqlRaw("SELECT Top 1 * FROM Trailer WHERE REPLACE(VehicleRegNo,' ','')=REPLACE(@name,' ','')", new SqlParameter("@name", info.VehicleRegNo)).SingleOrDefaultAsync();
                if (trailer != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
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
        public async Task<ResponseMessage> UpdateTrailer([FromForm] TrailerDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Trailer trailer = await _context.Trailer.SingleOrDefaultAsync(t => t.VehicleRegNo == info.VehicleRegNo);

                if (trailer == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
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
                    trailer.VehicleBackRegNo = info.VehicleBackRegNo;
                    trailer.IsBlack = info.IsBlack;
                    trailer.DriverLicenseNo = info.DriverLicenseNo;
                    trailer.BlackRemovedDate = info.BlackRemovedDate;
                    trailer.BlackDate = info.BlackDate;
                    trailer.LastPassedDate = info.LastPassedDate;
                    trailer.UpdatedDate = GetLocalStdDT();
                    trailer.UpdatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";

                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Successfully updated";
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
        public async Task<ResponseMessage> DeleteTrailer(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Trailer trailer = await _context.Trailer.FromSqlRaw("SELECT * FROM Trailer Where VehicleRegNo=@regNo", new SqlParameter("@regNo", id)).SingleOrDefaultAsync();
                if (trailer == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    _context.Trailer.Remove(trailer);
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

        #region Driver Nov_12_2024
        public async Task<ResponseMessage> SaveDriver([FromForm] DriverDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Driver driver = await _context.Driver.FromSqlRaw("SELECT Top 1 * FROM Driver WHERE REPLACE(LicenseNo,' ','')=REPLACE(@name,' ','')", new SqlParameter("@name", info.LicenseNo)).SingleOrDefaultAsync();
                if (driver != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
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
        public async Task<ResponseMessage> UpdateDriver([FromForm] DriverDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Driver driver = await _context.Driver.SingleOrDefaultAsync(tt => tt.LicenseNo == info.LicenseNo);
                if (driver == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
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
                    driver.ContactNo = info.ContactNo;
                    driver.Email = info.Email;
                    driver.Remarks = info.Remarks;
                    driver.IsBlack = info.IsBlack;
                    driver.BlackDate = info.BlackDate;
                    driver.BlackReason = info.BlackReason;
                    driver.BlackRemovedDate = info.BlackRemovedDate;
                    driver.BlackRemovedReason = info.BlackRemovedReason;
                    driver.UpdatedDate = GetLocalStdDT();
                    driver.UpdatedUser = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "UnknownUser";

                    await _context.SaveChangesAsync();
                    msg.Message = "Successfully Updated";
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
                Driver driver = await _context.Driver.FromSqlRaw("SELECT * FROM Driver Where LicenseNo=@lNo", new SqlParameter("@lNo", id)).SingleOrDefaultAsync();
                if (driver == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    _context.Driver.Remove(driver);
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

        #region Yard Nov_2_2024
        public async Task<ResponseMessage> SaveYard(YardDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {

                Yard data = await _context.Yard.FromSqlRaw("SELECT Top 1 * FROM Yard WHERE REPLACE(Name,'','')=REPLACE(@name,'','')", new SqlParameter("@name", info.Name)).SingleOrDefaultAsync();
                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Name duplicate!";
                }
                else
                {
                    Yard yardIDTest = await _context.Yard.FromSqlRaw("SELECT Top 1* FROM Yard WHERE REPLACE(YardID,'','')=REPLACE(@yID,'','')", new SqlParameter("@yID", info.YardID)).SingleOrDefaultAsync();
                    if (yardIDTest != null)
                    {
                        msg.Status = false;
                        msg.MessageContent = "YardID duplicate!";//For User Input ID Testing
                    }
                    else
                    {
                        Yard yard = _mapper.Map<Yard>(info);
                        yard.UpdatedDate = GetLocalStdDT();
                        _context.Yard.Add(yard);
                        await _context.SaveChangesAsync();
                        msg.Status = true;
                        msg.MessageContent = "Successfully added";
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
        public async Task<ResponseMessage> UpdateYard(YardDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Yard yard = await _context.Yard.FromSqlRaw("SELECT * FROM Yard WHERE REPLACE(YardID,' ','')=REPLACE(@yID,' ','') ", new SqlParameter("@yID", info.YardID)).SingleOrDefaultAsync();
                if (yard == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
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
        public async Task<ResponseMessage> DeleteYard(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                Yard data = await _context.Yard.FromSqlRaw("SELECT Top 1* FROM Yard WHERE REPLACE(YardID,'','')=REPLACE(@yID,'','')", new SqlParameter("@yID", id)).SingleOrDefaultAsync();
                if(data == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data Not Found";
                }
                else
                {
                    _context.Yard.Remove(data);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully Removed";
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
        #endregion

    }

}
