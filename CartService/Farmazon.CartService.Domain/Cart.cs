namespace Farmazon.CartService.Domain;

public class Cart(Guid id, Guid userId)
{
    public Guid Id { get; set; } = id;
    public Guid UserId { get; set; } = userId;
    public List<CartItem?> Items { get; } = new();

    public void AddItem(CartItem? item)
    {
        Items.Add(item);
    }

    public CartItem? FindItem(Guid productId)
    {
        return Items.FirstOrDefault(c => c.ProductId == productId);
    }
}