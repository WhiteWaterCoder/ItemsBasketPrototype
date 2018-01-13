using ItemsBasket.Client.Extensions;
using ItemsBasket.Client.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ItemsBasket.Client
{
    public abstract class BaseServiceCaller
    {
        private readonly IEnvironmentService _environmentService;
        private readonly IHttpClientProvider _httpClientProvider;

        protected IEnvironmentService EnvironmentService { get { return _environmentService; } }
        protected IHttpClientProvider HttpClientProvider { get { return _httpClientProvider; } }

        protected BaseServiceCaller(IEnvironmentService environmentService, IHttpClientProvider httpClientProvider)
        {
            _environmentService = environmentService;
            _httpClientProvider = httpClientProvider;
        }

        /// <summary>
        /// Performs a POST call to the API using no authentication.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object that will be sent to the API.</typeparam>
        /// <typeparam name="TRequestResponse">The type of object we expect back from the API.</typeparam>
        /// <typeparam name="TResponse">The response object returned after the call is complete.</typeparam>
        /// <param name="endpoint">The REST endpoint to make the call to.</param>
        /// <param name="requestObject">The contents of the POST body that will be sent to the API.</param>
        /// <param name="responseFunc">A func that will handle the API response end prepare the method return object.</param>
        /// <param name="exceptionFunc">The func that will be executed if an exception occurs.</param>
        /// <returns>A response object containing the result object.</returns>
        protected async Task<TResponse> PostNonAuthenticatedCall<TRequest, TRequestResponse, TResponse>(string endpoint,
            TRequest requestObject,
            Func<TRequestResponse, TResponse> responseFunc,
            Func<Exception, TResponse> exceptionFunc)
        {
            return await PerformHttpCall(endpoint,
                requestObject,
                (HttpClient httpClient, string uri, StringContent content) => { return httpClient.PostAsync(uri, content); },
                responseFunc,
                exceptionFunc,
                false);
        }

        /// <summary>
        /// Performs a PUT call to the API using no authentication.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object that will be sent to the API.</typeparam>
        /// <typeparam name="TRequestResponse">The type of object we expect back from the API.</typeparam>
        /// <typeparam name="TResponse">The response object returned after the call is complete.</typeparam>
        /// <param name="endpoint">The REST endpoint to make the call to.</param>
        /// <param name="requestObject">The contents of the PUT body that will be sent to the API.</param>
        /// <param name="responseFunc">A func that will handle the API response end prepare the method return object.</param>
        /// <param name="exceptionFunc">The func that will be executed if an exception occurs.</param>
        /// <returns>A response object containing the result object.</returns>
        protected async Task<TResponse> PutNonAuthenticatedCall<TRequest, TRequestResponse, TResponse>(string endpoint,
            TRequest requestObject,
            Func<TRequestResponse, TResponse> responseFunc,
            Func<Exception, TResponse> exceptionFunc)
        {
            return await PerformHttpCall(endpoint,
                requestObject,
                (HttpClient httpClient, string uri, StringContent content) => { return httpClient.PutAsync(uri, content); },
                responseFunc,
                exceptionFunc,
                false);
        }

        /// <summary>
        /// Performs a DELETE call to the API using no authentication.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object that will be sent to the API.</typeparam>
        /// <typeparam name="TRequestResponse">The type of object we expect back from the API.</typeparam>
        /// <typeparam name="TResponse">The response object returned after the call is complete.</typeparam>
        /// <param name="endpoint">The REST endpoint to make the call to.</param>
        /// <param name="requestObject">The contents of the DELETE body that will be sent to the API.</param>
        /// <param name="responseFunc">A func that will handle the API response end prepare the method return object.</param>
        /// <param name="exceptionFunc">The func that will be executed if an exception occurs.</param>
        /// <returns>A response object containing the result object.</returns>
        protected async Task<TResponse> DeleteNonAuthenticatedCall<TRequest, TRequestResponse, TResponse>(string endpoint,
            TRequest requestObject,
            Func<TRequestResponse, TResponse> responseFunc,
            Func<Exception, TResponse> exceptionFunc)
        {
            return await PerformHttpCall(endpoint,
                requestObject,
                (HttpClient httpClient, string uri, StringContent content) => { return httpClient.DeleteAsync(uri); },
                responseFunc,
                exceptionFunc,
                false);
        }

        /// <summary>
        /// Performs a POST call to the API using authentication. If the user is not yet authenicated 
        /// then an exception will be thrown.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object that will be sent to the API.</typeparam>
        /// <typeparam name="TRequestResponse">The type of object we expect back from the API.</typeparam>
        /// <typeparam name="TResponse">The response object returned after the call is complete.</typeparam>
        /// <param name="endpoint">The REST endpoint to make the call to.</param>
        /// <param name="requestObject">The contents of the POST body that will be sent to the API.</param>
        /// <param name="responseFunc">A func that will handle the API response end prepare the method return object.</param>
        /// <param name="exceptionFunc">The func that will be executed if an exception occurs.</param>
        /// <returns>A response object containing the result object.</returns>
        protected async Task<TResponse> PostAuthenticatedCall<TRequest, TRequestResponse, TResponse>(string endpoint,
            TRequest requestObject,
            Func<TRequestResponse, TResponse> responseFunc,
            Func<Exception, TResponse> exceptionFunc)
        {
            return await PerformHttpCall(endpoint,
                requestObject,
                (HttpClient httpClient, string uri, StringContent content) => { return httpClient.PostAsync(uri, content); },
                responseFunc,
                exceptionFunc,
                true);
        }

        /// <summary>
        /// Performs an HTTP call to the API.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object that will be sent to the API.</typeparam>
        /// <typeparam name="TRequestResponse">The type of object we expect back from the API.</typeparam>
        /// <typeparam name="TResponse">The response object returned after the call is complete.</typeparam>
        /// <param name="endpoint">The REST endpoint to make the call to.</param>
        /// <param name="requestObject">The contents of the POST body that will be sent to the API.</param>
        /// <param name="requestFunc">The func that will execute the HTTP call. This could be GET/POST/PUT/DELETE etc.</param>
        /// <param name="responseFunc">A func that will handle the API response end prepare the method return object.</param>
        /// <param name="exceptionFunc">The func that will be executed if an exception occurs.</param>
        /// <param name="authenticate">A flag to denote if the request should be authenticated If set to true a bearer value 
        /// will be added to the authentication header.</param>
        /// <returns>A response object containing the result object.</returns>
        private async Task<TResponse> PerformHttpCall<TRequest, TRequestResponse, TResponse>(string endpoint,
            TRequest requestObject,
            Func<HttpClient, string, StringContent, Task<HttpResponseMessage>> requestFunc,
            Func<TRequestResponse, TResponse> responseFunc,
            Func<Exception, TResponse> exceptionFunc,
            bool authenticate)
        {
            var httpClient = authenticate 
                ? _httpClientProvider.AuthenticatedClient 
                : _httpClientProvider.NonAuthenticatedClient;

            var postData = new StringContent(JsonConvert.SerializeObject(requestObject), Encoding.UTF8, "application/json");

            try
            {
                var response = await requestFunc(httpClient, endpoint, postData);

                response.EnsureSuccessStatusCode();

                string responseString = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<TRequestResponse>(responseString);

                return responseFunc(responseObject);
            }
            catch (Exception e)
            {
                return exceptionFunc(e);
            }
        }
    }
}