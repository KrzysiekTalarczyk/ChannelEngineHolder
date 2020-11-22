using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelEngineHolder.Domain.Models;

namespace ChannelEngineHolder.Application.Interfaces
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetByNumbersAsync(List<string> numbers);
        Task SetStock(string productNumber, int stock);
        Task<Product> GetAsync(string productNumber);
    }
}
