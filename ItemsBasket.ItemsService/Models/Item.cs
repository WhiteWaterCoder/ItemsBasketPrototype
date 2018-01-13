namespace ItemsBasket.ItemsService.Models
{
    public class Item
    {
        public int ItemId { get; }
        public string Description { get; }
        public string ImageUrl { get; }
        public decimal Price { get; }

        public Item(int itemId, string description, string imageUrl, decimal price)
        {
            ItemId = itemId;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
        }
    }
}