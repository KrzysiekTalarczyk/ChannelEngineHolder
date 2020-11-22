using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChannelEngineHolder.Application.Products.Services;
using ChannelEngineHolder.Domain.Models;
using ChannelEngineHolder.Tests.Helpers;
using Xunit;

namespace ChannelEngineHolder.Tests.Application
{
    public class TopProductsServiceTests
    {
        [Theory, AutoMoqData]
        public async Task Should_BindProductName_When_TheSameProductNumber(ProductsRepositoryMock productsRepositoryMock,
                                                                           string productNumber, string productName)
        {
            var products = new List<Product>() {new Product() {MerchantProductNo = productNumber},};
            var productsWithName = new List<Product>() {new Product() {MerchantProductNo = productNumber, Name = productName},};
            var orders = new List<Order>() {new Order() {Products = products}};
            productsRepositoryMock.SetupGetByNumbersResult(productsWithName);
            var service = new TopProductsService(productsRepositoryMock.Object);

            var result = await service.GetProductsByQuantity(orders, 1);

            Assert.Equal(productName, result.First().Name);
        }

        [Theory, AutoMoqData]
        public async Task Should_SumQuantity_When_TheSameProductNumber(ProductsRepositoryMock productsRepositoryMock, string productNumber)
        {
            var products = new List<Product>() 
            {
                new Product() {MerchantProductNo = productNumber, Quantity = 1},
                new Product() {MerchantProductNo = productNumber, Quantity = 3},
            };
            var orders = new List<Order>() {new Order() {Products = products}};
            productsRepositoryMock.SetupGetByNumbersResult(products);
            var service = new TopProductsService(productsRepositoryMock.Object);

            var result = await service.GetProductsByQuantity(orders, 2);

            Assert.Equal(4, result.First().Quantity);
        }

        [Theory, AutoMoqData]
        public async Task Should_ReturnAllProducts_When_IsLessProductsThatRequested(ProductsRepositoryMock productsRepositoryMock,
                                                                                    Product product1, Product product2,
                                                                                    Product product3)
        {
            var number = 4;
            var products = new List<Product>() {product1, product2, product3};
            var orders = new List<Order>() {new Order() {Products = products}};
            productsRepositoryMock.SetupGetByNumbersResult(products);
            var service = new TopProductsService(productsRepositoryMock.Object);

            var result = await service.GetProductsByQuantity(orders, number);

            Assert.Equal(products.Count, result.Count());
            Assert.NotEqual(number, result.Count());
        }

        [Theory, AutoMoqData]
        public async Task Should_ReturnExpectedProducts_When_IsMoreProductsThatRequested(ProductsRepositoryMock productsRepositoryMock,
                                                                                         Product product1, Product product2,
                                                                                         Product product3)
        {
            var number = 2;
            var products = new List<Product>() {product1, product2, product3};
            var orders = new List<Order>() {new Order() {Products = products}};
            productsRepositoryMock.SetupGetByNumbersResult(products);
            var service = new TopProductsService(productsRepositoryMock.Object);

            var result = await service.GetProductsByQuantity(orders, number);

            Assert.NotEqual(products.Count, result.Count());
            Assert.Equal(number, result.Count());
        }

        [Theory, AutoMoqData]
        public async Task Should_ReturnProducts_With_ProperOrder(ProductsRepositoryMock productsRepositoryMock,
                                                                 Product product1,
                                                                 Product product2,
                                                                 Product product3)
        {
            var number = 3;
            product1.Quantity = 2;
            product2.Quantity = 1;
            product3.Quantity = 6;

            var products = new List<Product>()
            {
                product1, product2, product3
            };
            var orders = new List<Order>() {new Order() {Products = products}};
            productsRepositoryMock.SetupGetByNumbersResult(products);
            var service = new TopProductsService(productsRepositoryMock.Object);

            var result = await service.GetProductsByQuantity(orders, number);

            Assert.Equal(result.First().MerchantProductNo, product3.MerchantProductNo);
            Assert.Equal(result.Last().MerchantProductNo, product2.MerchantProductNo);
        }

        [Theory, AutoMoqData]
        public async Task Should_ReturnEmpty_When_OrderRepoReturnEmptyLists(TopProductsService service, int number)
        {
            var orders = new List<Order>();

            var result = await service.GetProductsByQuantity(orders, number);

            Assert.Empty(result);
        }
    }
}
