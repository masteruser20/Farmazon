using Farmazon.CartService.App.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Farmazon.CartService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController(IMediator mediator) : ControllerBase
    {
        [HttpPost("{cartId}/items")]
        public async Task<IActionResult> AddItemToCart(Guid cartId, [FromBody] AddProductToCartCommand request)
        {
            var command = new AddProductToCartCommand
            {
                CartId = cartId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            };

            var result = await mediator.Send(command);

            if (!result)
            {
                // detailed error there + logging
                throw new Exception("Error adding item to the cart");
            }

            return CreatedAtAction(nameof(GetCart), new { cartId }, null);
        }

        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(Guid cartId)
        {
            // get cart by id
            return Ok();
        }
    }
}