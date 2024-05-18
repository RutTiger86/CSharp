using CSharp.RestAPI.Logging.Enums;
using CSharp.RestAPI.Logging.Models;
using CSharp.RestAPI.Logging.Models.Requests;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSharp.RestAPI.Logging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {

        // POST api/<AccountController>
        [HttpPost]
        public BaseResponse<string> LoggingPost(LoggingRequest loggingRequest)
        {
            return new BaseResponse<string>
            {
                Result = true,
                ErrorCode = (int)ErrorCode.None,
                ErrorMessage = ErrorCode.None.ToString(),
                Data = $"LoggingPost - {loggingRequest}"
            };
        }


        // GET: api/<AccountController>
        [HttpGet]
        public BaseResponse<string> LoggingGetAPI([FromQuery] LoggingRequest loggingRequest)
        {
            return new BaseResponse<string>
            {
                Result = true,
                ErrorCode = (int)ErrorCode.None,
                ErrorMessage = ErrorCode.None.ToString(),
                Data = $"LoggingPost - {loggingRequest}"
            };
        }
    }
}
