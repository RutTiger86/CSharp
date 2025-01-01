using CSharp.RestAPI.Repository.Enums;
using CSharp.RestAPI.Repository.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CSharp.RestAPI.Repository.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : Controller
    {
        protected ILogger<BaseController> log;
        protected string ModuleName
        {
            get => ControllerContext.ActionDescriptor.AttributeRouteInfo.Template;
        }

        private string Serialize(object parameters)
        {
            return JsonSerializer.Serialize(parameters);
        }

        protected BaseResponse<T> ExceptionError<T>(string sDetail)
        {
            BaseResponse<T> response = new()
            {
                Result = false,
                ErrorCode = (int)ErrorCode.SYSTEM_EXCEPTION,
                ErrorMessage = sDetail,
                Data = default
            };

            log.LogError($"[{ModuleName}]  RESPONSE DATA  [{Serialize(response)}]{Environment.NewLine} Detail- {sDetail}{Environment.NewLine}");
            return response;
        }
    }
}
