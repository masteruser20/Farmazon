namespace Farmazon.Shared;

public interface ICartProvider
{
    object[] GetCartWithItems(Guid userId);
}