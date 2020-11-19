using ChannelEngineHolder.Domain.Models;

namespace ChannelEngineHolder.Application.Orders.Dtos
{
    public class OrderDto
    {
        private Order o;

        public OrderDto(Order o)
        {
            this.o = o;
        }
    }
}
