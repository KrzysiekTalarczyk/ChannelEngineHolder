using System.Collections.Generic;
using ChannelEngineHolder.Application.Orders.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Domain.Models;

namespace ChannelEngineHolder.Application.Orders.Handlers
{
    public class GetInProgressOrdersQueryHandler : IRequestHandler<GetInProgressOrdersQuery, IEnumerable<Order>>
    {
        private readonly IOrdersRepository _ordersRepository;

        public GetInProgressOrdersQueryHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        public async Task<IEnumerable<Order>> Handle(GetInProgressOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _ordersRepository.GetAllInProgress();
            return orders;
        }
    }
}
