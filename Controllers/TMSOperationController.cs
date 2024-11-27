using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS_Api.DTOs;
using TMS_Api.Services;

namespace TMS_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TMSOperationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly TMSOperationQueryDAL _queryDAL;
        private readonly TMSOperationUpdateDAL _updateDAL;
        private readonly IMapper _mapper;
        public TMSOperationController(IConfiguration config, TMSOperationQueryDAL queryDAL, TMSOperationUpdateDAL updateDAL, IMapper mapper)
        {
            _configuration = config;
            _mapper = mapper;
            _queryDAL = queryDAL;
            _updateDAL = updateDAL;
        }

        #region PCard Nov_27_2024
        [HttpPost]
        public async Task<IActionResult> SavePCard(PCardDto info)
        {
            ResponseMessage msg = await _updateDAL.SavePCard(info);
            return Ok(msg);
        }
        #endregion
    }
}
