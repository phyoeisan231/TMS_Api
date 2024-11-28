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

        #region InBound Check Doc Nov_27_2024

        [HttpGet]
        public async Task<IActionResult> GetInBoundCheckList(DateTime startDate,DateTime endDate,string yard)
        {
            DataTable dt = await _queryDAL.GetInBoundCheckList(startDate, endDate,yard);
            return Ok(dt);
        }

        [HttpGet]
        public async Task<IActionResult> GetInBoundCheckById(int id)
        {
            InBoundCheckDto data = await _queryDAL.GetInBoundCheckById(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveInBoundCheck(InBoundCheckDto info)
        {
            ResponseMessage msg = await _updateDAL.SaveInBoundCheck(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInBoundCheck(InBoundCheckDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateInBoundCheck(info);
            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInBoundCheckDocument(InBoundCheckDocumentDto info)
        {
            ResponseMessage msg = await _updateDAL.UpdateInBoundCheckDocument(info);
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInBoundCheck(int id)
        {
            ResponseMessage msg = await _updateDAL.DeleteInBoundCheck(id);
            return Ok(msg);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInBoundCheckDocument(int regNo, int docCode)
        {
            ResponseMessage msg = await _updateDAL.DeleteInBoundCheckDocument(regNo, docCode);
            return Ok(msg);
        }
        
        #endregion
    }
}
