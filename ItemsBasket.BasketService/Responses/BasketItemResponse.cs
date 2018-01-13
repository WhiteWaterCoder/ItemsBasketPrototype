using ItemsBasket.AuthenticationService.Controllers;
using Newtonsoft.Json;

namespace ItemsBasket.BasketService.Responses
{
    public class BasketItemResponse : BaseResponse
    {
        public BasketItemResponse(bool isSuccessful)
            : this(isSuccessful, "")
        {
        }

        [JsonConstructor]
        public BasketItemResponse(bool isSuccessful, string errorMessage) 
            : base(isSuccessful, errorMessage)
        {
        }
    }
}