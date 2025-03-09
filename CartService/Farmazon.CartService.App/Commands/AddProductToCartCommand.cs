using MediatR;

namespace Farmazon.CartService.App.Commands
{
    public class AddProductToCartCommand : IRequest<bool>
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}