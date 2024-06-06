namespace CSharp.Authorization.Token.Middlewares
{

    public class AuthLoggingMiddleware
    {
        private readonly ILogger<AuthLoggingMiddleware> logger;
        private readonly RequestDelegate next;

        private IConfiguration configuration;

        public AuthLoggingMiddleware(ILogger<AuthLoggingMiddleware> logger, RequestDelegate next, IConfiguration configuration)
        {
            this.logger = logger;
            this.next = next;
            this.configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var claims = context.User.Claims.ToList();
                Console.WriteLine("User claims:");
                claims.ForEach(claim => Console.WriteLine($"{claim.Type}: {claim.Value}"));


                long expTimeValue = Convert.ToInt64(claims.First(p => p.Type == "exp").Value);
                DateTime expTime = DateTimeOffset.FromUnixTimeSeconds(expTimeValue).UtcDateTime;

                Console.WriteLine($"Exptime : {expTime} , NowTime : {DateTime.UtcNow}");

            }

            await next(context);
        }
    }
}
