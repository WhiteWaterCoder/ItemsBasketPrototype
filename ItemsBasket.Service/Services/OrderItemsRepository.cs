using System.Collections.Generic;
using ItemsBasket.Common.Models;
using ItemsBasket.Service.Models;
using ItemsBasket.Service.Services.Interfaces;

namespace ItemsBasket.Service.Services
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        public IEnumerable<OrderItem> ListOrderItems(User user)
        {
            return null;
        }
    }
}