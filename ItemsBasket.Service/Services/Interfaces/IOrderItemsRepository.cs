using ItemsBasket.Common.Models;
using ItemsBasket.Service.Models;
using System.Collections.Generic;

namespace ItemsBasket.Service.Services.Interfaces
{
    public interface IOrderItemsRepository
    {
        IEnumerable<OrderItem> ListOrderItems(User user);
    }
}