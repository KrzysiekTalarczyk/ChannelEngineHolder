using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ChannelEngineHolder.Domain.Models;
using ChannelEngineHolder.RestApiClient.Configuration;
using ChannelEngineHolder.RestApiClient.Exceptions;
using ChannelEngineHolder.RestApiClient.Models;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

namespace ChannelEngineHolder.RestApiClient.Services
{
    public class ChannelEngineApiClient : IChannelEngineApiClient
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly ChannelEngineApiConfig _apiConfig;

        public ChannelEngineApiClient(ChannelEngineApiConfig config)
        {
            _apiConfig = config;
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersInProgress()
        {
            var requestUrl = $"{_apiConfig.BaseUrl}v2/orders?statuses=IN_PROGRESS&apikey={_apiConfig.ApiKey}";
            var response = await GetAsync(requestUrl);
            var orders = await ReadResponse<IEnumerable<OrderResponse>>(response);
            return orders.Content;
        }

        public async Task<IEnumerable<Product>> GetProducts(List<string> numbers)
        {
            var requestUrl = $"{_apiConfig.BaseUrl}v2/products?merchantProductNoList{string.Join(",", numbers)}&apikey={_apiConfig.ApiKey}";
            var response = await GetAsync(requestUrl);
            var productResponse = await ReadResponse<IEnumerable<Product>>(response);
            return productResponse.Content;
        }

        public async Task<Product> GetProduct(string productNumber)
        {
            var requestUrl = $"{_apiConfig.BaseUrl}v2/products/{productNumber}?apikey={_apiConfig.ApiKey}";
            var response = await GetAsync(requestUrl);
            var productResponse = await ReadResponse<Product>(response);
            return productResponse.Content;
        }

        public async Task SetProductStock(string productNumber, int stockValue)
        {
            var patchUrl = $"{_apiConfig.BaseUrl}v2/products/{productNumber}?apikey={_apiConfig.ApiKey}";
            var patchDoc = new JsonPatchDocument<Product>().Replace(p => p.Stock, stockValue);
            var serializedItemToUpdate = JsonConvert.SerializeObject(patchDoc);
            var stringContent = new StringContent(serializedItemToUpdate, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PatchAsync(patchUrl, stringContent);
            await CheckResponse(response, patchUrl);
        }

        private static async Task CheckResponse(HttpResponseMessage response, string patchUrl)
        {
            var json = await response.Content.ReadAsStringAsync();
            var patchResponse = JsonConvert.DeserializeObject<PatchResponseBody>(json);

            if (!patchResponse.Success)
            {
                throw new RequestFailException(patchUrl, patchResponse.Message);
            }
        }

        private async Task<ChannelEngineContent<T>> ReadResponse<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            try
            {
                var des = JsonConvert.DeserializeObject<ChannelEngineContent<T>>(json);
                return des;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<HttpResponseMessage> GetAsync(string requestUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync(requestUrl);
                if (!response.IsSuccessStatusCode)
                    throw new RequestFailException(requestUrl);
                return response;
            }
            catch (Exception e)
            {
                throw new RequestFailException(requestUrl, e.Message);
            };
        }
    }
}
