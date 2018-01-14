extern alias Client;
using Client::ItemsBasket.Client.Interfaces;
using Moq;
using System.Collections.Generic;

namespace ItemsBasket.Client.Tests
{
    public static class TestEnvironmentSetupHelper
    {
        public static IEnvironmentService CreateMockEnvironmentService()
        {
            var enviornmentService = new Mock<IEnvironmentService>();

            enviornmentService
                .SetupGet(s => s.ServiceEndpoints)
                .Returns(
                    new Dictionary<KnownService, string>
                    {
                        { KnownService.AuthenticationService, "/api/Authentication" },
                        { KnownService.UserService, "api/Users" },
                        { KnownService.BasketService, "api/BasketItems" },
                    });

            return enviornmentService.Object;
        }
    }
}