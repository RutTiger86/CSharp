using CSharp.Authorization.Session.Attributes;
using CSharp.Authorization.Session.Models;
using CSharp.Commons.Helper;
using System.Text.Json;

namespace CSharp.Authorization.Session.Extensions
{
    public static class HttpContextExtensions
    {
        public static SessionInfo? GetSessionInfo(this HttpContext context)
        {
            var sessionValue = context.Session.GetString(AuthorizeAttribute.SessionKey);

            if (string.IsNullOrEmpty(sessionValue))
            {
                return null;
            }

            string userInfoJson = AesCryptHelper.Decrypt(sessionValue, AuthorizeAttribute.AesKey, AuthorizeAttribute.AesIv);

            return JsonSerializer.Deserialize<SessionInfo>(userInfoJson);
        }
    }
}
