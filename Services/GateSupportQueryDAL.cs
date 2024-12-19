using AutoMapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TMS_Api.Services
{
    public class GateSupportQueryDAL
    {
        private readonly TMSDBContext _context;
        private readonly IConfiguration _configuration;
        string _conStr;
        private readonly IMapper _mapper;

        public GateSupportQueryDAL(IConfiguration config, TMSDBContext context, IMapper mapper)
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
        #region Gate In Dec_2_2024
        public async Task<DataTable> GetInBoundCheckCardList(string yard, string gate)
        {
            string sql = @"SELECT * from ICD_TruckProcess where InYardID=@yard And InGateID=@gate And InYard=0 And Status='In(Check)'";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", yard), new SqlParameter("@gate", gate));
            return dt;
        }

        public async Task<DataTable> GetInBoundCheckList(string yard, string gate, string fDate, string tDate)
        {
            string sql = @"SELECT * from ICD_TruckProcess where InYardID=@yard And InGateID=@gate And InYard=0 And Status='In(Check)' And Cast(InCheckDateTime as Date) Between @fDate And @tDate";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", yard), new SqlParameter("@gate", gate), new SqlParameter("@fDate", fDate), new SqlParameter("@tDate", tDate));
            return dt;
        }
        #endregion

        #region Gate Out Dec_9_2024
        public async Task<DataTable> GetOutBoundCheckCardList(string yard, string gate)
        {
            string sql = @"SELECT * from ICD_TruckProcess where OutYardID=@yard And OutGateID=@gate And InYard=1 And Status='Out(Check)'";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", yard), new SqlParameter("@gate", gate));
            return dt;
        }

        public async Task<DataTable> GetOutBoundCheckList(string yard, string gate, string fDate, string tDate)
        {
            string sql = @"SELECT * from ICD_TruckProcess where OutYardID=@yard And OutGateID=@gate And InYard=1 And Status='Out(Check)' And Cast(OutCheckDateTime as Date) Between @fDate And @tDate";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", yard), new SqlParameter("@gate", gate), new SqlParameter("@fDate", fDate), new SqlParameter("@tDate", tDate));
            return dt;
        }

        #endregion

        #region Truck Status Dec_12_2024
        public async Task<DataTable> GetTruckStatusReport(string yard, string gate, string fDate, string tDate,string status)
        {
            string sql = @"SELECT * from ICD_TruckProcess where InYardID=@yard And InGateID=@gate And Status in (" + status + ")  And Cast(InCheckDateTime as Date) Between @fDate And @tDate";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", yard), new SqlParameter("@gate", gate), new SqlParameter("@fDate", fDate), new SqlParameter("@tDate", tDate));
            return dt;
        }

        #endregion

        #region Daily Report Dec_12_2024
        public async Task<DataTable> GetDailyInReport(string yard, string gate, string fDate, string tDate)
        {
            string sql = @"SELECT * from ICD_TruckProcess where InYardID=@yard And InGateID=@gate And InYard=1 And Status='In' And Cast(InCheckDateTime as Date) Between @fDate And @tDate";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", yard), new SqlParameter("@gate", gate), new SqlParameter("@fDate", fDate), new SqlParameter("@tDate", tDate));
            return dt;
        }

        public async Task<DataTable> GetDailyOutReport(string yard, string gate, string fDate, string tDate)
        {
            string sql = @"SELECT * from ICD_TruckProcess where OutYardID=@yard And OutGateID=@gate And InYard=0 And Status='Out' And Cast(OutCheckDateTime as Date) Between @fDate And @tDate";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", yard), new SqlParameter("@gate", gate), new SqlParameter("@fDate", fDate), new SqlParameter("@tDate", tDate));
            return dt;
        }

        #endregion
    }
}
