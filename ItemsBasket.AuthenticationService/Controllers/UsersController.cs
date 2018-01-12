using ItemsBasket.AuthenticationService.Models;
using ItemsBasket.AuthenticationService.Responses;
using ItemsBasket.AuthenticationService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItemsBasket.AuthenticationService.Controllers
{
    /// <summary>
    /// Performs CRUD actions for a single user account.
    /// </summary>
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        /// <summary>
        /// Return a list of usernames currently registered in the data store.
        /// </summary>
        /// <returns>List of registered usernames.</returns>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return await _usersRepository.List();
        }

        /// <summary>
        /// Update an existing user account. The only properties that can be modified are the 
        /// username and/or password of the account.
        /// </summary>
        /// <param name="user">The user details to be modified.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        [HttpPost]
        public async Task<UserResponse> Post([FromBody]User user)
        {
            return await _usersRepository.Update(user);
        }

        [HttpPut]
        public async Task<UserResponse> Put([FromBody]User user)
        {
            return await _usersRepository.Create(user.Username, user.Password);
        }

        [HttpDelete]
        public async Task<UserResponse> Delete([FromBody]User user)
        {
            return await _usersRepository.Delete(user);
        }
    }
}
