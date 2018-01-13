using ItemsBasket.BasketService.Models;
using ItemsBasket.AuthenticationService.Controllers;
using System.Collections.Generic;

namespace ItemsBasket.BasketService.Responses
{
    public class GetUserBasketItemsResponse : BaseResponse<IEnumerable<BasketItem>>
    {
        private static readonly List<BasketItem> EmptyList = new List<BasketItem>();

        public GetUserBasketItemsResponse(List<BasketItem> item, bool isSuccessful, string errorMessage) 
            : base(item, isSuccessful, errorMessage)
        {
        }

        public static GetUserBasketItemsResponse CreateSuccessfulResult(List<BasketItem> basketItems)
        {
            return new GetUserBasketItemsResponse(basketItems, true, "");
        }

        public static GetUserBasketItemsResponse CreateFailedResult(string errorMessage)
        {
            return new GetUserBasketItemsResponse(EmptyList, false, errorMessage);
        }
    }
}