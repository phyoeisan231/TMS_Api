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
        string _portalConStr;
        private readonly IMapper _mapper;

        public TMSOperationQueryDAL(IConfiguration config, TMSDBContext context, IMapper mapper, ILogger<TMSOperationQueryDAL> logger)
        {
            _context = context;
            _configuration = config;
            _conStr = _configuration.GetConnectionString("DefaultConnection");
            _portalConStr = _configuration.GetConnectionString("PortalConnection");
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

        public Task<DataTable> GetPortalDataTableAsync(string sSQL, params SqlParameter[] para)
        {
            return Task.Run(() =>
            {
                using (var newCon = new SqlConnection(_portalConStr))
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

        #region ICD/Other InBound Check Nov_27_2024

        public async Task<DataTable> GetCategoryInList(string type)
        {
            string sql = @"Select PCCode,CategoryName,GroupName from PCategory where  GroupName=@type And Active=1 And (InboundWeight=1 Or InboundWeight is null)";
            DataTable dt = await GetDataTableAsync(sql,new SqlParameter("@type",type));
            return dt;
        }
        public async Task<DataTable> GetGateInBoundList(string id)
        {
            string sql = @"Select GateID,Name,YardID from Gate where YardID=@yard And Type in('InBound','Both') And Active=1";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", id));
            return dt;
        }
        public async Task<DataTable> GetOperationAreaList(string id,string gpName)
        {
            string sql = @"Select AreaID,Name,YardID from OperationArea where YardID=@yard And Active=1 And (IsWaitingArea=0 or IsWaitingArea is null) And GroupName=@gpName";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", id),new SqlParameter("@gpName",gpName));
            return dt;
        }
        public async Task<DataTable> GetCardList(string id,string gpName)
        {
            string sql = @"SELECT CardNo,GroupName,YardID from PCard Where GroupName=@gpName And Active=1 And (IsUse=0 or IsUse is null) And YardID=@yard";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", id),new SqlParameter("@gpName", gpName));
            return dt;
        }
        public async Task<DataTable> GetTruckList(string id, string type)
        {
            string sql = "";
            DataTable dt = new DataTable();
            if (type == "RGL")
            {
                sql = @"Select TruckNo as VehicleRegNo from Trucks where TruckNo like '%" + id + "%' And Status='Active' And TruckNo not in (select TruckVehicleRegNo as TruckNo from ICD_TruckProcess where Status<>'Out')";
                dt = await GetPortalDataTableAsync(sql);
            }
            else
            {
                sql = @"Select VehicleRegNo,ContainerType,ContainerSize,TypeID,TransporterID,DriverLicenseNo from Truck where VehicleRegNo like '%" + id + "%' And (IsBlack<>1 OR IsBlack is null) And Active=1 And VehicleRegNo not in (select TruckVehicleRegNo as VehicleRegNo from ICD_TruckProcess where Status<>'Out')";
                dt = await GetDataTableAsync(sql);
            }           
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
           string sql = @"SELECT i.InRegNo,i.InYardID,i.InGateID,i.InPCCode,i.InType,i.InCargoType,i.InCargoInfo,convert(varchar, i.InCheckDateTime, 29) as InCheckDateTime,i.AreaID,i.TruckType,i.TruckVehicleRegNo,i.TrailerVehicleRegNo,i.DriverLicenseNo,i.DriverName,i.CardNo,i.TransporterID,i.TransporterName,
				        t.Status,i.Remark,i.InWeightBridgeID,i.OutWeightBridgeID,i.Customer,i.DriverContactNo,i.InWBBillOption,i.OutWBBillOption,i.IsUseWB,i.GroupName FROM ICD_InBoundCheck i
				        Left Join ICD_TruckProcess t On t.InRegNo=i.InRegNo where i.InYardID=@yard And i.GroupName <>'TMS' And Cast(i.InCheckDateTime as Date) Between @sDate and @eDate  Order by i.InRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@sDate", startDate), new SqlParameter("@eDate", endDate), new SqlParameter("@yard", yard));
            return dt;
        }


        #region New ICD/Other InBound Check
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

        #region ICD/Other Out Check Dec_9_2024
        public async Task<DataTable> GetCardICDOInList(string card,string id)
        {
            string sql = @"SELECT CardNo,TruckVehicleRegNo,DriverLicenseNo,DriverContactNo,DriverName,TrailerVehicleRegNo,Customer,InCargoInfo,InCargoType,OutWeightBridgeID,InRegNo,IsUseWB,OutWeightBridgeID,TruckType,AreaID,GroupName  from ICD_TruckProcess Where  Status='In' And GroupName<>'TMS' And InYardID=@yard And CardNo like '%" + card + "%'";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", id));
            return dt;
        }

        public async Task<DataTable> GetGateOutBoundList(string id)
        {
            string sql = @"Select GateID,Name,YardID from Gate where YardID=@yard And Type in('OutBound','Both') And Active=1";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", id));
            return dt;
        }

        public async Task<DataTable> GetCategoryOutList(string type)
        {
            string sql = @"Select PCCode,CategoryName,GroupName from PCategory where  GroupName=@type And Active=1 And (OutboundWeight=1 Or OutboundWeight is null)";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@type", type));
            return dt;
        }      

        public async Task<DataTable> GetDocumentSettingOutList(string id)
        {
            string sql = @"SELECT DocCode,DocName,'' as CheckStatus FROM DocumentSetting WHERE PCCode=@id And IsOutDoc=1 And Active=1";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@id", id));
            return dt;
        }

        public async Task<ICD_OutBoundCheckDto> GetOutBoundCheckById(int id)
        {
            ICD_OutBoundCheckDto info = new ICD_OutBoundCheckDto();
            try
            {
                ICD_OutBoundCheck? data = await _context.ICD_OutBoundCheck.FromSqlRaw("SELECT * FROM  ICD_OutBoundCheck WHERE OutRegNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (data != null)
                {
                    info = _mapper.Map<ICD_OutBoundCheckDto>(data);

                    List<ICD_OutBoundCheck_Document> documentList = await _context.ICD_OutBoundCheck_Document.FromSqlRaw("SELECT * FROM ICD_OutBoundCheck_Document WHERE OutRegNo=@id", new SqlParameter("@id", id)).ToListAsync();

                    info.DocumentList = new List<ICD_OutBoundCheck_DocumentDto>();
                    foreach (var d in documentList)
                    {

                        ICD_OutBoundCheck_DocumentDto docDto = _mapper.Map<ICD_OutBoundCheck_DocumentDto>(d);
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

        public async Task<DataTable> GetOutBoundCheckList(DateTime startDate, DateTime endDate, string yard)
        {
            string sql = @"SELECT o.OutRegNo,o.OutYardID,o.OutGateID,o.OutPCCode,o.OutType,o.OutCargoType,o.OutCargoInfo,convert(varchar, o.OutCheckDateTime, 29) as OutCheckDateTime,o.AreaID,o.TruckType,o.TruckVehicleRegNo,o.TrailerVehicleRegNo,o.DriverLicenseNo,o.DriverName,o.CardNo,o.TransporterID,o.TransporterName,t.Status,o.Remark,o.OutWeightBridgeID,o.Customer,o.DriverContactNo,o.GroupName  FROM ICD_OutBoundCheck o
				            Left Join ICD_TruckProcess t On t.OutRegNo=o.OutRegNo
				            where o.OutYardID=@yard And o.GroupName<>'TMS' And Cast(o.OutCheckDateTime as Date) Between @sDate and @eDate  Order by o.OutRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@sDate", startDate), new SqlParameter("@eDate", endDate), new SqlParameter("@yard", yard));
            return dt;
        }

        #endregion

        #region ICD/Other Truck Status Dec_16_12_2024
        public async Task<DataTable> GetTruckProcessList(DateTime startDate, DateTime endDate, string status, string yard)
        {
            string sql = @"SELECT InRegNo,InYardID,InGateID,InPCCode,InType,InCargoType,InCargoInfo,convert(varchar,OutWeightDateTime,29) as OutWeightDateTime,convert(varchar,InWeightDateTime,29) as InWeightDateTime,convert(varchar,InGatePassTime,29) as InGatePassTime,convert(varchar,OutCheckDateTime,29) as OutCheckDateTime,convert(varchar,OutGatePassTime,29) as OutGatePassTime,convert(varchar,InCheckDateTime,29) as InCheckDateTime,AreaID,TruckType,TruckVehicleRegNo,TrailerVehicleRegNo,DriverLicenseNo,DriverName,DriverContactNo,CardNo,Status,TransporterID,TransporterName,Customer,OutRegNo,OutYardID,OutGateID,OutPCCode,OutType,OutCargoType,OutCargoInfo,InWeightBridgeID,OutWeightBridgeID,GroupName FROM ICD_TruckProcess where Status in (" + status + ") And InYardID in (" + yard + ") And Cast(InCheckDateTime as Date) Between @sDate and @eDate Order by InRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@sDate", startDate), new SqlParameter("@eDate", endDate));
            return dt;
        }
        #endregion

        #region TMS In Check Dec_17_2024
       
        public async Task<DataTable> GetTMSProposalList(DateTime startDate, DateTime endDate, string yard,string deptType)
        {
            string sql = @"SELECT PropNo,Yard,convert(varchar, EstDate, 29) as EstDate,JobDept,JobCode,JobType,CompanyName,NoOfTruck,NoOfTEU,LCLQty,CargoInfo,CustomerId,CustomerName from TMS_Proposal  where Yard=@yard And JobDept in (" + deptType + ") And Cast(EstDate as Date) Between @sDate and @eDate And Status='Open'  Order by PropNo DESC";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@sDate", startDate), new SqlParameter("@eDate", endDate), new SqlParameter("@yard", yard));
            return dt;
        }

        public async Task<TMS_ProposalDto> GetTMSProposalById(int id)
        {
            TMS_ProposalDto info = new TMS_ProposalDto();
            try
            {
                TMS_Proposal? data = await _context.TMS_Proposal.FromSqlRaw("SELECT * FROM  TMS_Proposal WHERE PropNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (data != null)
                {
                    info = _mapper.Map<TMS_ProposalDto>(data);
                    info.Customer = data.CustomerName;
                    info.InYardID = data.Yard;
                    List<TMS_ProposalDetail> detailList = await _context.TMS_ProposalDetails.FromSqlRaw("SELECT * FROM TMS_ProposalDetails WHERE PropNo=@id", new SqlParameter("@id", id)).ToListAsync();
                    info.DetailList = new List<TMS_ProposalDetailDto>();
                    foreach (var d in detailList)
                    {
                        TMS_ProposalDetailDto docDto = _mapper.Map<TMS_ProposalDetailDto>(d);
                        info.DetailList.Add(docDto);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return info;
        }

        public async Task<DataTable> GetTruckDataListByProposal(string id, int poNo)
        {
            string sql = "";
            DataTable dt = new DataTable();
            sql = @"Select TruckNo as VehicleRegNo,'' as DriverLicenseNo from TMS_ProposalDetails where TruckNo like '%" + id + "%' And PropNo=@poNo And TruckNo not in (select TruckVehicleRegNo as TruckNo from ICD_TruckProcess where Status<>'Out')";
            dt = await GetDataTableAsync(sql,new SqlParameter("@poNo",poNo));
            if (dt.Rows.Count==0){
                sql = @"Select VehicleRegNo,ContainerType,ContainerSize,TypeID,TransporterID,DriverLicenseNo from Truck where VehicleRegNo like '%" + id + "%' And (IsBlack<>1 OR IsBlack is null) And Active=1 And VehicleRegNo not in (select TruckVehicleRegNo as VehicleRegNo from ICD_TruckProcess where Status<>'Out')";
                dt = await GetDataTableAsync(sql);
            }          
            return dt;
        }

        public async Task<DataTable> GetInBoundCheckTMSList(DateTime startDate, DateTime endDate, string yard)
        {
            string sql = @"SELECT i.InRegNo,i.InYardID,i.InGateID,i.InPCCode,i.InType,i.InCargoType,i.InCargoInfo,convert(varchar, i.InCheckDateTime, 29) as InCheckDateTime,i.AreaID,i.TruckType,i.TruckVehicleRegNo,i.TrailerVehicleRegNo,i.DriverLicenseNo,i.DriverName,i.CardNo,i.TransporterID,i.TransporterName,
				        t.Status,i.Remark,i.InWeightBridgeID,i.OutWeightBridgeID,i.Customer,i.DriverContactNo,i.InWBBillOption,i.OutWBBillOption,i.IsUseWB,i.GroupName,i.PropNo FROM ICD_InBoundCheck i
				        Left Join ICD_TruckProcess t On t.InRegNo=i.InRegNo where i.InYardID=@yard And i.GroupName='TMS' And Cast(i.InCheckDateTime as Date) Between @sDate and @eDate  Order by i.InRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@sDate", startDate), new SqlParameter("@eDate", endDate), new SqlParameter("@yard", yard));
            return dt;
        }

        #endregion

        #region TMS Out Check Dec_18_2024

        public async Task<DataTable> GetCardTMSInList(string card, string id)
        {
            //string sql = @"SELECT CardNo,TruckVehicleRegNo,DriverLicenseNo,DriverContactNo,DriverName,TrailerVehicleRegNo,Customer,InCargoInfo,InCargoType,OutWeightBridgeID,InRegNo,IsUseWB,OutWeightBridgeID,TruckType,AreaID,GroupName  from ICD_TruckProcess Where  Status='In' And GroupName='TMS' And InYardID=@yard And CardNo like '%" + card + "%'";
            string sql = @"SELECT *  from ICD_TruckProcess Where  Status='In' And GroupName='TMS' And InYardID=@yard And CardNo like '%" + card + "%'";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@yard", id));
            return dt;
        }
        public async Task<DataTable> GetOutBoundCheckTMSList(DateTime startDate, DateTime endDate, string yard)
        {
            string sql = @"SELECT o.OutRegNo,o.OutYardID,o.OutGateID,o.OutPCCode,o.OutType,o.OutCargoType,o.OutCargoInfo,convert(varchar, o.OutCheckDateTime, 29) as OutCheckDateTime,o.AreaID,o.TruckType,o.TruckVehicleRegNo,o.TrailerVehicleRegNo,o.DriverLicenseNo,o.DriverName,o.CardNo,o.TransporterID,o.TransporterName,t.Status,o.Remark,o.OutWeightBridgeID,o.Customer,o.DriverContactNo,o.GroupName  FROM ICD_OutBoundCheck o
				            Left Join ICD_TruckProcess t On t.OutRegNo=o.OutRegNo
				            where o.OutYardID=@yard And o.GroupName='TMS' And Cast(o.OutCheckDateTime as Date) Between @sDate and @eDate  Order by o.OutRegNo DESC";
            DataTable dt = await GetDataTableAsync(sql, new SqlParameter("@sDate", startDate), new SqlParameter("@eDate", endDate), new SqlParameter("@yard", yard));
            return dt;
        }
        #endregion

    }
}
