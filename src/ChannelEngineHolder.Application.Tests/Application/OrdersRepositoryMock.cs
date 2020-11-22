using System.Collections.Generic;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Domain.Models;
using Moq;

namespace ChannelEngineHolder.Tests.Application
{
     public class OrdersRepositoryMock : Mock<IOrdersRepository>
    {

        public void SetupOrderWithProducts(IEnumerable<Product> products)
        {
            Setup(x => x.GetAllInProgress()).ReturnsAsync(new List<Order>() {new Order() {Products = products}});
        }
    }
}
