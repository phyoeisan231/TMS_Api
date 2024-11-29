using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TMS_Api.DBModels;
using TMS_Api.DTOs;

namespace TMS_Api.Services
{
    public class TMSOperationQueryDAL
    {
        private readonly TMSDBContext _context;
        private readonly IConfiguration _configuration;
        string _conStr;
        private readonly IMapper _mapper;

        public TMSOperationQueryDAL(IConfiguration config, TMSDBContext context, IMapper mapper, ILogger<TMSOperationQueryDAL> logger)
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

        #region InBound Check Nov_27_2024

        public async Task<DataTable> GetTruckList(string id, string type)
        {
            string sql = "";
            if (type == "RGL")
            {
                sql = @"Select VehicleRegNo,ContainerType,ContainerSize,TypeID,TransporterID,DriverLicenseNo from Truck where VehicleRegNo like '%'" + id + "'%' And IsRGL=1 And IsBlack<>1 And Active=1";
            }
            else
            {
                sql = @"Select VehicleRegNo,ContainerType,ContainerSize,TypeID,TransporterID,DriverLicenseNo from Truck where VehicleRegNo like '%'" + id + "'%' And IsRGL<>1 And IsBlack<>1 And Active=1";
            }
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }


        public async Task<DataTable> GetDriverList(string id)
        {
            DateTime strDate = GetLocalStdDT();
            string sql = @"Select Name,LicenseNo,LicenseExpiration from Driver where IsBlack<>1 And Active=1 And Cast(LicenseExpiration as Date)>=@eDate And LicenseNo like '%" + id + "%' ";
            DataTable dt = await GetDataTableAsync(sql,new SqlParameter("@eDate", strDate));
            return dt;
        }

        public async Task<DataTable> GetTrailerList()
        {
            string sql = @"Select VehicleRegNo,DriverLicenseNo,ContainerType,ContainerSize,TransporterID from Trailer where IsBlack<>1 And Active=1";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }


        public async Task<DataTable> GetInBoundCheckList(DateTime startDate, DateTime endDate,string yard)
        {          
           string sql = @"Select * from ICD_InBoundCheck where InYardID=@yard And Cast(InCheckDateTime as Date) Between @sDate and @eDate  Order by InRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@sDate", startDate), new SqlParameter("@eDate", endDate), new SqlParameter("@yard", yard));
            return dt;
        }

        public async Task<ICD_InBoundCheckDto> GetInBoundCheckById(int id)
        {
            ICD_InBoundCheckDto info = new ICD_InBoundCheckDto();
            try
            {
                ICD_InBoundCheck? data = await _context.ICD_InBoundCheck.FromSqlRaw("SELECT * FROM  InBoundCheck WHERE InRegNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (data != null)
                {
                    info = _mapper.Map<ICD_InBoundCheckDto>(data);

                    List<ICD_InBoundCheck_Document> documentList = await _context.ICD_InBoundCheck_Document.FromSqlRaw("SELECT * FROM ICD_InBoundCheck_Document WHERE InRegNo=@id", new SqlParameter("@id", id)).ToListAsync();

                    info.DocumentList = new List<ICD_InBoundCheck_DocumentDto>();
                    foreach (var d in documentList)
                    {

                        ICD_InBoundCheck_DocumentDto docDto = _mapper.Map<ICD_InBoundCheck_DocumentDto>(d);
                        info.DocumentList.Add(docDto);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return info;
        }
        #endregion
    }
}
