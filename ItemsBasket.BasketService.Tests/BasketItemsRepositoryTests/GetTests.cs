using FluentAssertions;
using ItemsBasket.BasketService.Models;
using ItemsBasket.BasketService.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ItemsBasket.BasketService.Tests.GivenAUserId
{
    public class WhenRequestingAllBasketItems
    {
        [Fact]
        public async Task AndNoBasketExists_ThenNewEmptyBasketIsCreated()
        {
            var repository = new BasketItemsRepository();

            var result = await repository.GetBasketItems(1);

            result.IsSuccessful.Should().Be(true);
            result.ErrorMessage.Should().BeEmpty();
            result.Item.Count().Should().Be(0);
        }

        [Fact]
        public async Task AndBasketExists_ThenContentsShouldBeReturned()
        {
            var repository = new BasketItemsRepository();

            await repository.AddItem(1, new BasketItem(1, 2));

            var result = await repository.GetBasketItems(1);

            result.IsSuccessful.Should().Be(true);
            result.ErrorMessage.Should().BeEmpty();
            result.Item.Count().Should().Be(1);
            result.Item.ElementAt(0).ItemId.Should().Be(1);
            result.Item.ElementAt(0).Quantity.Should().Be(2);
        }
    }
}
