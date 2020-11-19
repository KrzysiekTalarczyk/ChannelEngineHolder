using System;

namespace ChannelEngineHolder.Application.Exceptions
{
   public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string number) : base($"Product with number {number} not found")
        {
            
        }
    }
}
