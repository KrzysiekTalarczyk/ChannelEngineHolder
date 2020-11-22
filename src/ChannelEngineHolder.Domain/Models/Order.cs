using System;
using System.Collections.Generic;

namespace ChannelEngineHolder.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ChannelName { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
