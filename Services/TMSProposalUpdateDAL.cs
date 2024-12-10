using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using TMS_Api.DBModels;
using TMS_Api.DTOs;

namespace TMS_Api.Services
{
    public class TMSProposalUpdateDAL
    {
        private readonly TMSDBContext _context;
        private readonly IConfiguration _configuration;
        public string _conStr { get; }
        private readonly IMapper _mapper;


        public TMSProposalUpdateDAL(TMSDBContext context, IConfiguration config, IMapper mapper)
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

        public async Task<ResponseMessage> CreateTMSProposal(TMS_ProposalDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TMS_Proposal proposal = _mapper.Map<TMS_Proposal>(info);
                _context.TMS_Proposal.Add(proposal);
                await _context.SaveChangesAsync();
                msg.Status = true;
                msg.MessageContent = "Successfully Created!";
            }
            catch (Exception e)
            {
                msg.MessageContent = e.Message;
            }
            return msg;
        }
    }
}
