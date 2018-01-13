using ItemsBasket.AuthenticationService.Models;
using ItemsBasket.AuthenticationService.Controllers;

namespace ItemsBasket.AuthenticationService.Responses
{
    public class UserResponse : BaseResponse<User>
    {
        public UserResponse(User item, bool isSuccessful, string errorMessage) 
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