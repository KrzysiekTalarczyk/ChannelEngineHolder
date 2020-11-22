using System;
using System.Collections.Generic;
using System.Linq;
using ChannelEngineHolder.Domain.Models;

namespace ChannelEngineHolder.RestApiClient.Models
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string ChannelName { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<Line> Lines { get; set; }

        public Order MapToOrder() => new Order()
         {
             Id = Id,
             ChannelName = ChannelName,
             CreatedAt = CreatedAt,
             Products = Lines.Select(l => l.MapToProduct())
         };
    }

    public class Line
    {
        public string MerchantProductNo { get; set; }
        public string Gtin { get; set; }
        public int Quantity { get; set; }

        internal Product MapToProduct() => new Product()
        {
            MerchantProductNo = MerchantProductNo,
            Gtin = Gtin,
            Quantity = Quantity
        };
    }
}
