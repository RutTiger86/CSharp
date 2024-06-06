using CSharp.Authorization.Token.Middlewares;

namespace CSharp.Authorization.Token.Extensions
{
    public static class AuthLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthLoggingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthLoggingMiddleware>();
        }
    }
}
