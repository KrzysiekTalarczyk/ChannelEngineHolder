using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChannelEngineHolder.Application.Orders.Queries;
using ChannelEngineHolder.Application.Products.Commands;
using ChannelEngineHolder.Application.Products.Queries;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace ChannelEngineHolder.ConsoleApp
{
    public class ConsoleAppHost : IHostedService
    {
        private readonly IConsoleAppDisplay _appDisplay;
        private readonly IMediator _mediator;
        private readonly IHostApplicationLifetime _hostAppLifetime;

        public ConsoleAppHost(IMediator mediator, IConsoleAppDisplay appDisplay, IHostApplicationLifetime hostAppLifetime)
        {
            _mediator = mediator;
            _appDisplay = appDisplay;
            _hostAppLifetime = hostAppLifetime;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await DisplayResults();
            }
            catch (Exception ex)
            {
                _appDisplay.DisplayError(ex);
                Console.ReadKey();
            }
            finally
            {
                _hostAppLifetime.StopApplication();
            }
        }

        private async Task DisplayResults()
        {
            var orders = await _mediator.Send(new GetInProgressOrdersQuery());
            _appDisplay.DisplayInProgressOrders(orders.ToList());

            var products = await _mediator.Send(new GetTopSoldProductsQuery() { Orders = orders });
            _appDisplay.DisplayTop5Products(products);

            var productNumber = _appDisplay.GetProductNumberForUpdateStock();
            await _mediator.Send(new SetProductStockCommand() { ProductNumber = productNumber });

            var product = await _mediator.Send(new GetProductQuery() { ProductNumber = productNumber });
            _appDisplay.DisplayProduct(product);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
