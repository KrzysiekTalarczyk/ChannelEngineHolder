using System.Collections.Generic;
using ChannelEngineHolder.Application.Products.Dtos;
using MediatR;

namespace ChannelEngineHolder.Application.Products.Queries
{
    public class GetTopSoldProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }
}
