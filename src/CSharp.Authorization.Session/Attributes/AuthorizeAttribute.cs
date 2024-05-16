using CSharp.Authorization.Session.Enums;
using CSharp.Authorization.Session.Extensions;
using CSharp.Authorization.Session.Models;
using CSharp.Commons.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace CSharp.Authorization.Session.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public const string SessionKey = "CSharp.Authorizationn.Session.SessionKey";

        public readonly static byte[] AesKey = AesCryptHelper.CreateKey();

        public readonly static byte[] AesIv = AesCryptHelper.CreateIv();

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowPublicAttribute>().Any())
            {
                return;
            }

            var sessionInfo = context.HttpContext.GetSessionInfo();

            if (sessionInfo == null)
            {
                BaseResponse<string> res = new()
                {
                    ErrorCode = (int)ErrorCode.SESSION_UNAAUTHORIZED,
                    ErrorMessage = ErrorCode.SESSION_UNAAUTHORIZED.ToString(),
                    Result = false,
                };
                context.Result = new JsonResult(res);
            }
            else
            {

                if (sessionInfo.ExpriedTime < DateTime.Now)
                {
                    BaseResponse<string> res = new()
                    {
                        ErrorCode = (int)ErrorCode.SESSION_EXPIRED,
                        ErrorMessage = ErrorCode.SESSION_EXPIRED.ToString(),
                        Result = false,
                    };
                    context.Result = new JsonResult(res);
                }
                else
                {
                    sessionInfo.InitExpiredTime();
                    var userInfoJson = JsonSerializer.Serialize(sessionInfo);
                    var sessionValue = AesCryptHelper.Encrypt(userInfoJson, AuthorizeAttribute.AesKey, AuthorizeAttribute.AesIv);
                    context.HttpContext.Session.SetString(AuthorizeAttribute.SessionKey, sessionValue);
                }
            }
        }
    }
}
