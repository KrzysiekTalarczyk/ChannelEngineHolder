using System.Reflection;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Application.Orders.Queries;
using ChannelEngineHolder.Application.Products.Services;
using ChannelEngineHolder.RestApiClient.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ChannelEngineHolder.Web
{
    public static class IoConfig
    {
        public static IServiceCollection RegisterIoDependencies(this IServiceCollection services)
        {
            services.AddScoped<IChannelEngineApiClient, ChannelEngineApiClient>();
            services.AddScoped<ITopProductsService, TopProductsService>();
            services.AddScoped<IProductsRepository, ProductProvider>();
            services.AddScoped<IOrdersRepository, OrderProvider>();
            services.AddMediatR(typeof(GetInProgressOrdersQuery).GetTypeInfo().Assembly);
            return services;
        }
    }
}
