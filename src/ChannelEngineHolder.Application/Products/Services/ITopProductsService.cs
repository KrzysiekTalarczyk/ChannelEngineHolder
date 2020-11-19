using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelEngineHolder.Domain.Models;

namespace ChannelEngineHolder.Application.Products.Services
{
    public interface ITopProductsService
    {
        Task<IEnumerable<Product>> GetProductsByQuantity(int number);
    }
}
