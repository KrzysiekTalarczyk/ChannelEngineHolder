using System.Collections.Generic;
using System.Linq;
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
            var orderResponses = await _apiClient.GetOrdersInProgress();
            return orderResponses.Select(o => o.MapToOrder());
        }
    }
}
