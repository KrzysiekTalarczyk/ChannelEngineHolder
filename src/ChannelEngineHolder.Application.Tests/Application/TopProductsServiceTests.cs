using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using ChannelEngineHolder.Application.Interfaces;
using ChannelEngineHolder.Application.Products.Services;
using ChannelEngineHolder.Domain.Models;
using ChannelEngineHolder.Tests.Helpers;
using Moq;
using Xunit;

namespace ChannelEngineHolder.Tests.Application
{
    public class TopProductsServiceTests
    {

        [Theory, AutoMoqData]
        public async Task Should_ReturnEmpty_When_OrderRepoReturnEmptyLists([Frozen] Mock<IOrdersRepository> ordersRepositoryMock,
            TopProductsService service, int number)
        {
            ordersRepositoryMock.Setup(r => r.GetAllInProgress()).ReturnsAsync(new List<Order>());

            var result = await service.GetProductsByQuantity(number);

            Assert.Empty(result);
        }


        [Theory, AutoMoqData]
        public async Task Should_ReturnAllProducts_When_IsLessProductsThatRequested(
            [Frozen] ProductsRepositoryMock productsRepositoryMock,
            Product product1, Product product2, Product product3)
        {
            var number = 4;
            var products = new List<Product>() { product1, product2, product3 };

            var ordersRepositoryMock = new OrdersRepositoryMock();
            var service = new TopProductsService(ordersRepositoryMock.Object, productsRepositoryMock.Object);
            ordersRepositoryMock.SetupOrderWithProducts(products);

            var result = await service.GetProductsByQuantity(number);

            Assert.Equal(products.Count, result.Count());
            Assert.NotEqual(number, result.Count());
        }

        [Theory, AutoMoqData]
        public async Task Should_ReturnExpectedProducts_When_IsMoreProductsThatRequested(
            [Frozen] ProductsRepositoryMock productsRepositoryMock,
            Product product1, Product product2, Product product3)
        {
            var number = 2;
            var products = new List<Product>() {product1, product2, product3};

            var ordersRepositoryMock = new OrdersRepositoryMock();
            var service = new TopProductsService(ordersRepositoryMock.Object, productsRepositoryMock.Object);
            ordersRepositoryMock.SetupOrderWithProducts(products);

            var result = await service.GetProductsByQuantity(number);

            Assert.NotEqual(products.Count, result.Count());
            Assert.Equal(number, result.Count());
        }

        [Theory, AutoMoqData]
        public async Task Should_ReturnProducts_With_ProperOrder(
            [Frozen] ProductsRepositoryMock productsRepositoryMock,
            Product product1, Product product2, Product product3)
        {
            var number = 3;
            product1.Quantity = 2;
            product2.Quantity = 1;
            product3.Quantity = 6;

            var products = new List<Product>()
            {
               product1, product2, product3
            };
            var ordersRepositoryMock = new OrdersRepositoryMock();
            var service = new TopProductsService(ordersRepositoryMock.Object, productsRepositoryMock.Object);
            ordersRepositoryMock.SetupOrderWithProducts(products);

            var result = await service.GetProductsByQuantity(number);

            Assert.Equal(result.First(), product3);
            Assert.Equal(result.Last(), product2);
        }
    }
}
