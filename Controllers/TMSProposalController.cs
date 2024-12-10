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
        public async Task<IActionResult> GetRailDailyJobList(DateTime sDate,string jobType,string yard)
        {
            DataTable dt = await _queryDAL.GetRailDailyJobList(sDate,jobType,yard);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerList()
        {
            DataTable dt = await _queryDAL.GetCustomerList();
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetWHDailyJobList(DateTime sDate, string jobType)
        {
            DataTable dt = await _queryDAL.GetWHDailyJobList(sDate, jobType);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetCCADailyJobList(DateTime sDate, string jobType)
        {
            DataTable dt = await _queryDAL.GetCCADailyJobList(sDate, jobType);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTMSProposal(TMS_ProposalDto info)
        {
            ResponseMessage msg = await _updateDAL.CreateTMSProposal(info);
            return Ok(msg);
        }

        
        #endregion

    }
}
