using Farmazon.CartService.App.Commands;
using Farmazon.CartService.App.Interfaces;
using Farmazon.CartService.Domain;
using Farmazon.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Farmazon.CartService.App.Handlers
{
    public class AddProductToCartHandler(
        ICartRepository cartRepository,
        ILogger logger,
        IProductProvider productProvider) : IRequestHandler<AddProductToCartCommand, bool>
    {
        public async Task<bool> Handle(AddProductToCartCommand command, CancellationToken cancellationToken)
        {
            var mockedUserId = Guid.NewGuid(); // from userservice
            try
            {
                // fluentvalidation before this 

                // Check if cart exists, create if it doesn't
                var cart = await cartRepository.GetByIdAsync(command.CartId, cancellationToken);
                if (cart == null)
                {
                    cart = new Cart(command.CartId, mockedUserId);
                    await cartRepository.CreateAsync(cart, cancellationToken);
                }

                //  if exists
                var productExists = await productProvider.ProductExistsAsync(command.ProductId, cancellationToken);
                if (!productExists)
                {
                    return false;
                }

                // if stock is available
                var stockInfo = await productProvider.GetProductStockAsync(command.ProductId, cancellationToken);
                if (stockInfo < command.Quantity)
                {
                    return false;
                }

                var existingItem = cart.FindItem(command.ProductId);
                if (existingItem != null)
                {
                    existingItem.UpdateQuantity(existingItem.Quantity + command.Quantity);
                }
                else
                {
                    var cartItem = new CartItem(Guid.NewGuid(), command.CartId, command.ProductId, command.Quantity);
                    cart.AddItem(cartItem);
                }

                // Save
                await cartRepository.UpdateAsync(cart, cancellationToken);

                return false;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding item to cart");
                return false;
            }
        }
    }
}