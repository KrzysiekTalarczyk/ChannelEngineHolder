using ChannelEngineHolder.RestApiClient.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace ChannelEngineHolder.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
           await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration.GetSection(nameof(ChannelEngineApiConfig))
                        .Get<ChannelEngineApiConfig>();
                    services.AddSingleton(configuration);
                    services.RegisterIoDependencies();
                    services.AddHostedService<ConsoleAppHost>();
                });
    }
}
