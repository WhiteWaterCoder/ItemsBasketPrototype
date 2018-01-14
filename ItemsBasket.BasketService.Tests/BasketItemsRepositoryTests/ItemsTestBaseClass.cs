using ItemsBasket.BasketService.Services;
using ItemsBasket.Client.Interfaces;
using ItemsBasket.Common.Models;
using ItemsBasket.ItemsService.Responses;
using Moq;
using System.Collections.Generic;

namespace ItemsBasket.BasketService.Tests.BasketItemsRepositoryTests
{
    public class ItemsTestBaseClass
    {
        protected BasketItemsRepository CreateBasketItemsRepository(int numberOfItems)
        {
            var itemsService = new Mock<IItemsService>();

            var itemDetails = new List<ItemDetails>();
            for (int i = 0; i < numberOfItems; i++)
            {
                itemDetails.Add(new ItemDetails(i, $"Item {i}", 10));
            }

            var response = GetItemsResponse.CreateSuccessfulResult(itemDetails);

            itemsService
                .Setup(s => s.GetItems(It.IsAny<List<int>>()))
                .ReturnsAsync(response);

            return new BasketItemsRepository(itemsService.Object);
        }
    }
}