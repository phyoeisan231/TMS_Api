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
        #endregion

        #region Gate In December_9_2024
        [HttpGet]
        public async Task<IActionResult> GetOutBoundCheckCardList(string yard, string gate)
        {
            DataTable dt = await _queryDAL.GetOutBoundCheckCardList(yard, gate);
            return Ok(dt);
        }

        [HttpPost]
        public async Task<IActionResult> SaveGateOut([FromForm] ICD_TruckProcessDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveGateOut(info);
            return Ok(msg);
        }
        #endregion

    }
}
