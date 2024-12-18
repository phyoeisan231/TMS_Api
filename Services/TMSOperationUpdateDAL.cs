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

        #region  InBound Check Nov_27_2024
        public async Task<ResponseMessage> DeleteInCheck(int id, string user)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                ICD_InBoundCheck? data = await _context.ICD_InBoundCheck.FromSqlRaw("SELECT * FROM ICD_InBoundCheck WHERE InRegNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (data == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found.";
                    return msg;
                }
                else
                {
                    ICD_TruckProcess? process = await _context.ICD_TruckProcess.FromSqlRaw("SELECT * FROM ICD_TruckProcess WHERE InRegNo=@id And Status='In(Check)'", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                    if (process == null)
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data is used by another process!";
                        return msg;
                    }
                    _context.ICD_TruckProcess.Remove(process);

                    _context.ICD_InBoundCheck.Remove(data);
                    List<ICD_InBoundCheck_Document> detailList = await _context.ICD_InBoundCheck_Document.FromSqlRaw("SELECT * FROM ICD_InBoundCheck_Document WHERE InRegNo=@id", new SqlParameter("@id", id)).ToListAsync();
                    if (detailList.Count > 0)
                    {
                        _context.ICD_InBoundCheck_Document.RemoveRange(detailList);
                    }
                    if (!string.IsNullOrEmpty(data.CardNo))
                    {
                        PCard? card = await _context.PCard.FromSqlRaw("SELECT * FROM PCard WHERE CardNo=@id", new SqlParameter("@id", data.CardNo)).SingleOrDefaultAsync();
                        if (card == null)
                        {
                            msg.Status = false;
                            msg.MessageContent = "PCard Data not found!";
                            return msg;
                        }
                        card.IsUse = false;
                        card.VehicleRegNo = null;
                        card.UpdatedUser = user;
                        card.UpdatedDate = GetLocalStdDT();
                    }

                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Removed successfully!";
                    return msg;
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                msg.Status = false;
                msg.MessageContent += e.Message;
                return msg;
            }

        }
        public async Task<ResponseMessage> SaveInCheck(ICD_InBoundCheckDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ICD_InBoundCheck inBound = _mapper.Map<ICD_InBoundCheck>(info);
                    inBound.CreatedDate = GetLocalStdDT();
                    inBound.Status = true;
                    _context.ICD_InBoundCheck.Add(inBound);
                    await _context.SaveChangesAsync();
                    if (info.DocumentList != null)
                    {
                        foreach (var i in info.DocumentList)
                        {
                            ICD_InBoundCheck_Document doc = new ICD_InBoundCheck_Document();
                            doc.CheckStatus = true;
                            doc.DocName = i.DocName;
                            doc.DocCode = i.DocCode;
                            doc.InRegNo = inBound.InRegNo;
                            doc.CreatedDate = GetLocalStdDT();
                            doc.CreatedUser = info.CreatedUser;
                            _context.ICD_InBoundCheck_Document.Add(doc);
                        }
                    }
                    PCard? card = await _context.PCard.FromSqlRaw("SELECT * FROM PCard WHERE CardNo=@id", new SqlParameter("@id", info.CardNo)).SingleOrDefaultAsync();
                    if (card == null)
                    {
                        // Rollback the transaction if any exception occurs
                        await transaction.RollbackAsync();
                        msg.Status = false;
                        msg.MessageContent = "PCard Data not found!";
                        return msg;
                    }

                    card.IsUse = true;
                    card.UpdatedUser = info.UpdatedUser;
                    card.UpdatedDate = GetLocalStdDT();
                    card.VehicleRegNo = info.TruckVehicleRegNo;
                    card.CardIssueDate = GetLocalStdDT();

                    ICD_TruckProcess? process = await _context.ICD_TruckProcess.FromSqlRaw("SELECT * FROM ICD_TruckProcess WHERE InRegNo=@id", new SqlParameter("@id", inBound.InRegNo)).SingleOrDefaultAsync();
                    if (process == null)
                    {
                        ICD_TruckProcess truck = new ICD_TruckProcess();
                        truck = _mapper.Map<ICD_TruckProcess>(inBound);
                        truck.InRegNo = inBound.InRegNo;
                        truck.CreatedDate = GetLocalStdDT();
                        truck.CreatedUser = info.CreatedUser;
                        truck.Status = "In(Check)";
                        truck.InYard = false;
                        _context.ICD_TruckProcess.Add(truck);
                    }
                    if (info.PropNo != null)
                    {
                        TMS_Proposal? proposal = await _context.TMS_Proposal.FromSqlRaw("SELECT * FROM TMS_Proposal WHERE PropNo=@id And Status='Open'", new SqlParameter("@id", info.PropNo)).SingleOrDefaultAsync();
                        if (proposal != null)
                        {
                            proposal.UpdatedDate = GetLocalStdDT();
                            proposal.UpdatedUser = info.CreatedUser;
                        }
                        else
                        {
                            // Rollback the transaction if any exception occurs
                            await transaction.RollbackAsync();
                            msg.Status = false;
                            msg.MessageContent = "TMS_Proposal Data not found!";
                            return msg;
                        }
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfuly added!";
                }
                catch (DbUpdateConcurrencyException e)
                {
                    // Rollback the transaction if any exception occurs
                    await transaction.RollbackAsync();
                    msg.MessageContent += e.Message;
                    return msg;
                }
            }
            return msg;
        }


        #endregion

        #region OutBound Check Dec_9_2024
        public async Task<ResponseMessage> DeleteOutCheck(int id, string user)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                ICD_OutBoundCheck? data = await _context.ICD_OutBoundCheck.FromSqlRaw("SELECT * FROM ICD_OutBoundCheck WHERE OutRegNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (data == null)
                {
                    msg.Status = false;
                    msg.MessageContent = "Data not found.";
                    return msg;
                }
                else
                {
                    ICD_TruckProcess? process = await _context.ICD_TruckProcess.FromSqlRaw("SELECT * FROM ICD_TruckProcess WHERE OutRegNo=@id And Status='Out(Check)'", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                    if (process == null)
                    {
                        msg.Status = false;
                        msg.MessageContent = "Data is used by another process!";
                        return msg;
                    }
                    process.OutRegNo = null;
                    process.UpdatedDate = GetLocalStdDT();
                    process.UpdatedUser = user;
                    process.OutCargoInfo = null;
                    process.OutCargoType = null;
                    process.OutPCCode = null;
                    process.OutYardID = null;
                    process.OutGateID = null;
                    process.Status = "In";
                    _context.ICD_OutBoundCheck.Remove(data);
                    List<ICD_OutBoundCheck_Document> detailList = await _context.ICD_OutBoundCheck_Document.FromSqlRaw("SELECT * FROM ICD_OutBoundCheck_Document WHERE OutRegNo=@id", new SqlParameter("@id", id)).ToListAsync();
                    if (detailList.Count > 0)
                    {
                        _context.ICD_OutBoundCheck_Document.RemoveRange(detailList);
                    }
                   
                    await _context.SaveChangesAsync();
                    msg.Status = true;
                    msg.MessageContent = "Removed successfully!";
                    return msg;
                }
            }
            catch (DbUpdateConcurrencyException e)
            {
                msg.Status = false;
                msg.MessageContent += e.Message;
                return msg;
            }

        }
        public async Task<ResponseMessage> SaveOutCheck(ICD_OutBoundCheckDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ICD_OutBoundCheck outBound = _mapper.Map<ICD_OutBoundCheck>(info);
                    outBound.CreatedDate = GetLocalStdDT();
                    outBound.Status = true;
                    _context.ICD_OutBoundCheck.Add(outBound);
                    await _context.SaveChangesAsync();
                    if (info.DocumentList != null)
                    {
                        foreach (var i in info.DocumentList)
                        {
                            ICD_OutBoundCheck_Document doc = new ICD_OutBoundCheck_Document();
                            doc.CheckStatus = true;
                            doc.DocName = i.DocName;
                            doc.DocCode = i.DocCode;
                            doc.OutRegNo = outBound.OutRegNo;
                            doc.CreatedDate = GetLocalStdDT();
                            doc.CreatedUser = info.CreatedUser;
                            _context.ICD_OutBoundCheck_Document.Add(doc);
                        }
                    }
                    PCard? card = await _context.PCard.FromSqlRaw("SELECT * FROM PCard WHERE CardNo=@id", new SqlParameter("@id", info.CardNo)).SingleOrDefaultAsync();
                    if (card == null)
                    {
                        // Rollback the transaction if any exception occurs
                        await transaction.RollbackAsync();
                        msg.Status = false;
                        msg.MessageContent = "PCard Data not found!";
                        return msg;
                    }

                    card.UpdatedUser = info.UpdatedUser;
                    card.UpdatedDate = GetLocalStdDT();

                    ICD_TruckProcess? process = await _context.ICD_TruckProcess.FromSqlRaw("SELECT * FROM ICD_TruckProcess WHERE InRegNo=@id And Status='In'", new SqlParameter("@id", info.InRegNo)).SingleOrDefaultAsync();
                    if (process==null){
                        // Rollback the transaction if any exception occurs
                        await transaction.RollbackAsync();
                        msg.Status = false;
                        msg.MessageContent = "Truck Process In Data not found!";
                        return msg;
                    }
                    if (process != null)
                    {
                        process.OutCheckDateTime = outBound.OutCheckDateTime;
                        process.OutRegNo = outBound.OutRegNo;
                        process.OutCargoInfo = info.OutCargoInfo;
                        process.OutCargoType = info.OutCargoType;
                        process.OutWeightBridgeID = info.OutWeightBridgeID;
                        process.OutPCCode = info.OutPCCode;
                        process.OutYardID = info.OutYardID;
                        process.OutGateID = info.OutGateID;
                        process.OutType = info.OutType;
                        process.UpdatedDate = GetLocalStdDT();
                        process.UpdatedUser = info.CreatedUser;
                        process.Status = "Out(Check)";

                        if (process.PropNo != null)
                        {
                            TMS_Proposal? proposal = await _context.TMS_Proposal.FromSqlRaw("SELECT * FROM TMS_Proposal WHERE PropNo=@id", new SqlParameter("@id", process.PropNo)).SingleOrDefaultAsync();
                            if (proposal != null)
                            {
                                proposal.UpdatedDate = GetLocalStdDT();
                                proposal.UpdatedUser = info.CreatedUser;
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    msg.Status = true;
                    msg.MessageContent = "Successfuly added!";
                }
                catch (DbUpdateConcurrencyException e)
                {
                    // Rollback the transaction if any exception occurs
                    await transaction.RollbackAsync();
                    msg.MessageContent += e.Message;
                    return msg;
                }
            }
            return msg;
        }
        #endregion
    }
}
