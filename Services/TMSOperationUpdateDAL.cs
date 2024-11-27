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
        //#region PCard Nov_27_2024
        //public async Task<ResponseMessage> SavePCard(PCardDto info)
        //{
        //    ResponseMessage msg = new ResponseMessage { Status = false };
        //    try
        //    {
        //        PCard card = await _context.PCard.FromSqlRaw("SELECT Top 1* FROM PCard WHERE YardID=@yid And GroupName=@gname Order By CardNo Desc", new SqlParameter("@yid", info.YardID), new SqlParameter("@gname", info.GroupName)).SingleOrDefaultAsync();
        //        int srNo = 1;
        //        string id = string.IsNullOrEmpty(info.YardID) ? "" : info.YardID + "-";
        //        string sg = string.IsNullOrEmpty(info.GroupName) ? "" : info.GroupName.Substring(0, 1) + "-";
        //        id += sg;
        //        if (card != null)
        //        {                  
        //            srNo = int.Parse(card.CardNo.Substring(id.Length))+1;
        //            id += srNo.ToString("000");
        //        }
        //        else
        //        {
        //            id+= srNo.ToString("000");
        //        }
        //        info.CardNo = id;
        //        PCard data = _mapper.Map<PCard>(info);
        //        data.CreatedDate = GetLocalStdDT();
        //        data.CreatedUser = info.CreatedUser;
        //        _context.PCard.Add(data);
        //        await _context.SaveChangesAsync();
        //        msg.Status = true;
        //        msg.MessageContent = "Successfully added";
        //    }
        //    catch (DbUpdateException e)
        //    {
        //        msg.MessageContent += e.Message;
        //        return msg;
        //    }
        //    return msg;
        //}
        //#endregion
    }
}
