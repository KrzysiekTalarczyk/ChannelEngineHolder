using System;
using System.Collections.Generic;
using ChannelEngineHolder.Application.Products.Services;
using ChannelEngineHolder.Domain.Models;
using Moq;

namespace ChannelEngineHolder.Application.Tests.Application
{
   public class TopProductsServiceMock : Mock<ITopProductsService>
    {
        public void SetupEmptyResults()
        {
            Setup(x => x.GetProductsByQuantity(It.IsAny<int>())).ReturnsAsync(new List<Product>());
        }

        public void SetupResults(List<Product> products)
        {
            Setup(x => x.GetProductsByQuantity(It.IsAny<int>())).ReturnsAsync(products);
        }
    }
}
