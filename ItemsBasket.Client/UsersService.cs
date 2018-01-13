using ItemsBasket.AuthenticationService.Models;
using ItemsBasket.AuthenticationService.Responses;
using ItemsBasket.Client.Interfaces;
using System.Threading.Tasks;

namespace ItemsBasket.Client
{
    public class UsersService : BaseServiceCaller, IUsersService
    {
        /// <summary>
        /// Default constructor. To be used in scenarios where Dependency Injection 
        /// is not available.
        /// </summary>
        public UsersService()
            : base(new EnvironmentService(), new HttpClientProvider())
        {
        }

        /// <summary>
        /// Dependency Injection friendly constructor. 
        /// </summary>
        /// <param name="environmentService">The environment service containing endpoint information.</param>
        /// /// <param name="httpClientProvider">The http client provider for authenticatead and non authenticated clients.</param>
        public UsersService(IEnvironmentService environmentService, IHttpClientProvider httpClientProvider)
            : base(environmentService, httpClientProvider)
        {
        }

        public async Task<User> CreateUser(User user)
        {
            return await PutNonAuthenticatedCall<User, UserResponse, User>(
                $"{EnvironmentService.ServiceEndpoints[KnownService.UserService]}",
                user,
                response =>
                {
                    return response.IsSuccessful 
                        ? response.Item 
                        : User.Empty;
                },
                e =>
                {
                    return User.Empty;
                });
        }

        public async Task<User> UpdateUser(User user)
        {
            return await PostNonAuthenticatedCall<User, UserResponse, User>(
                $"{EnvironmentService.ServiceEndpoints[KnownService.UserService]}",
                user,
                response =>
                {
                    return response.IsSuccessful
                        ? response.Item
                        : User.Empty;
                },
                e =>
                {
                    return User.Empty;
                });
        }

        public async Task<User> DeleteUser(User user)
        {
            return await DeleteNonAuthenticatedCall<User, UserResponse, User>(
                $"{EnvironmentService.ServiceEndpoints[KnownService.UserService]}/{user.UserId}",
                user,
                response =>
                {
                    return response.IsSuccessful
                        ? response.Item
                        : User.Empty;
                },
                e =>
                {
                    return User.Empty;
                });
        }
    }
}