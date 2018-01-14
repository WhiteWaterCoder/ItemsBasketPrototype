using ItemsBasket.AuthenticationService.Controllers;
using Newtonsoft.Json;

namespace ItemsBasket.BasketService.Responses
{
    /// <summary>
    /// The response object when performing actions on basket items
    /// </summary>
    public class BasketItemResponse : BaseResponse
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isSuccessful">A flag to denote success/failure.</param>
        public BasketItemResponse(bool isSuccessful)
            : this(isSuccessful, "")
        {
        }

        /// <summary>
        /// Constructor. This is the default one used by Json.Net.
        /// </summary>
        /// <param name="isSuccessful">A flag to denote success/failure.</param>
        /// <param name="errorMessage">The error message if one occurred.</param>
        [JsonConstructor]
        public BasketItemResponse(bool isSuccessful, string errorMessage) 
            : base(isSuccessful, errorMessage)
        {
        }
    }
}