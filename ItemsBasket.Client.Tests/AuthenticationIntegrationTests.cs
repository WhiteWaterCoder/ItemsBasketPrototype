extern alias Client;
using Client::ItemsBasket.Client.Interfaces;
using FluentAssertions;
using ItemsBasket.AuthenticationService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ClientAuthenticationService = Client::ItemsBasket.Client.AuthenticationService;
using ClientUsersService = Client::ItemsBasket.Client.UsersService;
using ClientUser = Client::ItemsBasket.AuthenticationService.Models.User;
using Xunit;

namespace ItemsBasket.Client.Tests
{
    public class AuthenticationIntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUsersService _usersService;

        public AuthenticationIntegrationTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();

            var environmentService = TestEnvironmentSetupHelper.CreateMockEnvironmentService();

            var clientProvider = new Mock<IHttpClientProvider>();
            clientProvider.SetupGet(p => p.NonAuthenticatedClient).Returns(_client);

            _authenticationService = new ClientAuthenticationService(environmentService, clientProvider.Object);

            _usersService = new ClientUsersService(environmentService, clientProvider.Object);
        }

        [Fact]
        public async Task WhenAuthenticatingAnExistingUser_ThenTrueIsReturned()
        {
            // We know we have hard-coded the admin user so should always return successful
            var response = await _authenticationService.TryLogin("admin", "pass");

            response.IsSuccessful.Should().BeTrue();
            response.ErrorMessage.Should().BeEmpty();
            response.Item.Token.Should().NotBeEmpty();
        }

        [Fact]
        public async Task WhenAuthenticatingANonExistingUser_ThenFalseIsReturned()
        {
            var response = await _authenticationService.TryLogin("nonexistinguser", "blah");

            response.IsSuccessful.Should().BeFalse();
            response.ErrorMessage.Should().Be("No user found with username = nonexistinguser");
            response.Item.Token.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task UserAccountLifeCycle_WorksAsExpected()
        {
            var randomUserId = Guid.NewGuid().ToString();
            var randomPassword = Guid.NewGuid().ToString();

            // First check that account is not already there
            var loginResponse = await _authenticationService.TryLogin(randomUserId, randomPassword);

            loginResponse.IsSuccessful.Should().BeFalse();
            loginResponse.ErrorMessage.Should().Be($"No user found with username = {randomUserId}");

            // Now create account
            var createUserResponse = await _usersService.CreateUser(new ClientUser(-1, randomUserId, randomPassword));

            createUserResponse.UserId.Should().BeGreaterThan(1);
            createUserResponse.Username.Should().Be(randomUserId);
            createUserResponse.Password.Should().Be(randomPassword);

            // Check we can loggin with this account
            loginResponse = await _authenticationService.TryLogin(randomUserId, randomPassword);

            loginResponse.IsSuccessful.Should().BeTrue();
            loginResponse.ErrorMessage.Should().BeEmpty();

            // Modify the username and password
            var modifiedUserId = Guid.NewGuid().ToString();
            var modifiedPassword = Guid.NewGuid().ToString();

            var modifyUserResponse = await _usersService.UpdateUser(new ClientUser(createUserResponse.UserId, modifiedUserId, modifiedPassword));

            modifyUserResponse.UserId.Should().Be(createUserResponse.UserId);
            modifyUserResponse.Username.Should().Be(modifiedUserId);
            modifyUserResponse.Password.Should().Be(modifiedPassword);

            // Check we can loggin with the modified details to this account
            loginResponse = await _authenticationService.TryLogin(modifiedUserId, modifiedPassword);

            loginResponse.IsSuccessful.Should().BeTrue();
            loginResponse.ErrorMessage.Should().BeEmpty();

            // Now delete account
            var deleteUserResponse = await _usersService.DeleteUser(new ClientUser(createUserResponse.UserId, modifiedUserId, modifiedPassword));

            deleteUserResponse.Should().Be(ClientUser.Empty);

            // Finally check we cannot login with the details
            loginResponse = await _authenticationService.TryLogin(modifiedUserId, modifiedPassword);

            loginResponse.IsSuccessful.Should().BeFalse();
            loginResponse.ErrorMessage.Should().Be($"No user found with username = {modifiedUserId}");
        }
    }
}