using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelEngineHolder.Domain.Models;

namespace ChannelEngineHolder.Application.Interfaces
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task SetStock(string productNumber, int stock);
    }
}
