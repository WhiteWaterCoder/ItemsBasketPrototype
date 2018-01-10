using System.Collections.Generic;

namespace ItemsBasket.Common.Models
{
    public class Item
    {
        public int Id { get; }
        public string Description { get; }
        public decimal Price { get; }

        public Item(int id, string description, decimal price)
        {
            Id = id;
            Description = description;
            Price = price;
        }

        public override string ToString()
        {
            return Description;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Item;
            return item != null &&
                   Id == item.Id &&
                   Description == item.Description &&
                   Price == item.Price;
        }

        public override int GetHashCode()
        {
            var hashCode = 1836259643;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            return hashCode;
        }
    }
}