using CSharp.Authorization.Session.Attributes;
using CSharp.Authorization.Session.Enums;
using CSharp.Authorization.Session.Interfaces;
using CSharp.Authorization.Session.Models;
using CSharp.Commons.Helper;
using System.Text.Json;

namespace CSharp.Authorization.Session.Services
{
    public class AuthorizationService(ILogger<AuthorizationService> logger) : IAuthorizationService
    {
        private ILogger<AuthorizationService> logger = logger;

        public bool SetSessionInfo(HttpContext httpContext, string sessionId, int? timeoutMin)
        {
            try
            {
                SessionInfo sessionInfo = new()
                {
                    SessionId = sessionId
                };

                if (timeoutMin != null)
                {
                    sessionInfo.InitExpiredTime((int)timeoutMin);
                }
                else
                {
                    sessionInfo.InitExpiredTime();
                }

                var sessionInfoJson = JsonSerializer.Serialize(sessionInfo);
                var sessionValue = AesCryptHelper.Encrypt(sessionInfoJson, AuthorizeAttribute.AesKey, AuthorizeAttribute.AesIv);
                httpContext.Session.SetString(AuthorizeAttribute.SessionKey, sessionValue);

                return true;
            }
            catch (Exception ex)
            {                
                logger.LogError(ErrorCode.SYSTEM_EXCEPTION.ToString(), ex.ToString());
                return false;
            }
        }

    }
}
