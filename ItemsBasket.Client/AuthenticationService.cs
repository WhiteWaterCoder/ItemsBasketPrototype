using ItemsBasket.Client.Interfaces;
using ItemsBasket.AuthenticationService.Models;
using System.Threading.Tasks;
using ItemsBasket.AuthenticationService.Responses;

namespace ItemsBasket.Client
{
    public class AuthenticationService : BaseServiceCaller, IAuthenticationService
    {
        /// <summary>
        /// Default constructor. To be used in scenarios where Dependency Injection 
        /// is not available.
        /// </summary>
        public AuthenticationService()
            : base(new EnvironmentService(), new HttpClientProvider())
        {
        }

        /// <summary>
        /// Dependency Injection friendly constructor. 
        /// </summary>
        /// <param name="environmentService">The environment service containing endpoint information.</param>
        /// /// <param name="httpClientProvider">The http client provider for authenticatead and non authenticated clients.</param>
        public AuthenticationService(IEnvironmentService environmentService, IHttpClientProvider httpClientProvider)
            : base(environmentService, httpClientProvider)
        {
        }

        public async Task<(bool, string)> TryLogin(string username, string password)
        {
            return await PostNonAuthenticatedCall<AuthenticationRequest, AuthenticationResponse, (bool, string)>(
                $"{EnvironmentService.ServiceEndpoints[KnownService.AuthenticationService]}",
                new AuthenticationRequest(username, password),
                response =>
                {
                    if (!response.IsSuccessful)
                    {
                        return (false, response.ErrorMessage);
                    }

                    Session.Token = response.Item.Token;
                    
                    return (true, "");
                },
                e =>
                {
                    return (false, e.Message);
                });
        }       
    }
}