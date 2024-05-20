using CSharp.Authorization.OAuth.Models;
using System.Configuration;

namespace CSharp.Authorization.OAuth.Helpers
{
    public static class OAuthHelper
    {
        public static OAuthInfo GetOAuthInfo()
        {
            return new OAuthInfo()
            {
                ClientId = ConfigurationManager.AppSettings.Get("clientid"),
                ClientSecret = ConfigurationManager.AppSettings.Get("clientsecret"),
                AuthUri = ConfigurationManager.AppSettings.Get("authuri"),
                TokenUri = ConfigurationManager.AppSettings.Get("tokenuri")
            };
        }
    }
}
