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
        private readonly IProductsRepository _productsRepository;
        public TopProductsService(IOrdersRepository ordersRepository, IProductsRepository productsRepository)
        {
            _ordersRepository = ordersRepository;
            _productsRepository = productsRepository;
        }
        public async Task<IEnumerable<Product>> GetProductsByQuantity(int number)
        {
            var orders = await _ordersRepository.GetAllInProgress();
            var products = orders.SelectMany(o => o.Products)
                         .OrderByDescending(p => p.Quantity)
                         .Take(number)
                         .Select( l =>l); //toDo check TotalQuantity

            var productsDetails = _productsRepository.GetAllAsync();
            //toDo : set name
            return products;
        }
    }
}
