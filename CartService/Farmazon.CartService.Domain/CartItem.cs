namespace Farmazon.CartService.Domain;

public class CartItem
{
    public Guid Id { get; private set; }
    public Guid CartId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public DateTime DateAdded { get; private set; }

    protected CartItem() { }

    public CartItem(Guid id, Guid cartId, Guid productId, int quantity)
    {
        Id = id;
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity > 0 ? quantity : throw new ArgumentException("Quantity must be positive", nameof(quantity));
        DateAdded = DateTime.UtcNow;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
        {
            throw new ArgumentException("Quantity must be positive", nameof(newQuantity));
        }

        Quantity = newQuantity;
    }
}