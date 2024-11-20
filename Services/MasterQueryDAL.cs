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

        //#region Truck Type Nov_11_2024
        //public async Task<DataTable> GetTruckTypeList(string active)
        //{
        //    string sql = "";
        //    if (active == "All" || active == null)
        //    {
        //        sql = @"SELECT * from TruckType";
        //    }
        //    else
        //    {
        //        sql = @"SELECT * from TruckType where Active='" + active + "'";
        //    }


        //    DataTable dt = await GetDataTableAsync(sql);
        //    return dt;
        //}

        //#endregion

        //#region Trailer Type Nov_12_2024
        //public async Task<DataTable> GetTrailerTypeList(string active)
        //{
        //    string sql = "";
        //    if (active == "All" || active == null)
        //    {
        //        sql = @"SELECT * from TrailerType Order By Description";
        //    }
        //    else
        //    {
        //        sql = @"SELECT * from TrailerType Where Active='" + active + "'";
        //    }

        //    DataTable dt = await GetDataTableAsync(sql);
        //    return dt;
        //}

        //#endregion

        //#region Transporter Type Nov_12_2024
        //public async Task<DataTable> GetTransporterTypeList(string active)
        //{
        //    string sql = "";
        //    if (active == "All" || active == null)
        //    {
        //        sql = @"SELECT * from TransporterType Order By TypeName";
        //    }
        //    else
        //    {
        //        sql = @"SELECT * from TransporterType where Active='" + active + "'";
        //    }
        //    DataTable dt = await GetDataTableAsync(sql);
        //    return dt;
        //}

        //#endregion

        //#region Transporter Nov_12_2024
        ////public async Task<DataTable> GetTransporterList()
        ////{
        ////    string sql = @"SELECT * from Transporter";
        ////    DataTable dt = await GetDataTableAsync(sql);
        ////    return dt;
        ////}
        ////public async Task<string[]> GetTransporterNames()
        ////{
        ////    return await _context.Transporter.Where(t => t.Active == true).OrderBy(t => t.TransporterName).Select(t => t.TransporterName).ToArrayAsync();
        ////}
        ////public async Task<TransporterDto> GetTransporterId(string id)
        ////{
        ////    TransporterDto transporterDto = new TransporterDto();
        ////    try
        ////    {
        ////        Transporter data = await _context.Transporter.FromSqlRaw("SELECT * FROM Transporter WHERE TransporterCode=@code", new SqlParameter("@code", id)).SingleOrDefaultAsync();
        ////        if (data != null)
        ////        {
        ////            transporterDto = _mapper.Map<TransporterDto>(data);
        ////        }
        ////    }
        ////    catch (Exception e)
        ////    {
        ////        Console.WriteLine(e);
        ////    }
        ////    return transporterDto;
        ////}
        ////public async Task<string[]> GetOnlyTransporterTypes()
        ////{
        ////    return await _context.TransporterType.Where(t => t.Active == true).OrderBy(t => t.Description).Select(t => t.Description).ToArrayAsync();
        ////}

        //#endregion

        //#region Gate Nov_12_2024
        ////public async Task<DataTable> GetGateList(string active)
        ////{
        ////    string sql = "";
        ////    if (active == "All" || active == null)
        ////    {
        ////        sql = @"SELECT * from Gate Order By Name";
        ////    }
        ////    else
        ////    {
        ////        sql = @"SELECT * from Gate Where Active='" + active + "'";
        ////    }
        ////    DataTable dt = await GetDataTableAsync(sql);
        ////    return dt;

        ////}
        ////public async Task<GateDto> GetGateId(string id)
        ////{
        ////    GateDto gateDto = new GateDto();
        ////    try
        ////    {

        ////        Gate data = await _context.Gate.FromSqlRaw("SELECT * FROM Gate WHERE GateId=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
        ////        if (data != null)
        ////        {
        ////            gateDto = _mapper.Map<GateDto>(data);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Console.WriteLine(ex);
        ////    }

        ////    return gateDto;
        ////}

        //#endregion

        //#region Truck Nov_12_2024
        //public async Task<DataTable> GetTruckList()
        //{
        //    string sql = @"SELECT * from Truck";
        //    DataTable dt = await GetDataTableAsync(sql);
        //    return dt;
        //}
        //public async Task<TruckDto> GetTruckId(string id)
        //{
        //    TruckDto truckDto = new TruckDto();
        //    try
        //    {
        //        Truck truck = await _context.Truck.FromSqlRaw("SELECT * FROM TRUCK WHERE VehicleRegNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
        //        if (truck != null)
        //        {
        //            truckDto = _mapper.Map<TruckDto>(truck);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //    return truckDto;
        //}
        ////public async Task<string[]> GetOnlyTruckTypes()
        ////{
        ////    return await _context.TruckType.Where(t => t.Active == true).OrderBy(t => t.Description).Select(t => t.Description).ToArrayAsync();
        ////}

        //#endregion

        //#region Trailer Nov_12_2024
        //public async Task<TrailerDto> GetTrailerId(string id)
        //{
        //    TrailerDto trailerDto = new TrailerDto();
        //    try
        //    {
        //        Trailer trailer = await _context.Trailer.FromSqlRaw("SELECT * FROM Trailer WHERE VehicleRegNo=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
        //        if (trailer != null)
        //        {
        //            trailerDto = _mapper.Map<TrailerDto>(trailer);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //    return trailerDto;
        //}
      
        //public async Task<DataTable> GetTrailerLiist()
        //{
        //    string sql = @"SELECT * from Trailer";
        //    DataTable dt = await GetDataTableAsync(sql);
        //    return dt;
        //}

        //#endregion

        //#region Driver Nov_12_2024

        //public async Task<DataTable> GetDriverList()
        //{
        //    string sql = @"SELECT * FROM Driver";
        //    DataTable dt = await GetDataTableAsync(sql);
        //    return dt;
        //}
        //public async Task<DriverDto> GetDriverId(string id)
        //{
        //    DriverDto driverDto = new DriverDto();
        //    try
        //    {

        //        Driver data = await _context.Driver.FromSqlRaw("SELECT * FROM Driver WHERE LicenseNo=@lId", new SqlParameter("@lId", id)).SingleOrDefaultAsync();
        //        if (data != null)
        //        {
        //            driverDto = _mapper.Map<DriverDto>(data);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }

        //    return driverDto;
        //}

        //#endregion





    }

}
