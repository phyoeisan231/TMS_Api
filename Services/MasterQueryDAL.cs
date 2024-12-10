using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TMS_Api.DBModels;
using TMS_Api.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TMS_Api.Services
{
    public class MasterQueryDAL
    {
        private readonly TMSDBContext _context;
        private readonly IConfiguration _configuration;
        string _conStr;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<MasterQueryDAL> _logger;
        private readonly string _storageConnectionString;
        private readonly string _storageFormContainerName;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public MasterQueryDAL(IConfiguration config, TMSDBContext context, IMapper mapper, ILogger<MasterQueryDAL> logger, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = config;
            _conStr = _configuration.GetConnectionString("DefaultConnection");
            _storageConnectionString = _configuration.GetValue<string>("BlobConnectionString");
            _storageFormContainerName = _configuration.GetValue<string>("BlobContainerName");
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
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

        #region Truck Type Nov_11_2024
        public async Task<DataTable> GetTruckTypeList(string active)
        {
            string sql = "";
            if (active == "All" || active == null)
            {
                sql = @"SELECT * from TruckType Order By TypeID";
            }
            else
            {
                sql = @"SELECT * from TruckType where Active='" + active + "'";
            }
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        #endregion

        #region Transporter Nov_12_2024
        public async Task<DataTable> GetTransporterList(string active, string isBlack)
        {
            string sql = "";
            if ((active == "All" || active == null) && (isBlack == "All" || isBlack == null))
            {
                sql = @"SELECT * from Transporter Order By TransporterID";
            }
            else
            {
                sql = @"select TransporterID,TransporterName,(TransporterID +' | '+TransporterName) as Name from Transporter Where Active=1 And (IsBlack<>1 OR IsBlack is null)";
            }
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        public async Task<TransporterDto> GetTransporterId(string id)
        {
            TransporterDto transporterDto = new TransporterDto();
            try
            {
                Transporter data = await _context.Transporter.FromSqlRaw("SELECT * FROM Transporter WHERE TransporterID=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (data != null)
                {
                    transporterDto = _mapper.Map<TransporterDto>(data);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return transporterDto;
        }

        #endregion

        #region Gate Nov_12_2024
        public async Task<DataTable> GetGateList(string active)
        {
            string sql = "";
            if (active == "All" || active == null)
            {
                sql = @"SELECT * from Gate Order By GateID";
            }
            else
            {
                sql = @"SELECT * from Gate Where Active='" + active + "'";
            }
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }
        public async Task<GateDto> GetGateId(string id)
        {
            GateDto gateDto = new GateDto();
            try
            {

                Gate data = await _context.Gate.FromSqlRaw("SELECT * FROM Gate WHERE GateId=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (data != null)
                {
                    gateDto = _mapper.Map<GateDto>(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return gateDto;
        }

        #endregion

        #region Truck Nov_12_2024
        public async Task<DataTable> GetTruckList()
        {
            string sql = @"SELECT * from Truck";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }
        public async Task<TruckDto> GetTruckId(string id)
        {
            TruckDto truckDto = new TruckDto();
            try
            {
                Truck truck = await _context.Truck.FromSqlRaw("SELECT * FROM TRUCK WHERE VehicleRegNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (truck != null)
                {
                    truckDto = _mapper.Map<TruckDto>(truck);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return truckDto;
        }

        #endregion

        #region Trailer Nov_12_2024
        public async Task<TrailerDto> GetTrailerId(string id)
        {
            TrailerDto trailerDto = new TrailerDto();
            try
            {
                Trailer trailer = await _context.Trailer.FromSqlRaw("SELECT * FROM Trailer WHERE VehicleRegNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (trailer != null)
                {
                    trailerDto = _mapper.Map<TrailerDto>(trailer);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return trailerDto;
        }
        public async Task<DataTable> GetTrailerList()
        {
            string sql = @"SELECT * from Trailer";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        #endregion

        #region Driver Nov_12_2024

        public async Task<DataTable> GetDriverList(string active, string isBlack)
        {
            string sql = "";
            if ((active == "All" || active == null) && (isBlack == "All" || isBlack == null))
            {
                sql = @"SELECT * from Driver Order By LicenseNo";
            }
            else
            {
                sql = @"select (LicenseNo+' | '+Name) as DriverName,Name,LicenseNo from Driver  where (IsBlack<>1 OR IsBlack is null) And Active=1";
            }
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }
        public async Task<DriverDto> GetDriverId(string id)
        {
            DriverDto driverDto = new DriverDto();
            try
            {

                Driver data = await _context.Driver.FromSqlRaw("SELECT * FROM Driver WHERE LicenseNo=@lId", new SqlParameter("@lId", id)).SingleOrDefaultAsync();
                if (data != null)
                {
                    driverDto = _mapper.Map<DriverDto>(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return driverDto;
        }

        #endregion

        #region Yard Nov_20_24
        public async Task<DataTable> GetYardList(string active)
        {
            string sql = "";
            if (active == "All" || active == null)
            {
                sql = @"SELECT * from Yard Order By YardID";
            }
            else
            {
                sql = @"SELECT * from Yard Where Active='" + active + "'";
            }
            DataTable dt = await GetDataTableAsync(sql);
            return dt;

        }

        #endregion

        //#region TruckEntryType Nov_22_2024
        //public async Task<DataTable> GetTruckEntryTypeList(string active)
        //{
        //    string sql = "";
        //    if (active == "All" || active == null)
        //    {
        //        sql = @"SELECT * from TruckEntryType Order By TypeID";
        //    }
        //    else
        //    {
        //        sql = @"SELECT * from TruckEntryType where Active='" + active + "'";
        //    }
        //    DataTable dt = await GetDataTableAsync(sql);
        //    return dt;
        //}

        //#endregion

        #region WeightBridge Nov_22_2024
        public async Task<DataTable> GetWeightBridgeList()
        {
            string sql = @"SELECT * FROM WeightBridge";
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }
        public async Task<WeightBridgeDto> GetWeightBridgeID(string id)
        {
            WeightBridgeDto wbDto = new WeightBridgeDto();
            try
            {
                WeightBridge data = await _context.WeightBridge.FromSqlRaw("SELECT * FROM WeightBridge WHERE WeightBridgeID=@wID", new SqlParameter("@wID", id)).SingleOrDefaultAsync();
                if (data != null)
                {
                    wbDto = _mapper.Map<WeightBridgeDto>(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return wbDto;
        }

        #endregion

        //#region TruckJobType Nov_26_2024
        //public async Task<DataTable> GetTruckJobTypeList(string active)
        //{
        //    string sql = "";
        //    if (active == "All" || active == null)
        //    {
        //        sql = @"SELECT * from TruckJobType Order By TypeID";
        //    }
        //    else
        //    {
        //        sql = @"SELECT * from TruckJobType Where Active='" + active + "'";
        //    }
        //    DataTable dt = await GetDataTableAsync(sql);
        //    return dt;

        //}


        //#endregion

        //#region WaitingArea Nov_26_2024
        //public async Task<DataTable> GetWaitingAreaList(string active)
        //{
        //    string sql = "";
        //    if (active == "All" || active == null)
        //    {
        //        sql = @"SELECT * from WaitingArea Order By AreaID";
        //    }
        //    else
        //    {
        //        sql = @"SELECT * from WaitingArea Where Active='" + active + "'";
        //    }
        //    DataTable dt = await GetDataTableAsync(sql);
        //    return dt;
        //}

        //#endregion

        #region PCategory Nov_27_2024
        public async Task<DataTable> GetPCategoryList(string active)
        {
            string sql = "";
            if (active == "All" || active == null)
            {
                sql = @"SELECT * from PCategory Order By PCCode";
            }
            else
            {
                sql = @"SELECT * from PCategory Where  Active='" + active + "'";
            }
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }
        #endregion

        #region PCard Nov_27_2024
        public async Task<DataTable> GetPCardList(string active)
        {
            string sql = "";
            if (active == "All" || active == null)
            {
                sql = @"SELECT * from PCard Order By CardNo";
            }
            else
            {
                sql = @"SELECT * from PCard Where Active='" + active + "'";
            }
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }
        #endregion

        #region DocumentSetting Nov_29_2024
        public async Task<DataTable> GetDocumentSettingList(string active)
        {
            string sql = "";
            if(active=="All"|| active == null)
            {
                sql = @"SELECT * from DocumentSetting Order By DocCode";
            }
            else
            {
                sql = @"SELECT * from DocumentSetting Where Active='" + active + "'";
            }
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        #endregion

        #region OperationArea Nov_29_2024
        public async Task<DataTable> GetOperationAreaList(string active)
        {
            string sql = "";
            if (active == "All" || active == null)
            {
                sql = @"SELECT * from OperationArea Order By AreaID";
            }
            else
            {
                sql = @"SELECT * from OperationArea Where Active='" + active + "'";
            }
            DataTable dt = await GetDataTableAsync(sql);
            return dt;
        }

        #endregion
    }

}
