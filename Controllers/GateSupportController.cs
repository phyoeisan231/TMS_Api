using Microsoft.AspNetCore.Mvc;
using System.Data;
using TMS_Api.DTOs;
using TMS_Api.Services;

namespace TMS_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GateSupportController : ControllerBase
    {
        private readonly GateSupportQueryDAL _queryDAL;
        private readonly GateSupportUpdateDAL _updateDAL;
        public GateSupportController(IConfiguration config, GateSupportQueryDAL queryDAL, GateSupportUpdateDAL updateDAL)
        {
            _queryDAL = queryDAL;
            _updateDAL = updateDAL;
        }

        #region Gate In December_2_2024
        [HttpGet]
        public async Task<IActionResult> GetInBoundCheckCardList(string yard, string gate)
        {
            DataTable dt = await _queryDAL.GetInBoundCheckCardList(yard, gate);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SaveGateIn([FromForm] ICD_TruckProcessDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveGateIn(info);
            return Ok(msg);
        }

        [HttpGet]
        public async Task<IActionResult> GetInBoundCheckList(string yard, string gate,string fDate,string tDate)
        {
            DataTable dt = await _queryDAL.GetInBoundCheckList(yard, gate, fDate, tDate);
            return Ok(dt);
        }
        #endregion

        #region Gate Out December_9_2024
        [HttpGet]
        public async Task<IActionResult> GetOutBoundCheckCardList(string yard, string gate)
        {
            DataTable dt = await _queryDAL.GetOutBoundCheckCardList(yard, gate);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetOutBoundCheckList(string yard, string gate, string fDate, string tDate)
        {
            DataTable dt = await _queryDAL.GetOutBoundCheckList(yard, gate, fDate, tDate);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SaveGateOut([FromForm] ICD_TruckProcessDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveGateOut(info);
            return Ok(msg);
        }
        #endregion

        #region Truck Status Dec_12_2024
        [HttpGet]
        public async Task<IActionResult> GetTruckStatusReport(string yard, string gate, string fDate, string tDate,string status)
        {
            DataTable dt = await _queryDAL.GetTruckStatusReport(yard, gate, fDate, tDate, status);
            return Ok(dt);
        }
        #endregion

        #region Daily Report Dec_12_2024
        [HttpGet]
        public async Task<IActionResult> GetDailyInReport(string yard, string gate, string fDate, string tDate)
        {
            DataTable dt = await _queryDAL.GetDailyInReport(yard, gate, fDate, tDate);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetDailyOutReport(string yard, string gate, string fDate, string tDate)
        {
            DataTable dt = await _queryDAL.GetDailyOutReport(yard, gate, fDate, tDate);
            return Ok(dt);
        }
        #endregion

    }
}
