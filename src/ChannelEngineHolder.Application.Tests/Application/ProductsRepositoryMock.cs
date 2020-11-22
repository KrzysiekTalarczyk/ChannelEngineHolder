using System.Collections.Generic;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Domain.Models;
using Moq;

namespace ChannelEngineHolder.Tests.Application
{
    public class ProductsRepositoryMock : Mock<IProductsRepository>
    {
        public void SetupEmptyResults()
        {
            Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(() => null);
        }


        public void SetupGetByNumbersResult(List<Product> products)
        {
            Setup(x => x.GetByNumbersAsync(It.IsAny<List<string>>())).ReturnsAsync(products);
        }
    }
}
