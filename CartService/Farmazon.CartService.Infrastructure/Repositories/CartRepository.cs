using Farmazon.CartService.App.Interfaces;
using Farmazon.CartService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Farmazon.CartService.Infrastructure.Repositories;

public class CartRepository(CartDbContext dbContext, ILogger<CartRepository> logger) : ICartRepository
{
    public async Task<Cart?> GetByIdAsync(Guid cartId, CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error");
            throw;
        }
    }

    public async Task<bool> CreateAsync(Cart? cart, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Carts.AddAsync(cart, cancellationToken);
            var result = await dbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error");
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Cart cart, CancellationToken cancellationToken)
    {
        try
        {
            dbContext.Entry(cart).State = EntityState.Modified;

            foreach (var item in cart.Items)
            {
                var state = dbContext.Entry(item).State;
                if (state == EntityState.Detached)
                {
                    dbContext.CartItems.Add(item);
                }
                else
                {
                    dbContext.Entry(item).State = EntityState.Modified;
                }
            }

            var result = await dbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error");
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid cartId, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await dbContext.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId, cancellationToken);

            if (cart == null)
            {
                return false;
            }

            dbContext.CartItems.RemoveRange(cart.Items);
            dbContext.Carts.Remove(cart);

            var result = await dbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error");
            throw;
        }
    }
}