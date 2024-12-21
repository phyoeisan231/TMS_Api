using AutoMapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TMS_Api.Services
{
    public class ReportQueryDAL
    {
        private readonly TMSDBContext _context;
        private readonly IConfiguration _configuration;
        string _conStr;
        private readonly IMapper _mapper;

        public ReportQueryDAL(TMSDBContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
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

        #region Truck in yard

        public async Task<DataTable> GetTruckInCheckList(string yard)
        {
            string sql = @"SELECT InRegNo,InYardID,InGateID,InPCCode,InType,InCargoType,InCargoInfo,
                            convert(varchar,InGatePassTime,29) as InGatePassTime,convert(varchar,OutCheckDateTime,29) as OutCheckDateTime,
                            convert(varchar,OutGatePassTime,29) as OutGatePassTime,convert(varchar,InCheckDateTime,29) as InCheckDateTime,
                            AreaID,TruckType,TruckVehicleRegNo,TrailerVehicleRegNo,DriverLicenseNo,DriverName,DriverContactNo,CardNo,Status,
                            TransporterID,TransporterName,Customer,OutRegNo,OutYardID,OutGateID,OutPCCode,OutType,OutCargoType,OutCargoInfo,
                            InWeightBridgeID,OutWeightBridgeID,GroupName 
                            FROM ICD_TruckProcess 
                            where InYardID=@yard And Status='In(Check)' Order by InRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", yard));
            return dt;
        }
        public async Task<DataTable> GetTruckInList(string yard)
        {
            string sql = @"SELECT InRegNo,InYardID,InGateID,InPCCode,InType,InCargoType,InCargoInfo,
                            convert(varchar,InGatePassTime,29) as InGatePassTime,convert(varchar,OutCheckDateTime,29) as OutCheckDateTime,
                            convert(varchar,OutGatePassTime,29) as OutGatePassTime,convert(varchar,InCheckDateTime,29) as InCheckDateTime,
                            AreaID,TruckType,TruckVehicleRegNo,TrailerVehicleRegNo,DriverLicenseNo,DriverName,DriverContactNo,CardNo,Status,
                            TransporterID,TransporterName,Customer,OutRegNo,OutYardID,OutGateID,OutPCCode,OutType,OutCargoType,OutCargoInfo,
                            InWeightBridgeID,OutWeightBridgeID,GroupName 
                            FROM ICD_TruckProcess 
                            where InYardID=@yard And Status='In' Order by InRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql,new SqlParameter("@yard", yard));
            return dt;
        }
        public async Task<DataTable> GetTruckInWeightList(string yard)
        {
            string sql = @"SELECT InRegNo,InYardID,InGateID,InPCCode,InType,InCargoType,InCargoInfo,
                            convert(varchar,InGatePassTime,29) as InGatePassTime,convert(varchar,OutCheckDateTime,29) as OutCheckDateTime,
                            convert(varchar,OutGatePassTime,29) as OutGatePassTime,convert(varchar,InCheckDateTime,29) as InCheckDateTime,
                            AreaID,TruckType,TruckVehicleRegNo,TrailerVehicleRegNo,DriverLicenseNo,DriverName,DriverContactNo,CardNo,Status,
                            TransporterID,TransporterName,Customer,OutRegNo,OutYardID,OutGateID,OutPCCode,OutType,OutCargoType,OutCargoInfo,
                            InWeightBridgeID,OutWeightBridgeID,GroupName 
                            FROM ICD_TruckProcess 
                            where InYardID=@yard And Status='In(Weight)' Order by InRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql,new SqlParameter("@yard", yard));
            return dt;
        }
        public async Task<DataTable> GetTruckOperationList(string yard)
        {
            string sql = @"SELECT InRegNo,InYardID,InGateID,InPCCode,InType,InCargoType,InCargoInfo,
                            convert(varchar,InGatePassTime,29) as InGatePassTime,convert(varchar,OutCheckDateTime,29) as OutCheckDateTime,
                            convert(varchar,OutGatePassTime,29) as OutGatePassTime,convert(varchar,InCheckDateTime,29) as InCheckDateTime,
                            AreaID,TruckType,TruckVehicleRegNo,TrailerVehicleRegNo,DriverLicenseNo,DriverName,DriverContactNo,CardNo,Status,
                            TransporterID,TransporterName,Customer,OutRegNo,OutYardID,OutGateID,OutPCCode,OutType,OutCargoType,OutCargoInfo,
                            InWeightBridgeID,OutWeightBridgeID,GroupName 
                            FROM ICD_TruckProcess 
                            where InYardID=@yard And Status='Operation' Order by InRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql,new SqlParameter("@yard", yard));
            return dt;
        }
        public async Task<DataTable> GetTruckOutWeightList(string yard)
        {
            string sql = @"SELECT InRegNo,InYardID,InGateID,InPCCode,InType,InCargoType,InCargoInfo,
                            convert(varchar,InGatePassTime,29) as InGatePassTime,convert(varchar,OutCheckDateTime,29) as OutCheckDateTime,
                            convert(varchar,OutGatePassTime,29) as OutGatePassTime,convert(varchar,InCheckDateTime,29) as InCheckDateTime,
                            AreaID,TruckType,TruckVehicleRegNo,TrailerVehicleRegNo,DriverLicenseNo,DriverName,DriverContactNo,CardNo,Status,
                            TransporterID,TransporterName,Customer,OutRegNo,OutYardID,OutGateID,OutPCCode,OutType,OutCargoType,OutCargoInfo,
                            InWeightBridgeID,OutWeightBridgeID,GroupName 
                            FROM ICD_TruckProcess 
                            where InYardID=@yard And Status='Out(Weight)' Order by InRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", yard));
            return dt;
        }
        public async Task<DataTable> GetTruckOutCheckList(string yard)
        {
            string sql = @"SELECT InRegNo,InYardID,InGateID,InPCCode,InType,InCargoType,InCargoInfo,
                            convert(varchar,InGatePassTime,29) as InGatePassTime,convert(varchar,OutCheckDateTime,29) as OutCheckDateTime,
                            convert(varchar,OutGatePassTime,29) as OutGatePassTime,convert(varchar,InCheckDateTime,29) as InCheckDateTime,
                            AreaID,TruckType,TruckVehicleRegNo,TrailerVehicleRegNo,DriverLicenseNo,DriverName,DriverContactNo,CardNo,Status,
                            TransporterID,TransporterName,Customer,OutRegNo,OutYardID,OutGateID,OutPCCode,OutType,OutCargoType,OutCargoInfo,
                            InWeightBridgeID,OutWeightBridgeID,GroupName 
                            FROM ICD_TruckProcess 
                            where InYardID=@yard And Status='Out(Check)' Order by InRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql,new SqlParameter("@yard", yard));
            return dt;
        }
        public async Task<DataTable> GetTruckOutList(string yard)
        {
            string sql = @"SELECT InRegNo,InYardID,InGateID,InPCCode,InType,InCargoType,InCargoInfo,
                            convert(varchar,InGatePassTime,29) as InGatePassTime,convert(varchar,OutCheckDateTime,29) as OutCheckDateTime,
                            convert(varchar,OutGatePassTime,29) as OutGatePassTime,convert(varchar,InCheckDateTime,29) as InCheckDateTime,
                            AreaID,TruckType,TruckVehicleRegNo,TrailerVehicleRegNo,DriverLicenseNo,DriverName,DriverContactNo,CardNo,Status,
                            TransporterID,TransporterName,Customer,OutRegNo,OutYardID,OutGateID,OutPCCode,OutType,OutCargoType,OutCargoInfo,
                            InWeightBridgeID,OutWeightBridgeID,GroupName 
                            FROM ICD_TruckProcess 
                            where InYardID=@yard And Status='Out' Order by InRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", yard));
            return dt;
        }

        #endregion


    }

}
