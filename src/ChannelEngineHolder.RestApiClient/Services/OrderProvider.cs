using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Domain.Models;

namespace ChannelEngineHolder.RestApiClient.Services
{
    public class OrderProvider : IOrdersRepository
    {
        private readonly IChannelEngineApiClient _apiClient;

        public OrderProvider(IChannelEngineApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<Order>> GetAllInProgress()
        {
            return await _apiClient.GetOrdersInProgress();
        }
    }
}
