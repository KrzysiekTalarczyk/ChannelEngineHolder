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
        private const int StockValue = 25;
        private readonly IProductsRepository _productsRepository;
        public SetProductStockCommandHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<Unit> Handle(SetProductStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.GetAsync(request.ProductNumber);
            if (product is null)
            {
                throw new ProductNotFoundException(request.ProductNumber);
            }
            await _productsRepository.SetStock(request.ProductNumber, StockValue);
            return Unit.Value;
        }
    }
}