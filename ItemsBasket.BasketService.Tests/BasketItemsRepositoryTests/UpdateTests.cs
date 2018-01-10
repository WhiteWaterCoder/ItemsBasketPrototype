using FluentAssertions;
using ItemsBasket.BasketService.Models;
using ItemsBasket.BasketService.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ItemsBasket.BasketService.Tests.BasketItemsRepositoryTests
{
    public class GivenAUserWithNoExistingBasket : UpdatesBaseClass
    {
        [Fact]
        public async Task WhenUpdatingAnItemNotInTheBasket_ThenItemGetsAdded()
        {
            var repository = new BasketItemsRepository();

            await UpdateItemNotInBasketAndAssert(repository);
        }

        [Fact]
        public async Task WhenUpdatingAnItemInTheBasket_ThenItemGetsUpdated()
        {
            var repository = new BasketItemsRepository();

            await UpdateItemInBasketAndAssert(repository);
        }

        [Fact]
        public async Task WhenUpdatingAnItemInTheBasketToZeroQuantity_ThenItemGetsRemoved()
        {
            var repository = new BasketItemsRepository();

            await UpdateItemInBasketToZeroQuantityAndAssert(repository);
        }
    }

    public class GivenAUserWithAnExistingBasket : UpdatesBaseClass
    {
        [Fact]
        public async Task WhenUpdatingAnItemNotInTheBasket_ThenItemGetsAdded()
        {
            var repository = new BasketItemsRepository();

            await repository.CreateAndClearBasket(UserId);

            await UpdateItemNotInBasketAndAssert(repository);
        }

        [Fact]
        public async Task WhenUpdatingAnItemInTheBasket_ThenItemGetsUpdated()
        {
            var repository = new BasketItemsRepository();

            await repository.CreateAndClearBasket(UserId);

            await UpdateItemInBasketAndAssert(repository);
        }

        [Fact]
        public async Task WhenUpdatingAnItemInTheBasketToZeroQuantity_ThenItemGetsRemoved()
        {
            var repository = new BasketItemsRepository();

            await repository.CreateAndClearBasket(UserId);

            await UpdateItemInBasketToZeroQuantityAndAssert(repository);
        }
    }

    public class UpdatesBaseClass
    {
        protected const int UserId = 1;

        protected async Task UpdateItemNotInBasketAndAssert(BasketItemsRepository repository)
        {
            var updateResult = await repository.UpdateItem(UserId, new BasketItem(1, 2));

            updateResult.IsSuccessful.Should().Be(true);
            updateResult.ErrorMessage.Should().BeEmpty();

            var getResult = await repository.GetBasketItems(UserId);

            getResult.IsSuccessful.Should().Be(true);
            getResult.ErrorMessage.Should().BeEmpty();
            getResult.Item.Count().Should().Be(1);
            getResult.Item.ElementAt(0).ItemId.Should().Be(1);
            getResult.Item.ElementAt(0).Quantity.Should().Be(2);
        }

        protected async Task UpdateItemInBasketAndAssert(BasketItemsRepository repository)
        {
            await repository.UpdateItem(UserId, new BasketItem(1, 1));

            var updateResult = await repository.UpdateItem(UserId, new BasketItem(1, 2));

            updateResult.IsSuccessful.Should().Be(true);
            updateResult.ErrorMessage.Should().BeEmpty();

            var getResult = await repository.GetBasketItems(UserId);

            getResult.IsSuccessful.Should().Be(true);
            getResult.ErrorMessage.Should().BeEmpty();
            getResult.Item.Count().Should().Be(1);
            getResult.Item.ElementAt(0).ItemId.Should().Be(1);
            getResult.Item.ElementAt(0).Quantity.Should().Be(2);
        }

        protected async Task UpdateItemInBasketToZeroQuantityAndAssert(BasketItemsRepository repository)
        {
            await repository.UpdateItem(UserId, new BasketItem(1, 1));

            var updateResult = await repository.UpdateItem(UserId, new BasketItem(1, 0));

            updateResult.IsSuccessful.Should().Be(true);
            updateResult.ErrorMessage.Should().BeEmpty();

            var getResult = await repository.GetBasketItems(UserId);

            getResult.IsSuccessful.Should().Be(true);
            getResult.ErrorMessage.Should().BeEmpty();
            getResult.Item.Count().Should().Be(0);
        }
    }
}