using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChannelEngineHolder.Application.Exceptions;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Application.Products.Commands;
using ChannelEngineHolder.Application.Products.Services;
using MediatR;

namespace ChannelEngineHolder.Application.Products.Handlers
{
    public class SetProductStockCommandHandler : IRequestHandler<SetProductStockCommand>
    {
        private const int ProductNumber = 5;

        private readonly ITopProductsService _productsService;
        private readonly IProductsRepository _productsRepository;
        public SetProductStockCommandHandler(ITopProductsService productsService, IProductsRepository productsRepository)
        {
            _productsService = productsService;
            _productsRepository = productsRepository;
        }
        public async Task<Unit> Handle(SetProductStockCommand request, CancellationToken cancellationToken)
        {
            var top5Products = await _productsService.GetProductsByQuantity(ProductNumber);
            if (!top5Products.Any(p => p.MerchantProductNo.Equals(request.ProductNumber)))
            {
                throw new ProductNotFoundException(request.ProductNumber);
            }
            await _productsRepository.SetStock(request.ProductNumber, request.Stock);
            return Unit.Value;
        }
    }
}