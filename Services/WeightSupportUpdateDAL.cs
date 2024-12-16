using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TMS_Api.DBModels;
using TMS_Api.DTOs;

namespace TMS_Api.Services
{
    public class WeightSupportUpdateDAL
    {

        private readonly TMSDBContext _context;
        private readonly IConfiguration _configuration;
        public string _conStr { get; }
        private readonly IMapper _mapper;


        public WeightSupportUpdateDAL(TMSDBContext context, IConfiguration config, IMapper mapper)
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

        #region WeightServiceBill 14_Dec_2024
        public async Task<ResponseMessage> SaveWeightServiceBill(WeightServiceBillDto info)
        {
            ResponseMessage msg = new ResponseMessage { Status = false };
            try
            {
                //string code = info.WeightBridgeID + "-" + "E" + GetLocalStdDT().ToString("ddMMyy") + "-";
                WeightServiceBill wserviceBill = await _context.WeightServiceBill.FromSqlRaw("SELECT Top 1 * FROM WeightServiceBill WHERE ServiceBillNo LIKE '" + info.WeightBridgeID + "%' ORDER BY ServiceBillNo DESC").SingleOrDefaultAsync();
                if (wserviceBill != null)
                {
                    int num = Convert.ToInt32(wserviceBill.ServiceBillNo[^6..]);
                    num++;
                    info.ServiceBillNo = info.WeightBridgeID + num.ToString("D3");
                }
                else
                {
                    info.ServiceBillNo = info.WeightBridgeID + "000001";
                }


                WeightServiceBill sBill = _mapper.Map<WeightServiceBill>(info);
                sBill.CreatedDate = GetLocalStdDT();
                _context.WeightServiceBill.Add(sBill);
                await _context.SaveChangesAsync();
                msg.Status = true;
                msg.MessageContent = "Successfully created!";


            }
            catch (DbUpdateException e)
            {
                msg.MessageContent += e.Message;
                return msg;
            }

            return msg;
        }

        #endregion
    }
}
