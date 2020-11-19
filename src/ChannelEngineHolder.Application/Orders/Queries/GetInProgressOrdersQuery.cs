using System.Collections.Generic;
using ChannelEngineHolder.Application.Orders.Dtos;
using MediatR;

namespace ChannelEngineHolder.Application.Orders.Queries
{
    public class GetInProgressOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
    }
}
