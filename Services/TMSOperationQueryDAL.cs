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
        public async Task<DataTable> GetInBoundCheckList(DateTime startDate, DateTime endDate,string yard)
        {
           
           string sql = @"Select * from InBoundCheck where InYardID=@yard And Cast(InCheckDateTime as Date) Between @sDate and @eDate  Order by InRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@sDate", startDate), new SqlParameter("@eDate", endDate), new SqlParameter("@yard", yard));
            return dt;
        }

        public async Task<InBoundCheckDto> GetInBoundCheckById(int id)
        {
            InBoundCheckDto info = new InBoundCheckDto();
            try
            {
                InBoundCheck? data = await _context.InBoundCheck.FromSqlRaw("SELECT * FROM  InBoundCheck WHERE InRegNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (data != null)
                {
                    info = _mapper.Map<InBoundCheckDto>(data);

                    List<InBoundCheckDocument> documentList = await _context.InBoundCheckDocument.FromSqlRaw("SELECT * FROM InBoundCheckDocument WHERE InRegNo=@id", new SqlParameter("@id", id)).ToListAsync();

                    info.DocumentList = new List<InBoundCheckDocumentDto>();
                    foreach (var d in documentList)
                    {

                        InBoundCheckDocumentDto docDto = _mapper.Map<InBoundCheckDocumentDto>(d);
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
