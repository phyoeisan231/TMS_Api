﻿using AutoMapper;
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
        private readonly MasterQueryDAL _queryDAL;
        private readonly MasterUpdateDAL _updateDAL;
        public MasterController(MasterQueryDAL queryDAL, MasterUpdateDAL updateDAL)
        {
            _queryDAL = queryDAL;
            _updateDAL = updateDAL;
        }

        //For Testing 


        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetTesting()
        {
            return Ok("Working...123");
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

        #region Transporter Nov_12_2024


        [HttpGet]
        public async Task<IActionResult> GetTransporterList(string active, string isBlack)
        {
            DataTable dt = await _queryDAL.GetTransporterList(active, isBlack);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransporterId(string id)
        {
            TransporterDto transporterDto = await _queryDAL.GetTransporterId(id);
            return Ok(transporterDto);
        }


        [HttpPost]
        public async Task<IActionResult> SaveTransporter(TransporterDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveTransporter(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransporter(TransporterDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateTransporter(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> BlackFormForTransporter([FromBody] TransporterDto info)
        {
            ResponseMessage msg = await _updateDAL.BlackFormForTransporter(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransporter(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteTransporter(id);
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
        public async Task<IActionResult> SaveTruck(TruckDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveTruck(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> BlackFormForTruck([FromBody] TruckDto info)
        {
            ResponseMessage msg = await _updateDAL.BlackFormForTruck(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTruck(TruckDto info)
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
        public async Task<IActionResult> SaveTrailer(TrailerDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveTrailer(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTrailer(TrailerDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateTrailer(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> BlackFormForTrailer([FromBody] TrailerDto info)
        {
            ResponseMessage msg = await _updateDAL.BlackFormForTrailer(info);
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
        public async Task<IActionResult> GetDriverList(string active, string isBlack)
        {
            DataTable dt = await _queryDAL.GetDriverList(active, isBlack);
            return Ok(dt);
        }


        [HttpGet]
        public async Task<IActionResult> GetDriverId(string id)
        {
            DriverDto data = await _queryDAL.GetDriverId(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveDriver(DriverDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveDriver(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDriver(DriverDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateDriver(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> BlackFormForDriver([FromBody] DriverDto info)
        {
            ResponseMessage msg = await _updateDAL.BlackFormForDriver(info);
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


        //#region Truck_Entry_Type Nov_22_2024

        //[HttpGet]
        //public async Task<IActionResult> GetTruckEntryTypeList(string active)
        //{
        //    DataTable dt = await _queryDAL.GetTruckEntryTypeList(active);
        //    return Ok(dt);
        //}

        //[HttpPost]
        //public async Task<IActionResult> SaveTruckEntryType(TruckEntryTypeDto info)
        //{
        //    ResponseMessage msg = await _updateDAL.SaveTruckEntryType(info);
        //    return Ok(msg);
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateTruckEntryType(TruckEntryTypeDto info)
        //{
        //    ResponseMessage msg = await _updateDAL.UpdateTruckEntryType(info);
        //    return Ok(msg);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTruckEntryType(string id)
        //{
        //    ResponseMessage msg = await _updateDAL.DeleteTruckEntryType(id);
        //    return Ok(msg);
        //}

        //#endregion

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


        //#region TruckJobType Nov_26_2024

        //[HttpGet]
        //public async Task<IActionResult> GetTruckJobTypeList(string active)
        //{
        //    DataTable dt = await _queryDAL.GetTruckJobTypeList(active);
        //    return Ok(dt);
        //}

        //[HttpPost]
        //public async Task<IActionResult> SaveTruckJobType(TruckJobTypeDto info)
        //{
        //    ResponseMessage msg = await _updateDAL.SaveTruckJobType(info);
        //    return Ok(msg);
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateTruckJobType(TruckJobTypeDto info)
        //{
        //    ResponseMessage msg = await _updateDAL.UpdateTruckJobType(info);
        //    return Ok(msg);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTruckJobType(string id)
        //{
        //    ResponseMessage msg = await _updateDAL.DeleteTruckJobType(id);
        //    return Ok(msg);
        //}

        //#endregion

        //#region Waiting Area Nov_26_2024
        //[HttpGet]
        //public async Task<IActionResult> GetWaitingAreaList(string active)
        //{
        //    DataTable dt = await _queryDAL.GetWaitingAreaList(active);
        //    return Ok(dt);
        //}

        //[HttpPost]
        //public async Task<IActionResult> SaveWaitingArea(WaitingAreaDto info)
        //{
        //    ResponseMessage msg = await _updateDAL.SaveWaitingArea(info);
        //    return Ok(msg);
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateWaitingArea(WaitingAreaDto info)
        //{
        //    ResponseMessage msg = await _updateDAL.UpdateWaitingArea(info);
        //    return Ok(msg);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteWaitingArea(string id)
        //{
        //    ResponseMessage msg = await _updateDAL.DeleteWaitingArea(id);
        //    return Ok(msg);
        //}
        //#endregion


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


        #region DocumentSettings Nov_28_2024

        [HttpGet]
        public async Task<IActionResult> GetDocumentSettingList(string active)
        {
            DataTable dt = await _queryDAL.GetDocumentSettingList(active);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SaveDocumentSetting(DocumentSettingDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveDocumentSetting(info);
            return Ok(msg);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateDocumentSetting(DocumentSettingDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateDocumentSetting(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentSetting(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteDocumentSetting(id);
            return Ok(msg);
        }

        #endregion

        #region OperationArea Nov_29_2024

        [HttpGet]
        public async Task<IActionResult> GetOperationAreaList(string active)
        {
            DataTable dt = await _queryDAL.GetOperationAreaList(active);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOperationArea(OperationAreaDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveOperationArea(info);
            return Ok(msg);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateOperationArea(OperationAreaDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateOperationArea(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperationArea(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteOperationArea(id);
            return Ok(msg);
        }

        #endregion



    }
}
