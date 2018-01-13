using System.Threading.Tasks;

namespace ItemsBasket.Client.Interfaces
{
    /// <summary>
    /// Provides authorization methods and properties for the ItemsBasket API.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Will attempt to login the user. If successful then the user will have a session
        /// whereby he can make calls to API methods that require authorization.
        /// </summary>
        /// <param name="username">The username of the user's account.</param>
        /// <param name="password">The password of the user's account.</param>
        /// <returns>True if the login is successful, otherwise false along with the error message.</returns>
        Task<(bool, string)> TryLogin(string username, string password);
    }
}