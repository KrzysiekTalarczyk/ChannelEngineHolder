using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelEngineHolder.Domain.Models;

namespace ChannelEngineHolder.Application.Interfaces
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Order>> GetAllInProgress();
    }
}
