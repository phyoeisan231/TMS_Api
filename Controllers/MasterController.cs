using AutoMapper;
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
        private readonly IConfiguration _configuration;
        private readonly MasterQueryDAL _queryDAL;
        private readonly MasterUpdateDAL _updateDAL;
        private readonly IMapper _mapper;
        public MasterController(IConfiguration config, MasterQueryDAL queryDAL, MasterUpdateDAL updateDAL, IMapper mapper)
        {
            _configuration = config;
            _mapper = mapper;
            _queryDAL = queryDAL;
            _updateDAL = updateDAL;
        }

        //For Testing


        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetTesting()
        {
            return Ok("Working...");
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
    }
}
