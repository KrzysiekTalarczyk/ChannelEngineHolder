using System.Threading.Tasks;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Application.Orders.Queries;
using ChannelEngineHolder.Application.Products.Commands;
using ChannelEngineHolder.Application.Products.Queries;
using ChannelEngineHolder.Domain.Models;
using ChannelEngineHolder.Web.Data.Models;
using MediatR;

namespace ChannelEngineHolder.Web.Data
{

    public class ChannelEngineApiService
    {
        private readonly IMediator _mediator;
        public ChannelEngineApiService(IMediator mediator, IOrdersRepository ordersRepository)
        {
            _mediator = mediator;
        }
        public async Task<DisplayResults> GetResults()
        {
            var orders = await _mediator.Send(new GetInProgressOrdersQuery());
            var products = await _mediator.Send(new GetTopSoldProductsQuery() { Orders = orders });
            return new DisplayResults() { Orders = orders, Top5Products = products };
        }

        public async Task<Product> SetProductStock(string productNumber)
        {
            await _mediator.Send(new SetProductStockCommand() { ProductNumber = productNumber });
            var updatedProduct = await _mediator.Send(new GetProductQuery() { ProductNumber = productNumber });
            return updatedProduct;
        }
    }
}
