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
        public TMSOperationController(IConfiguration config, TMSOperationQueryDAL queryDAL, TMSOperationUpdateDAL updateDAL)
        {
            _configuration = config;
            _queryDAL = queryDAL;
            _updateDAL = updateDAL;
        }
        
    }
}
