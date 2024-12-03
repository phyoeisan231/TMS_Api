using AutoMapper;
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


        public GateSupportUpdateDAL(TMSDBContext context, IConfiguration config, IMapper mapper)
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
        public async Task<ResponseMessage> SaveGateIn(ICD_InBoundCheckDto info)
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
                        process.UpdatedUser = info.UpdatedUser;
                        process.UpdatedDate = GetLocalStdDT();
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
