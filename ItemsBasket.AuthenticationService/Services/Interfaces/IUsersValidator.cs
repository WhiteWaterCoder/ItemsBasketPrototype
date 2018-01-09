using ItemsBasket.Common.Models;
using System.Collections.Generic;

namespace ItemsBasket.AuthenticationService.Services.Interfaces
{
    public interface IUsersValidator
    {
        bool IsUsernameUnique(string userName, IDictionary<int, User> context, out string errorMessage);

        bool IsPasswordValid(string password, out string errorMessage);

        bool IsAuthorizedToModify(User user, User existingUser, out string notAuthorizedMessage);
    }
}