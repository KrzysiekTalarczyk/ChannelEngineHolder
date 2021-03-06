﻿using System.Collections.Generic;
using ChannelEngineHolder.Domain.Models;
using MediatR;

namespace ChannelEngineHolder.Application.Products.Queries
{
    public class GetTopSoldProductsQuery : IRequest<IEnumerable<Product>>
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
