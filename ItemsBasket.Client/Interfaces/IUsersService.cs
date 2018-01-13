using ItemsBasket.AuthenticationService.Models;
using System.Threading.Tasks;

namespace ItemsBasket.Client.Interfaces
{
    public interface IUsersService
    {
        Task<User> CreateUser(User user);

        Task<User> UpdateUser(User user);

        Task<User> DeleteUser(User user);
    }
}