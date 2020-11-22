using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using ChannelEngineHolder.Domain.Models;
using ChannelEngineHolder.RestApiClient.Configuration;
using ChannelEngineHolder.RestApiClient.Exceptions;
using ChannelEngineHolder.RestApiClient.Services;
using Xunit;

namespace ChannelEngineHolder.Tests.RestApiClient
{
    public class RestApiClientTests
    {
        private const string BaseUrl = "https://api-dev.channelengine.net/api/";
        private const string ApiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";

        [Fact]
        public async Task Should_ReturnOrders_FromRestApi()
        {
            var config = new ChannelEngineApiConfig() { BaseUrl = BaseUrl, ApiKey = ApiKey };
            var apiClient = new ChannelEngineApiClient(config);

            var orders = await apiClient.GetOrdersInProgress();

            Assert.NotEmpty(orders);
        }

        [Fact]
        public async Task Should_ReturnProducts_FromRestApi()
        {
            var config = new ChannelEngineApiConfig() { BaseUrl = BaseUrl, ApiKey = ApiKey };
            var apiClient = new ChannelEngineApiClient(config);

            var orders = await apiClient.GetProducts();

            Assert.NotEmpty(orders);
        }

        [Fact]
        public async Task Should_UpdateProducts_ByRestApi()
        {
            var config = new ChannelEngineApiConfig() { BaseUrl = BaseUrl, ApiKey = ApiKey };
            var apiClient = new ChannelEngineApiClient(config);
            var product = await GetRandomProduct(apiClient);
            var newProductStock = product.Stock + 1;

            await apiClient.SetProductStock(product.MerchantProductNo, newProductStock);

            var productAfter = await GetProduct(apiClient, product.MerchantProductNo);
            Assert.Equal(productAfter.Stock, newProductStock);
        }

        [Theory, AutoData]
        public async Task Should_ThrowError_When_PatchNotSuccess(string productNumber)
        {
            var config = new ChannelEngineApiConfig() { BaseUrl = BaseUrl, ApiKey = ApiKey };
            var apiClient = new ChannelEngineApiClient(config);

            var action = apiClient.SetProductStock(productNumber, 3);

            await Assert.ThrowsAsync<RequestFailException>(async () => await action);
        }


        private async Task<Product> GetProduct(ChannelEngineApiClient apiClient, string number)
        {
            var products = await apiClient.GetProducts();
            return products.FirstOrDefault(p => p.MerchantProductNo == number);
        }
        private async Task<Product> GetRandomProduct(ChannelEngineApiClient apiClient)
        {
            var products = await apiClient.GetProducts();
            return products.FirstOrDefault();
        }
    }
}
