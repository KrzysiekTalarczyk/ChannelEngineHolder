using System.Collections.Generic;
using System.Linq;
using ChannelEngineHolder.Application.Orders.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Application.Orders.Dtos;

namespace ChannelEngineHolder.Application.Orders.Handlers
{
    public class GetInProgressOrdersQueryHandler : IRequestHandler<GetInProgressOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrdersRepository _ordersRepository;

        public GetInProgressOrdersQueryHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        public async Task<IEnumerable<OrderDto>> Handle(GetInProgressOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _ordersRepository.GetAllInProgress();
            return orders.Select(o => new OrderDto(o));
        }
    }
}
