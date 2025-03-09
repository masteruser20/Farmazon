using Farmazon.OrderService.App.Interfaces;

namespace Farmazon.OrderService.Infrastructure;

public class OrderRepository : IOrderRepository
{
    public Task<Guid> CreateOrderAsync(Guid userId, object[] cartItems, CancellationToken cancellationToken)
    {
        var orderId = Guid.NewGuid();

        // context.Orders.Add(order); with items
        // context.SaveChangesAsync();

        return Task.FromResult(orderId);
    }
}