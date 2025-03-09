namespace Farmazon.OrderService.App.Interfaces;

public interface IOrderRepository
{
    Task<Guid> CreateOrderAsync(Guid userId, object[] cartItems, CancellationToken cancellationToken);
}