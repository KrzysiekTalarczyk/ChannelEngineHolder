using System;
using System.Collections.Generic;

namespace ChannelEngineHolder.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ChannelName { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<Line> Lines { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }

    public class Line
    {
        public string MerchantProductNo { get; set; }
        // public string Gtin { get; set; }
        public int Quantity { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public string MerchantProductNo { get; set; }
        public string Gtin { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
    }
}
