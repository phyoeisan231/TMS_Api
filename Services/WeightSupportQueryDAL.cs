using AutoMapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TMS_Api.Services
{
    public class WeightSupportQueryDAL
    {
        private readonly TMSDBContext _context;
        private readonly IConfiguration _configuration;
        string _conStr;
        private readonly IMapper _mapper;

        public WeightSupportQueryDAL(IConfiguration config, TMSDBContext context, IMapper mapper)
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
        #region In Weight 11_Dec_2024
        public async Task<DataTable> GetWeightBridgeQueueList(string yard, string gate)
        {
            string sql = @"SELECT * FROM WeightBridgeQueue WHERE YardID=@yard AND GateID=@gate AND Status='Queue'";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", yard), new SqlParameter("@gate", gate));
            return dt;
        }

        public async Task<DataTable> GetWeightBridgeList()
        {
            string sql = @"SELECT * FROM WeightBridge WHERE Active=1";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }


        public async Task<DataTable> GetTruckList(string active, string isBlack)
        {
            string sql = "SELECT VehicleRegNo FROM Truck Where Active=1 And (IsBlack<>1 OR IsBlack is null)";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }


        public async Task<DataTable> GetTrailerList(string active, string isBlack)
        {
            string sql = "SELECT VehicleRegNo FROM Trailer Where Active=1 And (IsBlack<>1 OR IsBlack is null)";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }


        public async Task<DataTable> GetServiceBillList(DateTime fromDate, DateTime toDate, string yard, string gate)
        {
            string sql = @"SELECT * FROM WeightServiceBill WHERE Cast(ServiceBillDate as Date) Between @fDate and @tDate AND GateID=@gate AND YardID=@yard  ORDER BY ServiceBillDate DESC";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@fDate", fromDate), new SqlParameter("@tDate", toDate), new SqlParameter("@gate", gate), , new SqlParameter("@yard", yard));
            return dt;
        }
        #endregion

    }
}
