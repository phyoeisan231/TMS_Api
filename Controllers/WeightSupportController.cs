using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TMS_Api.DTOs;
using TMS_Api.Services;

namespace TMS_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WeightSupportController : ControllerBase
    {
        private readonly WeightSupportQueryDAL _queryDAL;
        private readonly WeightSupportUpdateDAL _updateDAL;
        public WeightSupportController(IConfiguration config, WeightSupportQueryDAL queryDAL, WeightSupportUpdateDAL updateDAL)
        {
            _queryDAL = queryDAL;
            _updateDAL = updateDAL;
        }


        #region In Weight 11_Dec_2024
        [HttpGet]
        public async Task<IActionResult> GetWeightBridgeQueueList(string yard, string gate)
        {
            DataTable dt = await _queryDAL.GetWeightBridgeQueueList(yard, gate);
            return Ok(dt);
        }


        [HttpGet]
        public async Task<IActionResult> GetWeightBridgeList()
        {
            DataTable dt = await _queryDAL.GetWeightBridgeList();
            return Ok(dt);
        }


        [HttpGet]
        public async Task<IActionResult> GetTrailerList(string active, string isBlack)
        {
            DataTable dt = await _queryDAL.GetTrailerList(active, isBlack);
            return Ok(dt);
        }


        [HttpGet]
        public async Task<IActionResult> GetTruckList(string active, string isBlack)
        {
            DataTable dt = await _queryDAL.GetTruckList(active, isBlack);
            return Ok(dt);
        }


        [HttpPost]
        public async Task<IActionResult> SaveServiceBillForAdHoc(WeightServiceBillDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveServiceBillForAdHoc(info);
            return Ok(msg);
        }


        [HttpPost]
        public async Task<IActionResult> SaveServiceBillForQueue(WeightServiceBillDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveServiceBillForQueue(info);
            return Ok(msg);
        }



        [HttpPut]
        public async Task<IActionResult> UpdateServiceBillForQueue(WeightServiceBillDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateServiceBillForQueue(info);
            return Ok(msg);
        }


        [HttpGet]
        public async Task<IActionResult> GetServiceBillList(DateTime fromDate, DateTime toDate, string yard, string gate)
        {
            DataTable dt = await _queryDAL.GetServiceBillList(fromDate, toDate, yard, gate);
            return Ok(dt);
        }
        #endregion
    }
}
