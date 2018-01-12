using ItemsBasket.Common.Controllers;

namespace ItemsBasket.BasketService.Responses
{
    public class BasketItemResponse : BaseResponse
    {
        public BasketItemResponse(bool isSuccessful)
            : this(isSuccessful, "")
        {
        }

        public BasketItemResponse(bool isSuccessful, string errorMessage) 
            : base(isSuccessful, errorMessage)
        {
        }
    }
}