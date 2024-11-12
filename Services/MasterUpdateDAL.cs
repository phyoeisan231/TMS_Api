using AutoMapper;
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

        public MasterUpdateDAL(TMSDBContext context, IConfiguration config, IMapper mapper)
        {
            _context = context;
            _configuration = config;
            _conStr = _configuration.GetConnectionString("DefaultConnection");
            _mapper = mapper;
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
            //TZConvert.
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

        #region Truck Type Nov_11_2024
        public async Task<ResponseMessage> SaveTruckType(TruckTypeDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TruckType data = await _context.TruckType.FromSqlRaw("SELECT Top 1 * FROM TruckType WHERE  REPLACE(Description,' ','')=REPLACE(@name,' ','')", new SqlParameter("@name", info.Description)).SingleOrDefaultAsync();

                if (data != null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Name duplicate!";
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

                    type.Active = info.Active;
                    type.UpdatedDate = GetLocalStdDT();
                    type.UpdatedUser = info.UpdatedUser;

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

        public async Task<ResponseMessage> DeleteTruckType(int id)
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
    }

}
