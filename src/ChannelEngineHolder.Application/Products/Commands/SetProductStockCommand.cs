using MediatR;

namespace ChannelEngineHolder.Application.Products.Commands
{
    public class SetProductStockCommand : IRequest
    {
        public string ProductNumber { get; set; }
        public int Stock { get; private set; }
    }
}
