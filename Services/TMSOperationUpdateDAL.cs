using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TMS_Api.DBModels;
using TMS_Api.DTOs;

namespace TMS_Api.Services
{
    public class TMSOperationUpdateDAL
    {
        private readonly TMSDBContext _context;
        private readonly IConfiguration _configuration;
        public string _conStr { get; }
        private readonly IMapper _mapper;


        public TMSOperationUpdateDAL(TMSDBContext context, IConfiguration config, IMapper mapper)
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
        public async Task<ResponseMessage> SaveInBoundCheck(InBoundCheckDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    InBoundCheck inBound = _mapper.Map<InBoundCheck>(info);
                    inBound.CreatedDate = GetLocalStdDT();
                    inBound.Status = "Check(In)";
                    _context.InBoundCheck.Add(inBound);
                    await _context.SaveChangesAsync();
                    List<DocumentSetting> documentList = await _context.DocumentSetting.FromSqlRaw("SELECT * FROM DocumentSetting WHERE PCCode=@id And AttachRequired=1 And Active=1", new SqlParameter("@id", info.InPCCode)).ToListAsync();
                    foreach (var i in documentList)
                    {
                        InBoundCheckDocument doc = new InBoundCheckDocument();
                        doc.CheckStatus = false;
                        doc.DocName = doc.DocName;
                        doc.DocCode = doc.DocCode;
                        doc.InRegNo = inBound.InRegNo;
                        doc.CreatedDate = GetLocalStdDT();
                        doc.CreatedUser = info.CreatedUser;
                        _context.InBoundCheckDocument.Add(doc);
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    msg.Status = true;
                    msg.MessageContent = inBound.InRegNo.ToString();
                }
                catch (DbUpdateException e)
                {
                    // Rollback the transaction if any exception occurs
                    await transaction.RollbackAsync();
                    msg.MessageContent += e.Message;
                    return msg;
                }
            }
            return msg;
        }

        public async Task<ResponseMessage> UpdateInBoundCheck(InBoundCheckDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                InBoundCheck? data = await _context.InBoundCheck.FromSqlRaw("SELECT * FROM InBoundCheck WHERE  InRegNo=@id", new SqlParameter("@id", info.InRegNo)).SingleOrDefaultAsync();
                if (data == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found!";
                }
                else
                {
                    data.UpdatedDate = GetLocalStdDT();
                    data.UpdatedUser = info.UpdatedUser;
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

        public async Task<ResponseMessage> DeleteInBoundCheck(int id)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                InBoundCheck? data = await _context.InBoundCheck.FromSqlRaw("SELECT * FROM InBoundCheck WHERE InRegNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (data == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found.";
                    return msg;
                }
                else
                {
                    TruckProcess? process = await _context.TruckProcess.FromSqlRaw("SELECT Top 1* FROM TruckProcess WHERE InRegNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                    if (process != null)
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data is used by another process!";
                        return msg;
                    }
                     _context.InBoundCheck.Remove(data);
                     List<InBoundCheckDocument> detailList = await _context.InBoundCheckDocument.FromSqlRaw("SELECT * FROM InBoundCheckDocument WHERE InRegNo=@id", new SqlParameter("@id", id)).ToListAsync();
                     if (detailList.Count > 0)
                     {
                        _context.InBoundCheckDocument.RemoveRange(detailList);
                     }                                   
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Removed successfully!";
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.Status = false;
                msg.MessageContent += e.Message;
                return msg;
            }

        }

        public async Task<ResponseMessage> UpdateInBoundCheckDocument(InBoundCheckDocumentDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                InBoundCheckDocument? doc = await _context.InBoundCheckDocument.FromSqlRaw("SELECT * FROM InBoundCheckDocument WHERE InRegNo=@id AND DocCode=@doc And CheckStatus=false", new SqlParameter("@id", info.InRegNo), new SqlParameter("@doc", info.DocCode)).SingleOrDefaultAsync();
                if (doc == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found!";
                    return msg;
                }
                else
                {
                    doc.CheckStatus = true;
                    doc.UpdatedDate = GetLocalStdDT();
                    doc.UpdatedUser = info.UpdatedUser;
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfully updated!";
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.Status = false;
                msg.MessageContent += e.Message;
                return msg;
            }
        }

        public async Task<ResponseMessage> DeleteInBoundCheckDocument(int regNo,int docCode)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                InBoundCheckDocument?item = await _context.InBoundCheckDocument.FromSqlRaw("SELECT * FROM InBoundCheckDocument WHERE InRegNo=@id AND DocCode=@doc", new SqlParameter("@id", regNo), new SqlParameter("@doc", docCode)).SingleOrDefaultAsync();
                if (item == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found.";
                    return msg;
                }
                else
                {
                    _context.InBoundCheckDocument.Remove(item);
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Removed successfully!";
                    return msg;
                }
            }
            catch (DbUpdateException e)
            {
                msg.Status = false;
                msg.MessageContent += e.Message;
                return msg;
            }
        }

        #endregion
    }
}
