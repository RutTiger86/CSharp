using CSharp.Protobuf.Server.Services;

namespace CSharp.Protobuf.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    ConfigureGrpcServices(webBuilder);
                    ConfigureGrpcPipeline(webBuilder);
                });
        }

        private static void ConfigureGrpcServices(IWebHostBuilder webBuilder)
        {
            webBuilder.ConfigureServices(services =>
            {
                services.AddGrpc();
            });
        }

        private static void ConfigureGrpcPipeline(IWebHostBuilder webBuilder)
        {
            webBuilder.Configure(app =>
            {
                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGrpcService<IAPServiceImpl>();

                    // 간단한 HTTP 확인용 엔드포인트
                    endpoints.MapGet("/", async context =>
                    {
                        await context.Response.WriteAsync("gRPC server is running. Use a gRPC client to communicate.");
                    });
                });
            });
        }
    }
}