using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChannelEngineHolder.Application.Orders.Queries;
using ChannelEngineHolder.Domain.Models;
using ChannelEngineHolder.Web.Data.Models;
using MediatR;

namespace ChannelEngineHolder.Web.Data
{

        public class ChannelEngineApiService
        {
            private readonly IMediator _mediator;
            public ChannelEngineApiService(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<List<Order>> GetInProgressOrders()
            {
                 var orders = await _mediator.Send(new GetInProgressOrdersQuery());
                 return orders.ToList();
            }

            public async Task<List<ProductViewModel>> GetTopProducts()
            {   
                var products = new List<ProductViewModel>()
                {
                    new ProductViewModel() {Number = "1", Name = "Channel Name 1", Quantity = 5},
                    new ProductViewModel() {Number = "2", Name = "Channel Name 1", Quantity = 3},
                    new ProductViewModel() {Number = "3", Name = "Channel Name 1", Quantity = 1},
                };
                return await Task.FromResult(products);
            }

            public async Task SetProductStock(string number)
            {
               await Task.CompletedTask;
            }
        }
    }
