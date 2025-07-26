using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace stc.api.edu.unittest
{
    public class ServerProvider
    {
        private static Core.Common.ContainerManager _server = null;
        public static TestServer TestServer { get; private set; }

        public static Core.Common.ContainerManager GetEngine()
        {
            if (_server == null)
            {
                InitServerProvider();
                _server = Core.Common.Engine.ContainerManager;
            }
            return _server;
        }

        public static void InitServerProvider()
        {
            TestServer = new TestServer(new WebHostBuilder().ConfigureAppConfiguration((options, builder) => {
                builder.AddJsonFile($"appsettings.json");
            }).ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>());

            //TestServer = new TestServer(new WebHostBuilder().ConfigureAppConfiguration((options, builder) => {
            //    builder.AddJsonFile($"appsettings.{options.HostingEnvironment.EnvironmentName}.json");
            //}).ConfigureServices(services => services.AddAutofac())
            //    .UseStartup<Startup>()
            //    .UseEnvironment("Uat"));
        }
    }
}
