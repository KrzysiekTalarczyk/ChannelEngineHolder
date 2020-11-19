using System.Threading.Tasks;

namespace ChannelEngineHolder.Application.Interfaces
{
    public interface IProductsRepository
    {
        Task SetStock(string requestProductNumber, int requestStock);
    }
}
