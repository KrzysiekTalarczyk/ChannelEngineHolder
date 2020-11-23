using System.Collections.Generic;
using ChannelEngineHolder.Domain.Models;

namespace ChannelEngineHolder.Web.Data.Models
{
    public class DisplayResults
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Product> Top5Products { get; set; }
        public Product UpdatedProduct { get; set; }
    }
}
