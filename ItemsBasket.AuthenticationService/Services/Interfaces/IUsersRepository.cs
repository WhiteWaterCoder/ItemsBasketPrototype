using ItemsBasket.AuthenticationService.Responses;
using ItemsBasket.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItemsBasket.AuthenticationService.Services.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> List();
        Task<UserResponse> Get(string username, string password);
        Task<UserResponse> Create(string userName, string password);
        Task<UserResponse> Update(User user);
        Task<UserResponse> Delete(User user);
    }
}