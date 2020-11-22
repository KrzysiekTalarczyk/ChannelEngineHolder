using System.Threading;
using System.Threading.Tasks;
using ChannelEngineHolder.Application.Exceptions;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Application.Products.Queries;
using ChannelEngineHolder.Domain.Models;
using MediatR;

namespace ChannelEngineHolder.Application.Products.Handlers
{
    class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
    {
        private readonly IProductsRepository _productsRepository;
        public GetProductQueryHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetAsync(request.ProductNumber);
            if (product is null)
                throw new ProductNotFoundException(request.ProductNumber);
            return product;
        }
    }
}
