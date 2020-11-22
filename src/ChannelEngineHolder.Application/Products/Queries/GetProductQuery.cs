using System.Collections.Generic;
using ChannelEngineHolder.Domain.Models;
using MediatR;

namespace ChannelEngineHolder.Application.Products.Queries
{
    public class GetProductQuery : IRequest<Product>
    {
        public string ProductNumber { get; set; }
    }
}
