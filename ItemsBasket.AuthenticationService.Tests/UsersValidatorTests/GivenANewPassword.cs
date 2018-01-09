using ItemsBasket.AuthenticationService.Services;
using Xunit;

namespace ItemsBasket.AuthenticationService.Tests.UsersValidatorTests
{

    public class GivenANewPassword
    {
        [Fact]
        public void IfNull_ThenReturnFalse()
        {
            var validator = new UsersValidator();

            var result = validator.IsPasswordValid(null, out string errorMessage);

            Assert.False(result);
            Assert.Equal("A valid password of minimum 4 characters long needs to be supplied.", errorMessage);
        }

        [Fact]
        public void IfEmpty_ThenReturnFalse()
        {
            var validator = new UsersValidator();

            var result = validator.IsPasswordValid("", out string errorMessage);

            Assert.False(result);
            Assert.Equal("A valid password of minimum 4 characters long needs to be supplied.", errorMessage);
        }

        [Fact]
        public void IfLessThanFourCharactersLong_ThenReturnFalse()
        {
            var validator = new UsersValidator();

            var result = validator.IsPasswordValid("abc", out string errorMessage);

            Assert.False(result);
            Assert.Equal("A valid password of minimum 4 characters long needs to be supplied.", errorMessage);
        }

        [Fact]
        public void IfFourCharactersLong_ThenReturnTrue()
        {
            var validator = new UsersValidator();

            var result = validator.IsPasswordValid("abcd", out string errorMessage);

            Assert.True(result);
            Assert.Equal("", errorMessage);
        }
    }
}
