using ChannelEngineHolder.Domain.Models;

namespace ChannelEngineHolder.Application.Products.Dtos
{
   public class ProductDto
   {
        private Product p;

        public ProductDto(Product p)
        {
            this.p = p;
        }

        public string Number { get; set; }
       public string Name { get; set; }
       public string Gtin { get; set; }
       public int TotalQuantity { get; set; }
    }
}
