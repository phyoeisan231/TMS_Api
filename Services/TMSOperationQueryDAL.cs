﻿using AutoMapper;
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

        public async Task<DataTable> GetCategoryICDOList()
        {
            string sql = @"Select PCCode,CategoryName,GroupName from PCategory where  GroupName in ('ICD','Others')  And Active=1";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }
        public async Task<DataTable> GetGateInBoundList(string id)
        {
            string sql = @"Select GateID,Name,YardID from Gate where YardID=@yard And Type in('InBound','Both') And Active=1";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", id));
            return dt;
        }
        public async Task<DataTable> GetOperationAreaList(string id)
        {
            string sql = @"Select AreaID,Name,YardID from OperationArea where YardID=@yard And Active=1 And (IsWaitingArea=0 or IsWaitingArea is null)";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", id));
            return dt;
        }
        public async Task<DataTable> GetCardICDList(string id)
        {
            string sql = @"SELECT CardNo,GroupName,YardID from PCard Where GroupName in('ICD','Other') And Active=1 And (IsUse=0 or IsUse is null) And YardID=@yard";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", id));
            return dt;
        }
        public async Task<DataTable> GetTruckList(string id, string type)
        {
            string sql = "";
            if (type == "RG")
            {
                sql = @"Select VehicleRegNo,ContainerType,ContainerSize,TypeID,TransporterID,DriverLicenseNo from Truck where VehicleRegNo like '%" + id + "%' And IsRGL=1 And (IsBlack<>1 OR IsBlack is null) And Active=1 And VehicleRegNo not in (select TruckVehicleRegNo as VehicleRegNo from ICD_TruckProcess where Status<>'Out')";
            }
            else
            {
                sql = @"Select VehicleRegNo,ContainerType,ContainerSize,TypeID,TransporterID,DriverLicenseNo from Truck where VehicleRegNo like '%" + id + "%' And (IsRGL<>1 OR ISRGL is null) And (IsBlack<>1 OR IsBlack is null) And Active=1 And VehicleRegNo not in (select TruckVehicleRegNo as VehicleRegNo from ICD_TruckProcess where Status<>'Out')";
            }
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }
        public async Task<DataTable> GetDriverList(string id)
        {
            DateTime strDate = GetLocalStdDT();
            string sql = @"Select Name,LicenseNo,(LicenseNo +' | '+Name) as DriverName,LicenseExpiration,ContactNo from Driver where (IsBlack<>1 OR IsBlack is null) And Active=1 And LicenseNo not in (select DriverLicenseNo as LicenseNo from ICD_TruckProcess where Status<>'Out') And Cast(LicenseExpiration as Date)>=@eDate And LicenseNo like '%" + id + "%' ";
            DataTable dt = await GetDataTableAsync(sql,new SqlParameter("@eDate", strDate));
            return dt;
        }
        public async Task<DataTable> GetTrailerList()
        {
            string sql = @"Select VehicleRegNo,DriverLicenseNo,ContainerType,ContainerSize,TransporterID from Trailer where (IsBlack<>1 OR IsBlack is null) And Active=1 And VehicleRegNo not in (select TrailerVehicleRegNo as VehicleRegNo from ICD_TruckProcess where Status<>'Out')";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }
        public async Task<DataTable> GetTransporterList()
        {
            string sql = @"select TransporterID,TransporterName,(TransporterID +' | '+TransporterName) as Name from Transporter Where Active=1 And (IsBlack<>1 OR IsBlack is null)";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<DataTable> GetWBDataList(string id)
        {
            string sql = @"Select Name,WeightBridgeID,YardID from WeightBridge where YardID=@yard And Active=1";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", id));
            return dt;
        }
        
        public async Task<DataTable> GetInBoundCheckList(DateTime startDate, DateTime endDate,string yard)
        {          
           string sql = @"SELECT InRegNo,InYardID,InGateID,InPCCode,InType,InCargoType,InCargoInfo,convert(varchar, InCheckDateTime, 29) as InCheckDateTime,AreaID,TruckType,TruckVehicleRegNo,TrailerVehicleRegNo,DriverLicenseNo,DriverName,CardNo,TransporterID,TransporterName,Status,Remark,InWeightBridgeID,OutWeightBridgeID FROM ICD_InBoundCheck where InYardID in (" + yard + ") And Cast(InCheckDateTime as Date) Between @sDate and @eDate  Order by InRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@sDate", startDate), new SqlParameter("@eDate", endDate));
            return dt;
        }

        #region New
        public async Task<ICD_InBoundCheckDto> GetInBoundCheckById(int id)
        {
            ICD_InBoundCheckDto info = new ICD_InBoundCheckDto();
            try
            {
                ICD_InBoundCheck? data = await _context.ICD_InBoundCheck.FromSqlRaw("SELECT * FROM  ICD_InBoundCheck WHERE InRegNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
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

        public async Task<DataTable> GetDocumentSettingList(string id)
        {
            string sql = @"SELECT DocCode,DocName,'' as CheckStatus FROM DocumentSetting WHERE PCCode=@id And IsInDoc=1 And Active=1";
            DataTable dt = await GetDataTableAsync(sql,new SqlParameter("@id",id));
            return dt;
        }

        public async Task<DataTable> GetTrailerList(string searchedText)
        {
            string sql = $"SELECT VehicleRegNo FROM Trailer WHERE VehicleRegNo LIKE '%{searchedText}%'";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<DataTable> GetTruckDataList(string id)
        {
            string sql = $"SELECT * FROM ICD_TruckProcess WHERE InYard=1 AND TruckVehicleRegNo Like '%{id}%'";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }
        #endregion

        #endregion

    }
}
