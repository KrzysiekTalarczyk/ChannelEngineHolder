using System.Collections.Generic;
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
        private const string BaseUrl = "";
        private const string ApiKey = "";

        [Fact]
        public async Task Should_ReturnOrders_FromRestApi()
        {
            var apiClient = GetApiClient();
           
            var orders = await apiClient.GetOrdersInProgress();

            Assert.NotEmpty(orders);
        }

        [Fact]
        public async Task Should_ReturnProducts_FromRestApi()
        {
            var apiClient = GetApiClient();
            var numbers = new List<string>() {"001201-L", "001201-S"};
           
            var products = await apiClient.GetProducts(numbers);

            Assert.NotEmpty(products);
        }

        [Fact]
        public async Task Should_ReturnProduct_FromRestApi()
        {
            var apiClient = GetApiClient();
            var number = "001201-L";

            var product = await apiClient.GetProduct(number);

            Assert.Equal(number, product.MerchantProductNo);
        }

        [Fact]
        public async Task Should_UpdateProduct_ByRestApi()
        {
            var apiClient = GetApiClient();
            var productNumber = "001201-L";
            var product = await GetProduct(apiClient, productNumber);
            var newProductStock = product.Stock + 1;

            await apiClient.SetProductStock(product.MerchantProductNo, newProductStock);

            var productAfter = await GetProduct(apiClient, product.MerchantProductNo);
            Assert.Equal(productAfter.Stock, newProductStock);
        }

        [Theory, AutoData]
        public async Task Should_ThrowError_When_PatchNotSuccess(string productNumber)
        {
            var apiClient = GetApiClient();

            var action = apiClient.SetProductStock(productNumber, 3);

            await Assert.ThrowsAsync<RequestFailException>(async () => await action);
        }


        private async Task<Product> GetProduct(ChannelEngineApiClient apiClient, string number)
        {
            var products = await apiClient.GetProduct(number);
            return products;
        }

        private ChannelEngineApiClient GetApiClient()
        {
            var config = new ChannelEngineApiConfig() { BaseUrl = BaseUrl, ApiKey = ApiKey };
            return new ChannelEngineApiClient(config);
        }
    }
}
