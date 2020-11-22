using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChannelEngineHolder.Application.Products.Queries;
using ChannelEngineHolder.Application.Products.Services;
using ChannelEngineHolder.Domain.Models;
using MediatR;

namespace ChannelEngineHolder.Application.Products.Handlers
{
    class GetTopSoldProductsQueryHandler : IRequestHandler<GetTopSoldProductsQuery, IEnumerable<Product>>
    {
        private const int ProductNumber= 5;
        private readonly ITopProductsService _productsService;
        public GetTopSoldProductsQueryHandler(ITopProductsService productsService)
        {
            _productsService = productsService;
        }
        public async Task<IEnumerable<Product>> Handle(GetTopSoldProductsQuery request, CancellationToken cancellationToken)
        {
            var top5Products = await _productsService.GetProductsByQuantity(request.Orders, ProductNumber);
            return top5Products;
        }
    }
}
