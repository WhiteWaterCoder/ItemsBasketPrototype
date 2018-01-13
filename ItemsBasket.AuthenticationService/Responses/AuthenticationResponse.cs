using ItemsBasket.AuthenticationService.Controllers;
using ItemsBasket.AuthenticationService.Models;

namespace ItemsBasket.AuthenticationService.Responses
{
    /// <summary>
    /// The response object to be returned to an autrhentication request.
    /// </summary>
    public class AuthenticationResponse : BaseResponse<AuthenticatedUser>
    {
        public AuthenticationResponse(AuthenticatedUser item, bool isSuccessful, string errorMessage) 
            : base(item, isSuccessful, errorMessage)
        {
        }

        /// <summary>
        /// Create a successful response to an authentication request.
        /// </summary>
        /// <param name="item">The authenticated user details to be included in the response.</param>
        /// <returns>The response object with the payload set and success flag set to true.</returns>
        public static AuthenticationResponse CreateSuccessfulResult(AuthenticatedUser item)
        {
            return new AuthenticationResponse(item, true, "");
        }

        /// <summary>
        /// Create a failed response to an authentication request.
        /// </summary>
        /// <param name="errorMessage">The error message related to the failure.</param>
        /// <returns>The response object with the payload set to an empty authentication object and success flag set to false.</returns>
        public static AuthenticationResponse CreateFailedResult(string errorMessage)
        {
            return new AuthenticationResponse(AuthenticatedUser.Empty, false, errorMessage);
        }
    }
}
