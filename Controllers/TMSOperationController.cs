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

        #region ICD/Other InBound Check Doc Nov_27_2024
        [HttpGet]
        public async Task<IActionResult> GetCategoryList(string type)
        {
            DataTable dt = await _queryDAL.GetCategoryList(type);
            return Ok(dt);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetGateInBoundList(string yard)
        {
            DataTable dt = await _queryDAL.GetGateInBoundList(yard);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationAreaDataList(string yard,string gpName)
        {
            DataTable dt = await _queryDAL.GetOperationAreaList(yard, gpName);
            return Ok(dt);
        }


        [HttpGet]
        public async Task<IActionResult> GetCardList(string yard,string gpName)
        {
            DataTable dt = await _queryDAL.GetCardList(yard,gpName);
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

        #region ICD/Other In Check New

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

        #region ICD/Other Out Check Dec_9_2024
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

        #region ICD/Other Truck Status

        [HttpGet]
        public async Task<IActionResult> GetTruckProcessList(DateTime startDate, DateTime endDate, string status, string yard)
        {
            DataTable dt = await _queryDAL.GetTruckProcessList(startDate, endDate, status, yard);
            return Ok(dt);
        }

        #endregion

        #region TMS In Check Dec_17_2024

        [HttpGet]
        public async Task<IActionResult> GetInBoundCheckTMSList(DateTime startDate, DateTime endDate, string yard)
        {
            DataTable dt = await _queryDAL.GetInBoundCheckTMSList(startDate, endDate, yard);
            return Ok(dt);
        }
        [HttpGet]
        public async Task<IActionResult> GetTMSProposalList(DateTime startDate, DateTime endDate, string yard, string deptType)
        {
            DataTable dt = await _queryDAL.GetTMSProposalList(startDate, endDate, yard, deptType);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTMSProposalById(int id)
        {
            TMS_ProposalDto data = await _queryDAL.GetTMSProposalById(id);
            return Ok(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetTruckDataListByProposal(string id, int poNo,string type)
        {
            DataTable dt = await _queryDAL.GetTruckDataListByProposal(id, poNo, type);
            return Ok(dt);
        }

        #endregion

        #region TMS Out Check Dec_18_2024

        [HttpGet]
        public async Task<IActionResult> GetCardTMSInList(string card, string yard)
        {
            DataTable dt = await _queryDAL.GetCardTMSInList(card, yard);
            return Ok(dt);
        }
        [HttpGet]
        public async Task<IActionResult> GetOutBoundCheckTMSList(DateTime startDate, DateTime endDate, string yard)
        {
            DataTable dt = await _queryDAL.GetOutBoundCheckTMSList(startDate, endDate, yard);
            return Ok(dt);
        }
        #endregion

        #region Operation Dec_20_2024
        [HttpPut]
        public async Task<IActionResult> StartOperation(OperationDto info)
        {
            ResponseMessage msg = await _updateDAL.StartOperation(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> EndOperation(OperationDto info)
        {
            ResponseMessage msg = await _updateDAL.EndOperation(info);
            return Ok(msg);
        }
        #endregion
    }
}
