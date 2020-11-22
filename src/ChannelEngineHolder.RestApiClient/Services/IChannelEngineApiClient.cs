using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelEngineHolder.Domain.Models;
using ChannelEngineHolder.RestApiClient.Models;

namespace ChannelEngineHolder.RestApiClient.Services
{
    public interface IChannelEngineApiClient
    {
        Task<IEnumerable<OrderResponse>> GetOrdersInProgress();
        Task<IEnumerable<Product>> GetProducts(List<string> numbers);
        Task<Product> GetProduct(string productNumber);
        Task SetProductStock(string productNumber, int stockValue);
    }
}