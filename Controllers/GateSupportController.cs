using Microsoft.AspNetCore.Http;
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

        [HttpPut]
        public async Task<IActionResult> SaveGateIn(ICD_InBoundCheckDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveGateIn(info);
            return Ok(msg);
        }
        #endregion

    }
}
