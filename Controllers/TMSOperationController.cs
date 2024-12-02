using Microsoft.AspNetCore.Mvc;
using System.Data;
using TMS_Api.DTOs;
using TMS_Api.Services;

namespace TMS_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TMSOperationController : ControllerBase
    {
        private readonly TMSOperationQueryDAL _queryDAL;
        private readonly TMSOperationUpdateDAL _updateDAL;
        public TMSOperationController(IConfiguration config, TMSOperationQueryDAL queryDAL, TMSOperationUpdateDAL updateDAL)
        {
            _queryDAL = queryDAL;
            _updateDAL = updateDAL;
        }

        #region InBound Check Doc Nov_27_2024

        [HttpGet]
        public async Task<IActionResult> GetGateInBoundList(string yard)
        {
            DataTable dt = await _queryDAL.GetGateInBoundList(yard);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationAreaDataList(string yard)
        {
            DataTable dt = await _queryDAL.GetOperationAreaList(yard);
            return Ok(dt);
        }


        [HttpGet]
        public async Task<IActionResult> GetCardICDList(string yard)
        {
            DataTable dt = await _queryDAL.GetCardICDList(yard);
            return Ok(dt);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetTruckDataList(string id,string type)
        {
            DataTable dt = await _queryDAL.GetTruckList(id,type);
            return Ok(dt);
        }


        [HttpGet]
        public async Task<IActionResult> GetDriverDataList(string id)
        {
            DataTable dt = await _queryDAL.GetDriverList(id);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrailerDataList()
        {
            DataTable dt = await _queryDAL.GetTrailerList();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransporterDataList()
        {
            DataTable dt = await _queryDAL.GetTransporterList();
            return Ok(dt);
        }


        [HttpGet]
        public async Task<IActionResult> GetInBoundCheckList(DateTime startDate,DateTime endDate,string yard)
        {
            DataTable dt = await _queryDAL.GetInBoundCheckList(startDate, endDate,yard);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetInBoundCheckById(int id)
        {
            ICD_InBoundCheckDto data = await _queryDAL.GetInBoundCheckById(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveInBoundCheck(ICD_InBoundCheckDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveInBoundCheck(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInBoundCheck(ICD_InBoundCheckDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateInBoundCheck(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInBoundCheckDocument(int id,string docList,string user)
        {
            ResponseMessage msg = await _updateDAL.UpdateInBoundCheckDocument(id,docList,user);
            return Ok(msg);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInBoundCheck(int id,string user)
        {
            ResponseMessage msg = await _updateDAL.DeleteInBoundCheck(id,user);
            return Ok(msg);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInBoundCheckDocument(int id, string code)
        {
            ResponseMessage msg = await _updateDAL.DeleteInBoundCheckDocument(id, code);
            return Ok(msg);
        }

        #endregion

        #region Gate In December_2_2024
        [HttpGet]
        public async Task<IActionResult> GetInBoundCheckCardList(string yard,string gate)
        {
            DataTable dt = await _queryDAL.GetInBoundCheckCardList(yard,gate);
            return Ok(dt);
        }
        #endregion
    }
}
