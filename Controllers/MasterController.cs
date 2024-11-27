using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        //[HttpGet]
        //public async Task<IActionResult> GetTrailerTypeList(string active)
        //{
        //    DataTable dt = await _queryDAL.GetTrailerTypeList(active);
        //    return Ok(dt);
        //}

        //[HttpPost]
        //public async Task<IActionResult> SaveTrailerType(TrailerTypeDto info)
        //{
        //    ResponseMessage msg = await _updateDAL.SaveTrailerType(info);
        //    return Ok(msg);
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateTrailerType(TrailerTypeDto info)
        //{
        //    ResponseMessage msg = await _updateDAL.UpdateTrailerType(info);
        //    return Ok(msg);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTrailerType(string id)
        //{
        //    ResponseMessage msg = await _updateDAL.DeleteTrailerType(id);
        //    return Ok(msg);
        //}
        #endregion

        #region Transporter_Type Nov_12_2024

        [HttpGet]
        public async Task<IActionResult> GetTransporterTypeList(string active)
        {
            DataTable dt = await _queryDAL.GetTransporterTypeList(active);
            return Ok(dt);
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

        //[HttpGet]
        //public async Task<IActionResult> GetTransporterNames()
        //{
        //    var tNames = await _queryDAL.GetTransporterNames();
        //    return Ok(tNames);
        //}

        [HttpGet]
        public async Task<IActionResult> GetTransporterId(string id)
        {
            TransporterDto transporterDto = await _queryDAL.GetTransporterId(id);
            return Ok(transporterDto);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetOnlyTransporterTypes()
        //{
        //    var transporterTypes = await _queryDAL.GetOnlyTransporterTypes();
        //    return Ok(transporterTypes);
        //}

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
        public async Task<IActionResult> GetTrailerList()
        {
            DataTable dt = await _queryDAL.GetTrailerList();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrailerId(string id)
        {
            TrailerDto trailerDto = await _queryDAL.GetTrailerId(id);
            return Ok(trailerDto);
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
            //Console.WriteLine($"Received ID: {id}");
            ResponseMessage msg = await _updateDAL.DeleteDriver(id);
            return Ok(msg);
        }
        #endregion

        #region Yard Nov_20_2024
        [HttpGet]
        public async Task<IActionResult> GetYardList(string active)
        {
            DataTable dt=await _queryDAL.GetYardList(active);
            return Ok(dt);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetYardID(string id)
        //{
        //    YardDto yardDto=await _queryDAL.GetYardID(id);
        //    return Ok(yardDto);
        //}

        [HttpPost]
        public async Task<IActionResult> SaveYard(YardDto info)
        {
            ResponseMessage msg=await _updateDAL.SaveYard(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateYard(YardDto info)
        {
            ResponseMessage msg=await _updateDAL.UpdateYard(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYard(string id)
        {
            ResponseMessage msg=await _updateDAL.DeleteYard(id);
            return Ok(msg);
        }
        #endregion

        #region Truck_Entry_Type Nov_22_2024

        [HttpGet]
        public async Task<IActionResult> GetTruckEntryTypeList(string active)
        {
            DataTable dt = await _queryDAL.GetTruckEntryTypeList(active);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTruckEntryType(TruckEntryTypeDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveTruckEntryType(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTruckEntryType(TruckEntryTypeDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateTruckEntryType(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruckEntryType(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteTruckEntryType(id);
            return Ok(msg);
        }

        #endregion

        #region WeightBridge Nov_22_2024

        [HttpGet]
        public async Task<IActionResult> GetWeightBridgeList()
        {
            DataTable dt = await _queryDAL.GetWeightBridgeList();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetWeightBridgeID(string id)
        {
            WeightBridgeDto data = await _queryDAL.GetWeightBridgeID(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveWeightBridge(WeightBridgeDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveWeightBridge(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWeightBridge(WeightBridgeDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateWeightBridge(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeightBridge(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteWeightBridge(id);
            return Ok(msg);
        }
        #endregion

        #region TruckJobType Nov_26_2024
        [HttpGet]
        public async Task<IActionResult> GetTruckJobTypeList(string active)
        {
            DataTable dt = await _queryDAL.GetTruckJobTypeList(active);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTruckJobType(TruckJobTypeDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveTruckJobType(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTruckJobType(TruckJobTypeDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateTruckJobType(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTruckJobType(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteTruckJobType(id);
            return Ok(msg);
        }

        #endregion

        #region Waiting Area Nov_26_2024
        [HttpGet]
        public async Task<IActionResult> GetWaitingAreaList(string active)
        {
            DataTable dt = await _queryDAL.GetWaitingAreaList(active);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SaveWaitingArea(WaitingAreaDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveWaitingArea(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWaitingArea(WaitingAreaDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateWaitingArea(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWaitingArea(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteWaitingArea(id);
            return Ok(msg);
        }
        #endregion

        #region PCategory Nov_27_2024
        [HttpGet]
        public async Task<IActionResult> GetPCategoryList(string active)
        {
            DataTable dt = await _queryDAL.GetPCategoryList(active);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SavePCategory(PCategoryDto info)
        {
            ResponseMessage msg = await _updateDAL.SavePCategory(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePCategory(PCategoryDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdatePCategory(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePCategory(string id)
        {
            ResponseMessage msg = await _updateDAL.DeletePCategory(id);
            return Ok(msg);
        }
        #endregion

        #region PCard Nov_27_2024

        [HttpGet]
        public async Task<IActionResult> GetPCardList(string active)
        {
            DataTable dt = await _queryDAL.GetPCardList(active);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SavePCard(PCardDto info)
        {
            ResponseMessage msg = await _updateDAL.SavePCard(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePCard(PCardDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdatePCard(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePCard(string id)
        {
            ResponseMessage msg = await _updateDAL.DeletePCard(id);
            return Ok(msg);
        }
        #endregion



    }
}
