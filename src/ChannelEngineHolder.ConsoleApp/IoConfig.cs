using System.Reflection;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Application.Orders.Queries;
using ChannelEngineHolder.Application.Products.Services;
using ChannelEngineHolder.RestApiClient.Services;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace ChannelEngineHolder.ConsoleApp
{
    public static class IoConfig
    {
        public static IServiceCollection RegisterIoDependencies(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetInProgressOrdersQuery).GetTypeInfo().Assembly);
            services.AddScoped<IChannelEngineApiClient, ChannelEngineApiClient>();
            services.AddScoped<ITopProductsService, TopProductsService>();
            services.AddScoped<IProductsRepository, ProductProvider>();
            services.AddScoped<IOrdersRepository, OrderProvider>();
            services.AddScoped<IConsoleAppDisplay, ConsoleAppDisplay>();

            return services;
        }
    }
}
