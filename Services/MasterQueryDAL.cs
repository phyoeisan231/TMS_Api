using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace TMS_Api.Services
{
    public class MasterQueryDAL
    {
        private readonly TMSDBContext _context;
        private readonly IConfiguration _configuration;
        string _conStr;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<MasterQueryDAL> _logger;
        private readonly string _storageConnectionString;
        private readonly string _storageFormContainerName;

        public MasterQueryDAL(IConfiguration config, TMSDBContext context, IMapper mapper, ILogger<MasterQueryDAL> logger, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _configuration = config;
            _conStr = _configuration.GetConnectionString("DefaultConnection");
            _storageConnectionString = _configuration.GetValue<string>("BlobConnectionString");
            _storageFormContainerName = _configuration.GetValue<string>("BlobContainerName");
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
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

        #region Truck Type Nov_11_2024
        public async Task<DataTable> GetTruckTypeList(string active)
        {
            string sql = "";
            if (active == "All" || active == null)
            {
                sql = @"SELECT * from TruckType";
            }
            else
            {
                sql = @"SELECT * from TruckType where Active='" + active + "'";
            }


            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        #endregion
    }

}
