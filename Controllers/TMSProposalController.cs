using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TMS_Api.DTOs;
using TMS_Api.Services;

namespace TMS_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TMSProposalController : ControllerBase
    {
        private readonly TMSProposalQueryDAL _queryDAL;
        private readonly TMSProposalUpdateDAL _updateDAL;
        public TMSProposalController(IConfiguration config, TMSProposalQueryDAL queryDAL, TMSProposalUpdateDAL updateDAL)
        {
            _queryDAL = queryDAL;
            _updateDAL = updateDAL;
        }

        #region In Check TMS_Proposal 
        [HttpGet]
        public async Task<IActionResult> GetYardList()
        {
            DataTable dt = await _queryDAL.GetYardList();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetRailDailyJobList(string jobType,string yard)
        {
            DataTable dt = await _queryDAL.GetRailDailyJobList(jobType,yard);
            return Ok(dt);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProposal(string id)
        {
            ResponseMessage msg = await _updateDAL.DeleteProposal(id);
            return Ok(msg);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetJobCodeList(string id)
        //{
        //    DataTable dt = await _queryDAL.GetJobCodeList(id);
        //    return Ok(dt);
        //}

        [HttpGet]
        public async Task<IActionResult> GetWHDailyJobList(string jobType,string yard)
        {
            DataTable dt = await _queryDAL.GetWHDailyJobList(jobType,yard);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetCCADailyJobList(string jobType,string yard)
        {
            DataTable dt = await _queryDAL.GetCCADailyJobList(jobType,yard);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTMSProposal(TMS_ProposalDto info)
        {
            ResponseMessage msg = await _updateDAL.CreateTMSProposal(info);
            return Ok(msg);
        }

        [HttpGet]
        public async Task<IActionResult> GetProposalList(DateTime startDate,DateTime endDate, string deptType)
        {
            DataTable dt = await _queryDAL.GetProposalList(startDate, endDate, deptType);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTruckList(string type,string jobType)
        {
            DataTable dt = await _queryDAL.GetTruckList(type,jobType);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetProposalListById(string id)
        {
            DataTable dt = await _queryDAL.GetProposalListById(id);
            return Ok(dt);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTMSProposal(TMS_ProposalDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateTMSProposal(info);
            return Ok(msg);
        }
        #endregion


        #region TMSPropoal Detail
        [HttpPost]
        public async Task<IActionResult> CreateProposalDetail(TMS_ProposalDetailDto info)
        {
            ResponseMessage msg = await _updateDAL.CreateProposalDetail(info);
            return Ok(msg);
        }

        [HttpGet]
        public async Task<IActionResult> GetProposalDetailList(string propNo)
        {
            TMS_ProposalDto data = await _queryDAL.GetProposalDetailList(propNo);
            return Ok(data);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProposalDetail(string id, string truckNo)
        {
            ResponseMessage msg = await _updateDAL.DeleteProposalDetail(id,truckNo);
            return Ok(msg);
        }

        #endregion

    }
}
