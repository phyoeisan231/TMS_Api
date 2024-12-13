using Microsoft.AspNetCore.Mvc;
using System.Data;
using TMS_Api.DBModels;
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
        public async Task<IActionResult> GetCategoryICDOList()
        {
            DataTable dt = await _queryDAL.GetCategoryICDOInList();
            return Ok(dt);
        }
        
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
        public async Task<IActionResult> GetWBDataList(string id)
        {
            DataTable dt = await _queryDAL.GetWBDataList(id);
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


        #endregion

        #region old 


        //[HttpPost]
        //public async Task<IActionResult> SaveInBoundCheck(ICD_InBoundCheckDto info)
        //{
        //    ResponseMessage msg = await _updateDAL.SaveInBoundCheck(info);
        //    return Ok(msg);
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateInBoundCheck(ICD_InBoundCheckDto info)
        //{
        //    ResponseMessage msg = await _updateDAL.UpdateInBoundCheck(info);
        //    return Ok(msg);
        //}

        //[HttpPut]
        //public async Task<IActionResult> UpdateInBoundCheckDocument(int id,string docList,string user)
        //{
        //    ResponseMessage msg = await _updateDAL.UpdateInBoundCheckDocument(id,docList,user);
        //    return Ok(msg);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> DeleteInBoundCheck(int id,string user)
        //{
        //    ResponseMessage msg = await _updateDAL.DeleteInBoundCheck(id,user);
        //    return Ok(msg);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> DeleteInBoundCheckDocument(int id, string code)
        //{
        //    ResponseMessage msg = await _updateDAL.DeleteInBoundCheckDocument(id, code);
        //    return Ok(msg);
        //}

        #endregion

        #region TruckProcessReport

        [HttpGet]
        public async Task<IActionResult> GetTruckProcessList(DateTime startDate, DateTime endDate, string status,string yard)
        {
            DataTable dt = await _queryDAL.GetTruckProcessList(startDate, endDate, status,yard);
            return Ok(dt);
        }


        #endregion

        #region New

        [HttpDelete]
        public async Task<IActionResult> DeleteInCheck(int id, string user)
        {
            ResponseMessage msg = await _updateDAL.DeleteInCheck(id, user);
            return Ok(msg);
        }
        [HttpPost]
        public async Task<IActionResult> SaveInCheck(ICD_InBoundCheckDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveInCheck(info);
            return Ok(msg);
        }

        [HttpGet]
        public async Task<IActionResult> GetDocumentSettingList(string id)
        {
            DataTable dt = await _queryDAL.GetDocumentSettingList(id);
            return Ok(dt);
        }

        #endregion
      

        #region Out Check Dec_9_2024
        [HttpGet]
        public async Task<IActionResult> GetCardICDOInList(string card,string yard)
        {
            DataTable dt = await _queryDAL.GetCardICDOInList(card,yard);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetGateOutBoundList(string yard)
        {
            DataTable dt = await _queryDAL.GetGateOutBoundList(yard);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryICDOOutList()
        {
            DataTable dt = await _queryDAL.GetCategoryICDOOutList();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetOutBoundCheckList(DateTime startDate, DateTime endDate, string yard)
        {
            DataTable dt = await _queryDAL.GetOutBoundCheckList(startDate, endDate, yard);
            return Ok(dt);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOutCheck(int id, string user)
        {
            ResponseMessage msg = await _updateDAL.DeleteOutCheck(id, user);
            return Ok(msg);
        }
        [HttpPost]
        public async Task<IActionResult> SaveOutCheck(ICD_OutBoundCheckDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveOutCheck(info);
            return Ok(msg);
        }
        [HttpGet]
        public async Task<IActionResult> GetOutBoundCheckById(int id)
        {
            ICD_OutBoundCheckDto data = await _queryDAL.GetOutBoundCheckById(id);
            return Ok(data);
        }

        #endregion
    }
}
