using FluentAssertions;
using ItemsBasket.BasketService.Models;
using ItemsBasket.BasketService.Services;
using ItemsBasket.BasketService.Tests.BasketItemsRepositoryTests;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ItemsBasket.BasketService.Tests.GivenAUserId
{
    public class GivenAUserWithNoExistingBasket : AddBaseClass
    {
        [Fact]
        public async Task WhenAddingANewItem_ThenItemGetsAdded()
        {
            var repository = new BasketItemsRepository();

            await AddNewItemAndAssert(repository);
        }

        [Fact]
        public async Task WhenAddingAnExistingItem_ThenExistingItemGetsOverwritten()
        {
            var repository = new BasketItemsRepository();

            await AddExistingItemAndAssert(repository);
        }
    }

    public class GivenAUserWithAnExistingBasket : AddBaseClass
    {
        [Fact]
        public async Task WhenAddingANewItem_ThenItemGetsAdded()
        {
            var repository = new BasketItemsRepository();

            await repository.CreateAndClearBasket(UserId);

            await AddNewItemAndAssert(repository);
        }

        [Fact]
        public async Task WhenAddingAnExistingItem_ThenExistingItemGetsOverwritten()
        {
            var repository = new BasketItemsRepository();

            await repository.CreateAndClearBasket(UserId);

            await AddExistingItemAndAssert(repository);
        }
    }

    public class AddBaseClass
    {
        protected int UserId = 1;

        protected async Task AddNewItemAndAssert(BasketItemsRepository repository)
        {
            var addResult = await repository.AddItem(UserId, new BasketItem(2, 1));

            addResult.IsSuccessful.Should().Be(true);
            addResult.ErrorMessage.Should().BeEmpty();

            var getResult = await repository.GetBasketItems(UserId);

            getResult.IsSuccessful.Should().Be(true);
            getResult.ErrorMessage.Should().BeEmpty();
            getResult.Item.Count().Should().Be(1);
            getResult.Item.ElementAt(0).ItemId.Should().Be(2);
            getResult.Item.ElementAt(0).Quantity.Should().Be(1);
        }

        protected async Task AddExistingItemAndAssert(BasketItemsRepository repository)
        {
            await repository.AddItem(UserId, new BasketItem(2, 1));

            var addResult = await repository.AddItem(UserId, new BasketItem(2, 3));

            addResult.IsSuccessful.Should().Be(true);
            addResult.ErrorMessage.Should().BeEmpty();

            var getResult = await repository.GetBasketItems(UserId);

            getResult.IsSuccessful.Should().Be(true);
            getResult.ErrorMessage.Should().BeEmpty();
            getResult.Item.Count().Should().Be(1);
            getResult.Item.ElementAt(0).ItemId.Should().Be(2);
            getResult.Item.ElementAt(0).Quantity.Should().Be(3);
        }
    }
}