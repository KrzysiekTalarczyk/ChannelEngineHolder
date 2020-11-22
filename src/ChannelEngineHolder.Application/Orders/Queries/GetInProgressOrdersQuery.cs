using System.Collections.Generic;
using ChannelEngineHolder.Domain.Models;
using MediatR;

namespace ChannelEngineHolder.Application.Orders.Queries
{
    public class GetInProgressOrdersQuery : IRequest<IEnumerable<Order>>
    {
    }
}
