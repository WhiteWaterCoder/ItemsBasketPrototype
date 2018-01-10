using System.Collections.Generic;

namespace ItemsBasket.BasketService.Models
{
    public class BasketItemsUpdate
    {
        public int UserId { get; }
        public string SecurityToken { get; }
        public List<BasketItem> Items { get; }
    }
}