using Autofac.Extensions.DependencyInjection;
using stc.business.mce;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace stc.api.mce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
                  .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                  .ConfigureWebHostDefaults(webHostBuilder =>
                  {
                      webHostBuilder
                       .UseContentRoot(Directory.GetCurrentDirectory())
                       .UseIISIntegration()
                       .UseStartup<Startup>()
                       .ConfigureKestrel(options =>
                       {
                           options.Limits.MaxRequestBodySize = ApiConfig.Common.MaxRequestBodySize * 1024 * 1024; // MB
                           options.Limits.MaxRequestBufferSize = ApiConfig.Common.MaxRequestBodySize * 1024 * 1024; // MB
                           options.Limits.MaxRequestHeadersTotalSize = ApiConfig.Common.MaxRequestBodySize * 1024 * 1024; // MB
                       });
                  });
    }
}
