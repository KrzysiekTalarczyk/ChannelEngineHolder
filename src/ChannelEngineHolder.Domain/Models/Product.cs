namespace ChannelEngineHolder.Domain.Models
{
    public class Product
    {
        public Product()
        {
        }

        public static Product Create(string number, string gtin, int quantity) =>
            new Product()
            {
                MerchantProductNo = number,
                Gtin = gtin,
                Quantity = quantity
            };

        public static Product Create(Product source, string name, int stock) =>
            new Product()
            {
                MerchantProductNo = source.MerchantProductNo,
                Gtin = source.Gtin,
                Quantity = source.Quantity,
                Name = name,
                Stock = stock
            };

        public string Name { get; set; }
        public string MerchantProductNo { get; set; }
        public string Gtin { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
    }
}