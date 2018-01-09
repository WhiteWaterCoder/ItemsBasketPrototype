using ItemsBasket.AuthenticationService.Services;
using ItemsBasket.Common.Models;
using System.Collections.Generic;
using Xunit;

namespace ItemsBasket.AuthenticationService.Tests.UsersValidatorTests
{
    public class GivenAUsername
    {
        [Fact]
        public void IfItAlreadyExists_ThenReturnFalse()
        {
            var validator = new UsersValidator();

            const string duplicateUser = "Duplicate";

            var context = new Dictionary<int, User>
            {
                { 1, new User(1, duplicateUser, "", "") }
            };

            var result = validator.IsUsernameUnique(duplicateUser, context, out string errorMessage);

            Assert.False(result);
            Assert.Equal($"Username {duplicateUser} is not unique. Please select a unique username.", errorMessage);
        }

        [Fact]
        public void IfItIsAvailable_ThenReturnTrue()
        {
            var validator = new UsersValidator();

            var context = new Dictionary<int, User>
            {
                { 1, new User(1, "anotherUser", "", "") }
            };

            var result = validator.IsUsernameUnique("newUser", context, out string errorMessage);

            Assert.True(result);
            Assert.Equal("", errorMessage);
        }
    }
}
