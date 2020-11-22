using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using ChannelEngineHolder.Application.Exceptions;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Application.Products.Commands;
using ChannelEngineHolder.Application.Products.Handlers;
using ChannelEngineHolder.Domain.Models;
using ChannelEngineHolder.Tests.Helpers;
using Moq;
using Xunit;

namespace ChannelEngineHolder.Tests.Application
{
    public class SetProductStockCommandHandlerTests
    {
        [Theory, AutoMoqData]
        public async Task Should_ThrowError_When_ProductNotReturned(SetProductStockCommand command,
                                                                    CancellationToken token)
        {
            var productsRepositoryMock = new ProductsRepositoryMock();
            productsRepositoryMock.SetupEmptyResults();
            var handler = new SetProductStockCommandHandler(productsRepositoryMock.Object);

            await Assert.ThrowsAsync<ProductNotFoundException>(async () => await handler.Handle(command, token));
        }
        
        [Theory, AutoMoqData]
        public async Task Should_InvokeRepositoryMethod_When_ProductFounded([Frozen] Mock<IProductsRepository> productsRepositoryMock,

                                                                            SetProductStockCommand command,
                                                                            CancellationToken token,
                                                                            Product product)
        {
            command.ProductNumber = product.MerchantProductNo;
            var handler = new SetProductStockCommandHandler(productsRepositoryMock.Object);

            await handler.Handle(command, token);

            productsRepositoryMock.Verify(x => x.SetStock(command.ProductNumber, It.IsAny<int>()), Times.Once);
        }
    }
}
