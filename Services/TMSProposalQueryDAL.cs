using AutoMapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography.Xml;
using TMS_Api.DBModels;

namespace TMS_Api.Services
{
    public class TMSProposalQueryDAL
    {
        private readonly TMSDBContext _context;
        private readonly IConfiguration _configuration;
        string _conStr;
        string _portalConStr;
        private readonly IMapper _mapper;

        public TMSProposalQueryDAL(IConfiguration config, TMSDBContext context, IMapper mapper, ILogger<TMSOperationQueryDAL> logger)
        {
            _context = context;
            _configuration = config;
            _conStr = _configuration.GetConnectionString("DefaultConnection");
            _portalConStr = _configuration.GetConnectionString("PortalConnection");
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

        public Task<DataTable> GetPortalDataTableAsync(string sSQL, params SqlParameter[] para)
        {
            return Task.Run(() =>
            {
                using (var newCon = new SqlConnection(_portalConStr))
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


        #region In Check Proposal
        public async Task<DataTable> GetYardList()
        {
            string sql = @"SELECT * FROM Yards";
            DataTable dt = await GetPortalDataTableAsync(sql);
            return dt;
        }

        public async Task<DataTable> GetRailDailyJobList(string jobType, string yard)
        {
            string sql = @"SELECT * FROM RailDailyJobs where JobType=@jobType AND YCode=@yard AND JobStatus='In Progress'";
            DataTable dt = await GetPortalDataTableAsync(sql, new SqlParameter("@jobType", jobType), new SqlParameter("@yard", yard));
            return dt;
        }

        public async Task<DataTable> GetCustomerList()
        {
            string sql = @"select * from KYCCustomers";
            DataTable dt = await GetPortalDataTableAsync(sql);
            return dt;
        }

        public async Task<DataTable> GetWHDailyJobList(string jobType,string yard)
        {
            string sql = @"SELECT *, ServiceType As JobType FROM WarehouseDailyJobs where ServiceType=@jobType
AND  JobStatus='In Progress' AND LocationCode in (Select LocationCode from Locations where Description like '%"+yard+"%')";
            DataTable dt = await GetPortalDataTableAsync(sql,  new SqlParameter("@jobType", jobType));
            return dt;
        }

        public async Task<DataTable> GetCCADailyJobList(string jobType,string yard)
        {
            string sql = @"SELECT * FROM CCADailyJobs where JobType=@jobType AND JobStatus='In Progress' 
AND LocationCode in (Select LocationCode from Locations where Description like '%"+yard+"%')";
            DataTable dt = await GetPortalDataTableAsync(sql, new SqlParameter("@jobType", jobType));
            return dt;
        }

        public async Task<DataTable> GetProposalList(DateTime startDate, DateTime endDate, string deptType)
        {
            string sql = @"SELECT * FROM TMS_Proposal where JobDept in (" + deptType + ") And Cast(EstDate as Date) Between @sDate and @eDate  Order by PropNo DESC";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@sDate", startDate), new SqlParameter("@eDate", endDate));
            return dt;
        }

        public async Task<DataTable> GetTruckList(string type,string jobType)
        {
            string sql = @"SELECT * FROM TruckAssigns where  AssignType=@type AND JobType=@jobType AND JobStatus='In Progress'";
            DataTable dt = await GetPortalDataTableAsync(sql, new SqlParameter("@type", type),new SqlParameter("@jobType",jobType));
            return dt;
        }

        public async Task<DataTable> GetProposalDetailList(string propNo)
        {
            string sql = @"SELECT * FROM TMS_ProposalDetails WHERE  PropNo=@propNo";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@propNo", propNo));
            return dt;
        }

        public  async Task<DataTable> GetProposalListById(string id)
        {
            string sql = @"SELECT * FROM TMS_Proposal WHERE  PropNo=@propNo";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@propNo", id));
            return dt;
        }
        #endregion


        #region ProposalDetail

        #endregion

    }


}
