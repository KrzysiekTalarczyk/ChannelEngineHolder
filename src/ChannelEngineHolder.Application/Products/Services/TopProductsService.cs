using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Domain.Models;

namespace ChannelEngineHolder.Application.Products.Services
{
    public class TopProductsService : ITopProductsService
    {
        private readonly IProductsRepository _productsRepository;
        public TopProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<IEnumerable<Product>> GetProductsByQuantity(IEnumerable<Order> orders, int number)
        {
            var products = orders.SelectMany(o => o.Products)
                                 .GroupBy(p => p.MerchantProductNo)
                                 .Select(p => CreateProduct(p.First(), p.Sum(x => x.Quantity)))
                                 .OrderByDescending(p => p.Quantity)
                                 .Take(number).ToList();

            var productNumbers = products.Select(s => s.MerchantProductNo).ToList();
            var productsDetails = await _productsRepository.GetByNumbersAsync(productNumbers);

            var results = products.Join(productsDetails,
                p => p.MerchantProductNo,
                pd => pd.MerchantProductNo,
                (p, pd) =>  Product.Create(p, pd.Name, p.Stock))
                .Select(p => p);

            return results;
        }

        private Product CreateProduct(Product product, int quantity) =>
             Product.Create(product.MerchantProductNo, product.Gtin, quantity);
    }
}
