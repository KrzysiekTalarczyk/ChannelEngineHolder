using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChannelEngineHolder.Domain.Models;

namespace ChannelEngineHolder.ConsoleApp
{
    public interface IConsoleAppDisplay
    {
        void DisplayInProgressOrders(IList<Order> orders);
        void DisplayTop5Products(IEnumerable<Product> products);
        string GetProductNumberForUpdateStock();
        void DisplayProduct(Product product);
        void DisplayError(Exception exception);
    }
}