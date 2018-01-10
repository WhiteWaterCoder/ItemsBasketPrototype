using ItemsBasket.BasketService.Models;
using ItemsBasket.BasketService.Services;
using System.Threading.Tasks;

namespace ItemsBasket.BasketService.Tests.BasketItemsRepositoryTests
{
    public static class BasketItemsRepositoryTestExtensions
    {
        public static async Task CreateAndClearBasket(this BasketItemsRepository repository, int userId)
        {
            // Simulate creation of basket
            await repository.AddItem(userId, new BasketItem(1, 2));
            await repository.ClearItems(userId);
        }
    }
}