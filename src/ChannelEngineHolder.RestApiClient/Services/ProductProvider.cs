using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Domain.Models;
using Newtonsoft.Json;

namespace ChannelEngineHolder.RestApiClient.Services
{
    public class ProductProvider : IProductsRepository
    {
        private readonly IChannelEngineApiClient _apiClient;
        public ProductProvider(IChannelEngineApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        
        public async Task<IEnumerable<Product>> GetByNumbersAsync(List<string> nunbers)
        {
            return await _apiClient.GetProducts(nunbers);
        }

        public async Task SetStock(string productNumber, int stock)
        {
            await _apiClient.SetProductStock(productNumber, stock);
        }

        public async Task<Product> GetAsync(string productNumber)
        {
            return await _apiClient.GetProduct(productNumber);
        }
    }
}
