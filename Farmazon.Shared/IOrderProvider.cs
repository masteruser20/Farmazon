namespace Farmazon.Shared;

public interface IOrderProvider
{
    Task<IEnumerable<object[]>> GetCustomersWithTransactionsAsync(DateTime startDate, DateTime endDate);
    Task<object> GetCustomerTransactionsAsync(object[] customerId, DateTime startDate, DateTime endDate);
}