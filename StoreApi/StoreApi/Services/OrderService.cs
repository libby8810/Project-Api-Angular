using System.Linq;
using StoreApi.DTOs;
using StoreApi.Models;
using StoreApi.Repositories;

namespace StoreApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService(IOrderRepository repository)
        {
            this._repository = repository;
        }

        // ביצוע רכישה של עגלת קניות
        public async Task<bool?> BuyCartAsync(int userId)
        {
            return await _repository.BuyCartAsync(userId);
        }

        // קבלת כל ההזמנות של משתמש
        public async Task<List<OrderDto>?> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _repository.GetOrdersByUserIdAsync(userId);
            if (orders == null)
                return null;

            return orders.Select(o => new OrderDto
            {
                id = o.id,
                UserId = o.UserId,
                listOrder = o.listOrder?.Select(pp => new ProductPurchasedDto
                {
                    Id = pp.Id,
                    GiftId = pp.GiftId,
                    UserId = pp.UserId,
                    Count = pp.Count
                }).ToList() ?? new List<ProductPurchasedDto>()
            }).ToList();
        }

        // קבלת כל ההזמנות במערכת
        public async Task<List<OrderDto>?> GetAllOrdersAsync()
        {
            var orders = await _repository.GetAllOrdersAsync();
            if (orders == null)
                return null;

            return orders.Select(o => new OrderDto
            {
                id = o.id,
                UserId = o.UserId,
                listOrder = o.listOrder?.Select(pp => new ProductPurchasedDto
                {
                    Id = pp.Id,
                    GiftId = pp.GiftId,
                    UserId = pp.UserId,
                    Count = pp.Count
                }).ToList() ?? new List<ProductPurchasedDto>()
            }).ToList();
        }
    }
}
