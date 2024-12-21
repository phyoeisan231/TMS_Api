using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TMS_Api.Services;

namespace TMS_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportQueryDAL _queryDAL;

        public ReportController(ReportQueryDAL queryDAL)
        {
            _queryDAL = queryDAL;
        }
        #region Truck in yard
        [HttpGet]
        public async Task<IActionResult> GetTruckInCheckList(string yard)
        {
            DataTable dt = await _queryDAL.GetTruckInCheckList(yard);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTruckInList(string yard)
        {
            DataTable dt = await _queryDAL.GetTruckInList(yard);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTruckInWeightList(string yard)
        {
            DataTable dt = await _queryDAL.GetTruckInWeightList(yard);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTruckOperationList(string yard)
        {
            DataTable dt = await _queryDAL.GetTruckOperationList(yard);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTruckOutWeightList(string yard)
        {
            DataTable dt = await _queryDAL.GetTruckOutWeightList(yard);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTruckOutCheckList(string yard)
        {
            DataTable dt = await _queryDAL.GetTruckOutCheckList(yard);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetTruckOutList(string yard)
        {
            DataTable dt = await _queryDAL.GetTruckOutList(yard);
            return Ok(dt);
        }
        #endregion

    }
}
