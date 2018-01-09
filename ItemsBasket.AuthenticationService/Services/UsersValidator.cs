using System.Collections.Generic;
using System.Linq;
using ItemsBasket.AuthenticationService.Services.Interfaces;
using ItemsBasket.Common.Models;

namespace ItemsBasket.AuthenticationService.Services
{
    public class UsersValidator : IUsersValidator
    {
        public const string AdminUserName = "admin";

        public bool IsUsernameUnique(string userName, IDictionary<int, User> context, out string errorMessage)
        {
            if (context.Values.Any(v => string.Equals(v.UserName, userName)))
            {
                errorMessage = $"Username {userName} is not unique. Please select a unique username.";
                return false;
            }

            errorMessage = "";
            return true;
        }

        public bool IsPasswordValid(string password, out string errorMessage)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 4)
            {
                errorMessage = "A valid password of minimum 4 characters long needs to be supplied.";
                return false;
            }

            errorMessage = "";
            return true;
        }

        public bool IsAuthorizedToModify(User user, User existingUser, out string notAuthorizedMessage)
        {
            if (user.Id == 1 || string.Equals(user.UserName, AdminUserName))
            {
                notAuthorizedMessage = $"Noone is authorized to modify the admin user.";
                return false;
            }

            if (!string.Equals(user.Password, existingUser.Password))
            {
                notAuthorizedMessage = $"Password for user with ID = {user.Id} is incorrect. You are not authorized to modify the account.";
                return false;
            }

            if (!string.Equals(user.SecurityToken, existingUser.SecurityToken))
            {
                notAuthorizedMessage = $"Security token for user with ID = {user.Id} is incorrect. You are not authorized to modify the account.";
                return false;
            }

            notAuthorizedMessage = "";
            return true;
        }
    }
}