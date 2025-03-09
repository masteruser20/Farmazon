using Farmazon.CartService.Domain;

namespace Farmazon.CartService.App.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken);
    Task<bool> CreateAsync(Cart? cart, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Cart cart, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid cartId, CancellationToken cancellationToken);
}