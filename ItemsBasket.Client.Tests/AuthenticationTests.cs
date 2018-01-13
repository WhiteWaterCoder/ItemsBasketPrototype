using System.Net.Http;
using Xunit;
using Microsoft.AspNetCore.TestHost;

namespace ItemsBasket.BasketService.Tests.GivenAnAuthenticationWebApi
{
    public class WhenLoggingInWithWrongCredentials
    {
        //private readonly TestServer _server;
        private readonly HttpClient _client;

        public WhenLoggingInWithWrongCredentials()
        {
            //_server = null;
        }

        [Fact]
        public void ThenFalseIsReturned()
        {

        }
    }
}