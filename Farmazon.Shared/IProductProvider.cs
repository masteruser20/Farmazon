namespace Farmazon.Shared;

public interface IProductProvider
{
    Task<bool> ProductExistsAsync(Guid productId, CancellationToken cancellationToken);
    Task<int> GetProductStockAsync(Guid productId, CancellationToken cancellationToken);
    Task<string> GetProductInfoAsync(Guid productId, CancellationToken cancellationToken);
}