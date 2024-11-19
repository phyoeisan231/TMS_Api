using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using TMS_Api.DTOs;
using TMS_Api.Services;
namespace TMS_Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MasterQueryDAL _queryDAL;
        private readonly MasterUpdateDAL _updateDAL;
        private readonly IMapper _mapper;
        public MasterController(IConfiguration config, MasterQueryDAL queryDAL, MasterUpdateDAL updateDAL, IMapper mapper)
        {
            _configuration = config;
            _mapper = mapper;
            _queryDAL = queryDAL;
            _updateDAL = updateDAL;
        }

        //For Testing


        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetTesting()
        {
            return Ok("Working...");
        }

        #region Truck Type Nov_11_2024

        //[Authorize(Roles = "Admin,GateUser")]
        [HttpGet]
        public async Task<IActionResult> GetTruckTypeList(string active)
        {
            DataTable dt = await _queryDAL.GetTruckTypeList(active);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTruckType(TruckTypeDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveTruckType(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTruckType(TruckTypeDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateTruckType(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruckType(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteTruckType(id);
            return Ok(msg);
        }
        #endregion

        #region Trailer Type Nov_11_2024

        [HttpGet]
        public async Task<IActionResult> GetTrailerTypeList(string active)
        {
            DataTable dt = await _queryDAL.GetTrailerTypeList(active);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTrailerType(TrailerTypeDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveTrailerType(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTrailerType(TrailerTypeDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateTrailerType(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrailerType(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteTrailerType(id);
            return Ok(msg);
        }
        #endregion

        #region Transporter_Type Nov_12_2024

        [HttpGet]
        public async Task<IActionResult> GetTransporterTypeList(string active)
        {
            DataTable dt = await _queryDAL.GetTransporterTypeList(active);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransporterNames()
        {
            var tNames = await _queryDAL.GetTransporterNames();
            return Ok(tNames);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTransporterType(TransporterTypeDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveTransporterType(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransporterType(TransporterTypeDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateTransporterType(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransporterType(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteTransporterType(id);
            return Ok(msg);
        }
        #endregion

        #region Transporter Nov_12_2024

        [HttpGet]
        public async Task<IActionResult> GetTransporterList()
        {
            DataTable dt = await _queryDAL.GetTransporterList();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransporterId(string id)
        {
            TransporterDto transporterDto = await _queryDAL.GetTransporterId(id);
            return Ok(transporterDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetOnlyTransporterTypes()
        {
            var transporterTypes = await _queryDAL.GetOnlyTransporterTypes();
            return Ok(transporterTypes);
        }


        [HttpPost]
        public async Task<IActionResult> SaveTransporter([FromForm] TransporterDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveTransporter(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransporter([FromForm] TransporterDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateTransporter(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransporter(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteTransporter(id);
            return Ok(msg);
        }

        #endregion

        #region Gate Nov_11_2024

        [HttpGet]
        public async Task<IActionResult> GetGateList(string active)
        {
            DataTable dt = await _queryDAL.GetGateList(active);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetGateId(string id)
        {
            GateDto data = await _queryDAL.GetGateId(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveGate(GateDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveGate(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGate(GateDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateGate(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGate(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteGate(id);
            return Ok(msg);
        }
        #endregion

        #region Truck Nov_12_2024

        [HttpGet]
        public async Task<IActionResult> GetTruckList()
        {
            DataTable dt = await _queryDAL.GetTruckList();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTruckId(string id)
        {
            TruckDto truckDto = await _queryDAL.GetTruckId(id);
            return Ok(truckDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetOnlyTruckTypes()
        {
            var truckTypes = await _queryDAL.GetOnlyTruckTypes();
            return Ok(truckTypes);
        }


        [HttpPost]
        public async Task<IActionResult> SaveTruck([FromForm] TruckDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveTruck(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTruck([FromForm] TruckDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateTruck(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruck(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteTruck(id);
            return Ok(msg);
        }


        #endregion Truck

        #region Trailer Nov_12_2024

        [HttpGet]
        public async Task<IActionResult> GetTrailerLiist()
        {
            DataTable dt = await _queryDAL.GetTrailerLiist();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrailerId(string id)
        {
            TrailerDto trailerDto = await _queryDAL.GetTrailerId(id);
            return Ok(trailerDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetOnlyTrailerTypes()
        {
            var trailerTypes = await _queryDAL.GetOnlyTrailerTypes();
            return Ok(trailerTypes);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTrailer([FromForm] TrailerDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveTrailer(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTrailer([FromForm] TrailerDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateTrailer(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrailer(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteTrailer(id);
            return Ok(msg);
        }
        #endregion Trailer

        #region Driver Nov_12_2024

        [HttpGet]
        public async Task<IActionResult> GetDriverList()
        {
            DataTable dt = await _queryDAL.GetDriverList();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetDriverId(string id)
        {
            DriverDto data = await _queryDAL.GetDriverId(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveDriver([FromForm] DriverDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveDriver(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDriver([FromForm] DriverDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateDriver(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteDriver(id);
            return Ok(msg);
        }
        #endregion


    }
}
