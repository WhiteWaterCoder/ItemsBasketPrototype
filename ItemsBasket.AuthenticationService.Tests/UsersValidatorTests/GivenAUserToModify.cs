using ItemsBasket.AuthenticationService.Services;
using ItemsBasket.Common.Models;
using Xunit;

namespace ItemsBasket.AuthenticationService.Tests.UsersValidatorTests
{
    public class GivenAUserToModify
    {
        [Fact]
        public void WhenModifyingUserIdIsAdminId_ThenReturnFalse()
        {
            var validator = new UsersValidator();

            var result = validator.IsAuthorizedToModify(new User(1, "", "", ""), null, out string errorMessage);

            Assert.False(result);
            Assert.Equal($"Noone is authorized to modify the admin user.", errorMessage);
        }

        [Fact]
        public void WhenModifyingUserNameIsAdminId_ThenReturnFalse()
        {
            var validator = new UsersValidator();

            var result = validator.IsAuthorizedToModify(new User(2, UsersValidator.AdminUserName, "", ""), null, out string errorMessage);

            Assert.False(result);
            Assert.Equal($"Noone is authorized to modify the admin user.", errorMessage);
        }

        [Fact]
        public void IfPasswordsDoNotMatch_TheReturnFalse()
        {
            var validator = new UsersValidator();

            var userToModify = new User(2, "user", "pass", "");
            var existingUser = new User(2, "user", "differentpass", "");

            var result = validator.IsAuthorizedToModify(userToModify, existingUser, out string errorMessage);

            Assert.False(result);
            Assert.Equal($"Password for user with ID = {userToModify.Id} is incorrect. You are not authorized to modify the account.", errorMessage);
        }

        [Fact]
        public void IfSecurityTokensDoNotMatch_TheReturnFalse()
        {
            var validator = new UsersValidator();

            var userToModify = new User(2, "user", "pass", "123");
            var existingUser = new User(2, "user", "pass", "321");

            var result = validator.IsAuthorizedToModify(userToModify, existingUser, out string errorMessage);

            Assert.False(result);
            Assert.Equal($"Security token for user with ID = {userToModify.Id} is incorrect. You are not authorized to modify the account.", errorMessage);
        }

        [Fact]
        public void IfPasswordAndSecurityTokensMatch_TheReturnTrue()
        {
            var validator = new UsersValidator();

            var userToModify = new User(2, "user", "pass", "123");
            var existingUser = new User(2, "user", "pass", "123");

            var result = validator.IsAuthorizedToModify(userToModify, existingUser, out string errorMessage);

            Assert.True(result);
            Assert.Equal("", errorMessage);
        }
    }
}