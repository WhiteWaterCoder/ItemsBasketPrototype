extern alias Client;
using Client::ItemsBasket.Client.Extensions;
using Client::ItemsBasket.Client.Interfaces;
using FluentAssertions;
using ItemsBasket.BasketService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Moq;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ClientAuthenticationService = Client::ItemsBasket.Client.AuthenticationService;
using ClientBasketService = Client::ItemsBasket.Client.BasketService;

namespace ItemsBasket.Client.Tests
{
    public class BasketIntegrationTests
    {
        private readonly TestServer _authenticationServer;
        private readonly TestServer _basketServer;
        private readonly HttpClient _authenticationClient;
        private readonly HttpClient _basketClient;
        private readonly IAuthenticationService _authenticationService;
        private readonly IBasketService _basketService;
        private readonly IHttpClientProvider _basketHttpClientProvider;

        public BasketIntegrationTests()
        {
            _authenticationServer = new TestServer(new WebHostBuilder().UseStartup<AuthenticationService.Startup>());
            _authenticationClient = _authenticationServer.CreateClient();

            _basketServer = new TestServer(new WebHostBuilder().UseStartup<BasketService.Startup>());
            _basketClient = _basketServer.CreateClient();

            var environmentService = TestEnvironmentSetupHelper.CreateMockEnvironmentService();

            var authenticationClientProvider = new Mock<IHttpClientProvider>();
            authenticationClientProvider.SetupGet(p => p.NonAuthenticatedClient).Returns(_authenticationClient);

            _authenticationService = new ClientAuthenticationService(environmentService, authenticationClientProvider.Object);

            var basketHttpClientProvider = new Mock<IHttpClientProvider>();
            basketHttpClientProvider.SetupGet(p => p.AuthenticatedClient).Returns(_basketClient);

            _basketHttpClientProvider = basketHttpClientProvider.Object;

            _basketService = new ClientBasketService(environmentService, _basketHttpClientProvider, true);
        }

        [Fact]
        public async Task BasketActions_WorkAsExpected()
        {
            const int Item1Id = 1;
            const int Item2Id = 2;
            const int Item3Id = 3;
            const int Item4Id = 4;

            // First log on as admin user
            var response = await _authenticationService.TryLogin("admin", "pass");

            response.IsSuccessful.Should().BeTrue();

            // Simulate the call to _basketHttpClientProvider.SetAuthentication(string token)
            _basketHttpClientProvider.AuthenticatedClient.AddAuthorizationHeader(response.Item.Token);

            // Start by adding a single item
            var addItemResponse = await _basketService.AddItemToBasket(new BasketItem(Item1Id, 1));

            addItemResponse.IsSuccessful.Should().BeTrue();
            addItemResponse.ErrorMessage.Should().BeEmpty();

            await AssertItemsInBasket(1, 1);

            // Update the quantity
            var updateItemResponse = await _basketService.UpdateBasketItem(new BasketItem(Item1Id, 2));

            updateItemResponse.IsSuccessful.Should().BeTrue();
            updateItemResponse.ErrorMessage.Should().BeEmpty();

            await AssertItemsInBasket(1, 2);

            // Add 3 more items. 1 by "updating it"
            addItemResponse = await _basketService.AddItemToBasket(new BasketItem(Item2Id, 1));
            addItemResponse = await _basketService.AddItemToBasket(new BasketItem(Item3Id, 1));
            await _basketService.UpdateBasketItem(new BasketItem(Item4Id, 1));

            await AssertItemsInBasket(4, 5);

            // Remove 1 item from basket directly
            var removeResponse = await _basketService.RemoveItemFromBasket(new BasketItem(Item4Id, 1));

            removeResponse.IsSuccessful.Should().BeTrue();
            removeResponse.ErrorMessage.Should().BeEmpty();

            await AssertItemsInBasket(3, 4);

            // Remove 1 item by updating quantity to zero
            await _basketService.UpdateBasketItem(new BasketItem(Item3Id, 0));

            await AssertItemsInBasket(2, 3);

            // Test clearing out basket
            var clearResponse = await _basketService.ClearBasket();

            clearResponse.IsSuccessful.Should().BeTrue();
            clearResponse.ErrorMessage.Should().BeEmpty();

            await AssertItemsInBasket(0, 0);
        }

        private async Task AssertItemsInBasket(int expectedItems, int toalExpectedQuantity)
        {
            var listItemsResponse = await _basketService.ListBasketItems();

            listItemsResponse.IsSuccessful.Should().BeTrue();
            listItemsResponse.ErrorMessage.Should().BeEmpty();
            listItemsResponse.Item.Count().Should().Be(expectedItems);
            
            int totalQuantity = 0;
            foreach(var item in listItemsResponse.Item)
            {
                totalQuantity += item.Quantity;
            }
            totalQuantity.Should().Be(toalExpectedQuantity);
        }
    }
}
