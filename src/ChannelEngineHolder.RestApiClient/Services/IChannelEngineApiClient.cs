using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelEngineHolder.Domain.Models;

namespace ChannelEngineHolder.RestApiClient.Services
{
    public interface IChannelEngineApiClient
    {
        Task<IEnumerable<Order>> GetOrdersInProgress();
        Task<IEnumerable<Product>> GetProducts();
        Task SetProductStock(string productNumber, int stockValue);
    }
}