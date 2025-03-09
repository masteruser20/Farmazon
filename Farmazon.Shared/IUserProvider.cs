namespace Farmazon.Shared;

public interface IUserProvider
{
    Task<object> GetCustomerAsync(object[] customerId);
}