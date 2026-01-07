using StoreApi.Models;

namespace StoreApi.Repositories
{
    public interface IOrderRepository
    {
        Task<bool?> BuyCartAsync(int userId);
        Task<List<Order>?> GetAllOrdersAsync();
        Task<List<Order>?> GetOrdersByUserIdAsync(int userId);
    }
}