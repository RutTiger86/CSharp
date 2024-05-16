namespace CSharp.Authorization.Session.Interfaces
{
    public interface IAuthorizationService
    {
        bool SetSessionInfo(HttpContext httpContext, string sessionId, int? timeoutMin);
    }
}