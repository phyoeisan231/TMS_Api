using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Syncfusion.XlsIO;
using System.Data;
using System.Data.Common;
using TMS_Api.DBModels;
using TMS_Api.DTOs;
using static System.Net.Mime.MediaTypeNames;

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
                proposal.CreatedDate = GetLocalStdDT();
                proposal.CreatedUser = info.CreatedUser;
                proposal.Status = "Open";
                _context.TMS_Proposal.Add(proposal);
                await _context.SaveChangesAsync();

                msg.Status = true;
                msg.MessageContent = "Successfully Created!";
                msg.Message = Convert.ToString(proposal.PropNo);
            }
            catch (Exception e)
            {
                msg.MessageContent = e.Message;
            }
            return msg;
        }

        #region TMS Proposal Detail

        public async Task<ResponseMessage> CreateProposalDetail(TMS_ProposalDetailDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };

            try
            {
                TMS_Proposal proposal= await _context.TMS_Proposal.FromSqlRaw("SELECT * FROM TMS_Proposal WHERE PropNo=@propNo", new SqlParameter("@propNo", info.PropNo)).SingleOrDefaultAsync();
                if(proposal != null)
                {
                    TMS_ProposalDetail proDetail = await _context.TMS_ProposalDetails.FromSqlRaw("SELECT * FROM  TMS_ProposalDetails WHERE PropNo=@propNo AND TruckNo=@truckNo", new SqlParameter("@propNo", info.PropNo), new SqlParameter("@truckNo", info.TruckNo)).SingleOrDefaultAsync();
                    if (proDetail != null)
                    {
                        msg.MessageContent = "Truck No Duplicated!";
                        return msg;
                    }
                    else
                    {
                        List<String> truckNo = JsonConvert.DeserializeObject<List<String>>(info.TruckNo);
                        if (truckNo != null)
                        {
                            foreach(String truck in truckNo)
                            {
                               TMS_ProposalDetail detail = await _context.TMS_ProposalDetails.FromSqlRaw("SELECT * FROM TMS_ProposalDetails WHERE PropNo=@id AND TruckNo=@truckNo", new SqlParameter("@id", info.PropNo), new SqlParameter("@truckNo", truck)).SingleOrDefaultAsync();
                                if (detail == null)
                                {
                                    info.TruckNo = truck;
                                    TMS_ProposalDetail newPropDetial = _mapper.Map<TMS_ProposalDetail>(info);
                                    newPropDetial.CreatedDate = GetLocalStdDT();
                                    if (info.AssignType == "Customer") newPropDetial.TruckAssignOption = "None";
                                    else newPropDetial.TruckAssignOption = "Assign";
                                    _context.TMS_ProposalDetails.AddRange(newPropDetial);
                                }
                            }
                           
                        }
                       
                        await _context.SaveChangesAsync();
                        msg.MessageContent = "Successfully Created!";
                        msg.Status = true;
                    }
                }
            }
            catch(Exception e)
            {
                msg.MessageContent = e.Message;
            }
            return msg;
        }

        public async Task<ResponseMessage> DeleteProposal(string id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TMS_Proposal proposal = await _context.TMS_Proposal.FromSqlRaw("SELECT * FROM TMS_Proposal WHERE PropNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (proposal == null)
                {
                    msg.MessageContent = "Data Not Found!";
                    return msg;
                }
                else
                {
                    List<TMS_ProposalDetail> propDetail = await _context.TMS_ProposalDetails.FromSqlRaw("SELECT * FROM TMS_ProposalDetails WHERE PropNo=@id", new SqlParameter("@id", id)).ToListAsync();
                    if (propDetail != null)
                    {
                        _context.TMS_ProposalDetails.RemoveRange(propDetail);
                    }
                    _context.TMS_Proposal.Remove(proposal);
                    await _context.SaveChangesAsync();
                    msg.MessageContent = "Successfully Delete!";
                    msg.Status = true;
                }
            }
            catch(Exception e)
            {
                msg.MessageContent = e.Message;
            }
            return msg;
        }

        public async Task<ResponseMessage> UpdateTMSProposal(TMS_ProposalDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TMS_Proposal proposal = await _context.TMS_Proposal.FromSqlRaw("SELECT * FROM TMS_Proposal WHERE PropNo=@id", new SqlParameter("@id", info.PropNo)).SingleOrDefaultAsync();
                if (proposal == null)
                {
                    msg.MessageContent = "Data Not Found!";
                    return msg;
                }

                proposal.NoOfTruck = info.NoOfTruck;
                proposal.NoOfTEU = info.NoOfTEU;
                proposal.NoOfFEU = info.NoOfFEU;
                proposal.LCLQty = info.LCLQty;
                proposal.CargoInfo = info.CargoInfo;
                proposal.WeightOption = info.WeightOption;
                proposal.UpdatedDate = GetLocalStdDT();
                proposal.UpdatedUser = info.UpdatedUser;

                await _context.SaveChangesAsync();
                msg.MessageContent = "Successfully Updated!";
                msg.Status = true;
            }
            catch(Exception e)
            {
                msg.MessageContent = e.Message;
            }
            return msg;
        }

        public async Task<ResponseMessage> CompleteProposal(int id, string user)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TMS_Proposal proposal = await _context.TMS_Proposal.FromSqlRaw("SELECT * FROM TMS_Proposal WHERE PropNo=@id And Status='Open'", new SqlParameter("@id",id)).SingleOrDefaultAsync();
                if (proposal == null)
                {
                    msg.MessageContent = "Data Not Found!";
                    return msg;
                }
                proposal.Status = "Complete";
                proposal.UpdatedDate = GetLocalStdDT();
                proposal.UpdatedUser = user;
                await _context.SaveChangesAsync();
                msg.MessageContent = "Successfully completed!";
                msg.Status = true;
            }
            catch (Exception e)
            {
                msg.MessageContent = e.Message;
            }
            return msg;
        }
        
        #endregion

        #region ProposalDetail
        public async Task<ResponseMessage> DeleteProposalDetail(string id, string truckNo)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                TMS_ProposalDetail propDetail = await _context.TMS_ProposalDetails.FromSqlRaw("SELECT * FROM TMS_ProposalDetails WHERE PropNo=@id AND TruckNo=@truckNo", new SqlParameter("@id", id), new SqlParameter("@truckNo", truckNo)).SingleOrDefaultAsync();
                if (propDetail == null)
                {
                    msg.MessageContent = "Data Not Found!";
                    return msg;
                }

                _context.TMS_ProposalDetails.Remove(propDetail);
                await _context.SaveChangesAsync();
                msg.MessageContent = "Successfully Delete!";
                msg.Status = true;
            }
            catch(Exception e)
            {
                msg.MessageContent = e.Message;
                
            }
            return msg;
        }

        public async Task<ResponseMessage> StatusChange(FileUploadDTO info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };

            try
            {
                using (var stream = new MemoryStream())
                {
                    await info.UploadedFile.CopyToAsync(stream);
                    int i = 0;

                    using (ExcelEngine excelEngine = new ExcelEngine())
                    {
                        IApplication application = excelEngine.Excel;
                        application.DefaultVersion = ExcelVersion.Xlsx;

                        stream.Position = 0;
                        IWorkbook workbook = application.Workbooks.Open(stream);
                        IWorksheet worksheet = workbook.Worksheets[0];

                        int rowCount = worksheet.UsedRange.LastRow;
                        for (int row = 2; row <= rowCount; row++) // Assuming the first row is the header
                        {

                            string jobCode = worksheet[row, 1].Text;

                            List<TMS_Proposal> proposal = await _context.TMS_Proposal.FromSqlRaw("SELECT * FROM TMS_Proposal WHERE JobCode=@jobCode AND Status='Open' AND JobDept=@jobDept",new SqlParameter("@jobCode",jobCode),new SqlParameter("jobDept",info.JobDept)).ToListAsync();
                            foreach(TMS_Proposal p in proposal)
                            {
                                p.Status = "Close";
                                await _context.SaveChangesAsync();
                                i++;
                            }
                           
                        }
                    }

                    if (i > 0)
                    {
                        msg.Status = true;
                        msg.Message = Convert.ToString(i);
                        msg.MessageContent = "Successfully imported!";
                    }
                    else
                    {
                        msg.MessageContent = "JobCode Not Found!";
                    }
                    
                }

              
            }
            catch (Exception ex)
            {

                msg.MessageContent = ex.Message;
                return msg;
            }

            return msg;
        }
        #endregion


    }
}
