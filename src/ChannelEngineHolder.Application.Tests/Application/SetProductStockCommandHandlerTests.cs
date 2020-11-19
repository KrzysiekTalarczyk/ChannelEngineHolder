using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using ChannelEngineHolder.Application.Exceptions;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Application.Products.Commands;
using ChannelEngineHolder.Application.Products.Handlers;
using ChannelEngineHolder.Application.Tests.Helpers;
using ChannelEngineHolder.Domain.Models;
using Moq;
using Xunit;

namespace ChannelEngineHolder.Application.Tests.Application
{
    public class SetProductStockCommandHandlerTests
    {
        [Theory, AutoMoqData]
        public async Task Should_ThrowError_When_ProductsNotReturned([Frozen] TopProductsServiceMock topProductsServiceMock,
                                                                  SetProductStockCommandHandler handler,
                                                                  SetProductStockCommand command,
                                                                  CancellationToken token)
        {
            topProductsServiceMock.SetupEmptyResults();

            await Assert.ThrowsAsync<ProductNotFoundException>(async () => await handler.Handle(command, token));
        }

        [Theory, AutoMoqData]
        public async Task Should_ThrowError_When_ProductNotFound([Frozen] TopProductsServiceMock topProductsServiceMock,
            SetProductStockCommandHandler handler,
            SetProductStockCommand command,
            CancellationToken token,
            Product product)
        {
            command.ProductNumber = "number";
            topProductsServiceMock.SetupResults(new List<Product>() { product });

            await Assert.ThrowsAsync<ProductNotFoundException>(async () => await handler.Handle(command, token));
        }

        [Theory, AutoMoqData]
        public async Task Should_InvokeRepositoryMethod_When_ProductFounded([Frozen] Mock<IProductsRepository> productsRepositoryMock,
                                                                           
                                                                            SetProductStockCommand command,
                                                                            CancellationToken token,
                                                                            Product product)
        {
            command.ProductNumber = product.MerchantProductNo;
            var topProductsServiceMock = new TopProductsServiceMock();
            topProductsServiceMock.SetupResults(new List<Product>() { product });
            var handler =
                new SetProductStockCommandHandler(topProductsServiceMock.Object, productsRepositoryMock.Object);
           
            await handler.Handle(command, token);

            productsRepositoryMock.Verify(x => x.SetStock(command.ProductNumber, command.Stock), Times.Once);
        }
    }
}
