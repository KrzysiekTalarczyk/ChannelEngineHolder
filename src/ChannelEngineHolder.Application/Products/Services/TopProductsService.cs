using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Domain.Models;

namespace ChannelEngineHolder.Application.Products.Services
{
    public class TopProductsService : ITopProductsService
    {
        private readonly IOrdersRepository _ordersRepository;
        public TopProductsService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        public async Task<IEnumerable<Product>> GetProductsByQuantity(int number)
        {
            var orders = await _ordersRepository.GetAllInProgress();
            return orders.SelectMany(o => o.Products)
                         .OrderByDescending(p => p.Quantity)
                         .Take(number)
                         .Select( l =>l); //toDo check TotalQuantity
        }
    }
}
