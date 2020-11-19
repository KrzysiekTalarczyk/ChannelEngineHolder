using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChannelEngineHolder.Application.Products.Dtos;
using ChannelEngineHolder.Application.Products.Queries;
using ChannelEngineHolder.Application.Products.Services;
using MediatR;

namespace ChannelEngineHolder.Application.Products.Handlers
{
    class GetTopSoldProductsQueryHandler : IRequestHandler<GetTopSoldProductsQuery, IEnumerable<ProductDto>>
    {
        private const int ProductNumber= 5;
        private readonly ITopProductsService _productsService;
        public GetTopSoldProductsQueryHandler(ITopProductsService productsService)
        {
            _productsService = productsService;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetTopSoldProductsQuery request, CancellationToken cancellationToken)
        {
            var top5Products = await _productsService.GetProductsByQuantity(ProductNumber);
            return top5Products.Select(p => new ProductDto(p));
        }
    }
}
