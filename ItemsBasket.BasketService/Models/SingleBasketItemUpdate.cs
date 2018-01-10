namespace ItemsBasket.BasketService.Models
{
    public class SingleBasketItemUpdate
    {
        public int UserId { get; }
        public string SecurityToken { get; }
        public BasketItem Item { get; }

        public SingleBasketItemUpdate(int userId, string securityToken, BasketItem item)
        {
            UserId = userId;
            SecurityToken = securityToken;
            Item = item;
        }
    }
}