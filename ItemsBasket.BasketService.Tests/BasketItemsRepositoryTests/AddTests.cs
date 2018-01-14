using FluentAssertions;
using ItemsBasket.BasketService.Models;
using ItemsBasket.BasketService.Services;
using ItemsBasket.BasketService.Tests.BasketItemsRepositoryTests;
using ItemsBasket.Client.Interfaces;
using ItemsBasket.Common.Models;
using Moq;
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
            var repository = CreateBasketItemsRepository(1);

            await AddNewItemAndAssertItWasAdded(repository);
        }

        [Fact]
        public async Task WhenAddingANewInvalidItem_ThenItemGetsAddedButNoDetailsAreReturned()
        {
            var repository = CreateBasketItemsRepository(0);

            await AddNewItemAndAssertItWasAddedWithNoDetails(repository);
        }

        [Fact]
        public async Task WhenAddingAnAlreadyAddedItem_ThenExistingItemGetsOverwritten()
        {
            var repository = CreateBasketItemsRepository(1);

            await AddExistingItemAndAssert(repository);
        }
    }

    public class GivenAUserWithAnExistingBasket : AddBaseClass
    {
        [Fact]
        public async Task WhenAddingANewItem_ThenItemGetsAdded()
        {
            var repository = CreateBasketItemsRepository(1);

            await repository.CreateAndClearBasket(UserId);

            await AddNewItemAndAssertItWasAdded(repository);
        }

        [Fact]
        public async Task WhenAddingANewInvalidItem_ThenItemGetsAddedButNoDetailsAreReturned()
        {
            var repository = CreateBasketItemsRepository(0);

            await repository.CreateAndClearBasket(UserId);

            await AddNewItemAndAssertItWasAddedWithNoDetails(repository);
        }

        [Fact]
        public async Task WhenAddingAnExistingItem_ThenExistingItemGetsOverwritten()
        {
            var repository = CreateBasketItemsRepository(1);

            await repository.CreateAndClearBasket(UserId);

            await AddExistingItemAndAssert(repository);
        }
    }

    public class AddBaseClass : ItemsTestBaseClass
    {
        protected int UserId = 1;

        protected async Task AddNewItemAndAssertItWasAddedWithNoDetails(BasketItemsRepository repository)
        {
            var addResult = await repository.AddItem(UserId, new BasketItem(2, 1));

            addResult.IsSuccessful.Should().Be(true);
            addResult.Item.Count.Should().Be(1);
            addResult.Item[0].ItemId.Should().Be(2);
            addResult.Item[0].Description.Should().Be("Item description could not be found");
            addResult.Item[0].Price.Should().Be(-1);
            addResult.Item[0].Quantity.Should().Be(1);
            addResult.ErrorMessage.Should().BeEmpty();

            var getResult = await repository.GetBasketItems(UserId);

            getResult.IsSuccessful.Should().Be(true);
            getResult.ErrorMessage.Should().BeEmpty();
            getResult.Item.Count().Should().Be(1);
            getResult.Item.ElementAt(0).ItemId.Should().Be(2);
            getResult.Item.ElementAt(0).Quantity.Should().Be(1);
        }

        protected async Task AddNewItemAndAssertItWasAdded(BasketItemsRepository repository)
        {
            var addResult = await repository.AddItem(UserId, new BasketItem(2, 1));

            addResult.IsSuccessful.Should().Be(true);
            addResult.Item.Count.Should().Be(1);
            addResult.Item[0].ItemId.Should().Be(2);
            addResult.Item[0].Description.Should().NotBe("Item description could not be found");
            addResult.Item[0].Price.Should().NotBe(-1);
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