using ItemsBasket.AuthenticationService.Models;
using ItemsBasket.Common.Controllers;

namespace ItemsBasket.AuthenticationService.Responses
{
    public class UserResponse : BaseResponse<User>
    {
        protected UserResponse(User item, bool isSuccessful, string errorMessage) 
            : base(item, isSuccessful, errorMessage)
        {
        }

        public static UserResponse CreateSuccessfulResult(User user)
        {
            return new UserResponse(user, true, "");
        }

        public static UserResponse CreateFailedResult(string errorMessage)
        {
            return new UserResponse(User.Empty, false, errorMessage);
        }
    }
}