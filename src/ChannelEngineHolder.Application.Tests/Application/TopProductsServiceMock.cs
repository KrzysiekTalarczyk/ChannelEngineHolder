using System.Collections.Generic;
using ChannelEngineHolder.Application.Products.Services;
using ChannelEngineHolder.Domain.Models;
using Moq;

namespace ChannelEngineHolder.Tests.Application
{
   public class TopProductsServiceMock : Mock<ITopProductsService>
    {
        public void SetupResults(List<Product> products)
        {
            Setup(x => x.GetProductsByQuantity(It.IsAny<IEnumerable<Order>>(), It.IsAny<int>())).ReturnsAsync(products);
        }
    }
}
