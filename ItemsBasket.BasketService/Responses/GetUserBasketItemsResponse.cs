using ItemsBasket.BasketService.Models;
using ItemsBasket.Common.Middleware;
using System.Collections.Generic;

namespace ItemsBasket.BasketService.Responses
{
    public class GetUserBasketItemsResponse : BaseResponse<IEnumerable<BasketItem>>
    {
        private static readonly List<BasketItem> EmptyList = new List<BasketItem>();

        protected GetUserBasketItemsResponse(IEnumerable<BasketItem> items, bool isSuccessful, string errorMessage) 
            : base(items, isSuccessful, errorMessage)
        {
        }

        public static GetUserBasketItemsResponse CreateSuccessfulResult(IEnumerable<BasketItem> basketItems)
        {
            return new GetUserBasketItemsResponse(basketItems, true, "");
        }

        public static GetUserBasketItemsResponse CreateFailedResult(string errorMessage)
        {
            return new GetUserBasketItemsResponse(EmptyList, false, errorMessage);
        }
    }
}