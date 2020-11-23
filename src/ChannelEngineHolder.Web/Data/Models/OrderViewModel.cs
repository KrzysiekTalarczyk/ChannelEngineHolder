namespace ChannelEngineHolder.Web.Data
{
    using System;

    namespace ChannelEngineHolder.WebApp.Models
    {
        public class OrderViewModel
        {
            public int Id { get; set; }

            //[Display(Name = "Channel Name")]
            public string ChannelName { get; set; }

          //  [Display(Name = "Created Date")]
          //  [DataType(DataType.Date)]
            public DateTime CreatedAt { get; set; }

            // public IEnumerable<Product> Products { get; set; }
        }
    }
}

