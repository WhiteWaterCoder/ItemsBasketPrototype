using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ItemsBasket.Client.Extensions
{
    public static class HttpClientExtensions
    {
        public static void AddAuthorizationHeader(this HttpClient httpClient)
        {
            if (string.IsNullOrEmpty(Session.Token))
            {
                throw new InvalidOperationException("You first need to log on to perform this action.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session.Token);
        }
    }
}