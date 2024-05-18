using CSharp.RestAPI.Logging.Middlewares;

namespace CSharp.RestAPI.Logging.Extensions
{
    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggingMiddleware>();
        }
    }
}
