using CSharp.RestAPI.Logging.Enums;
using CSharp.RestAPI.Logging.Models;
using CSharp.RestAPI.Logging.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CSharp.RestAPI.Logging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        [HttpPost]
        public BaseResponse<string> LoggingPost(LoggingRequest loggingRequest)
        {
            return new BaseResponse<string>
            {
                Result = true,
                ErrorCode = (int)ErrorCode.NONE,
                ErrorMessage = ErrorCode.NONE.ToString(),
                Data = $"LoggingPost - {loggingRequest}"
            };
        }

        [HttpGet]
        public BaseResponse<string> LoggingGetAPI([FromQuery] LoggingRequest loggingRequest)
        {
            return new BaseResponse<string>
            {
                Result = true,
                ErrorCode = (int)ErrorCode.NONE,
                ErrorMessage = ErrorCode.NONE.ToString(),
                Data = $"LoggingPost - {loggingRequest}"
            };
        }
    }
}
