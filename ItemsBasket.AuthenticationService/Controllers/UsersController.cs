using ItemsBasket.AuthenticationService.Responses;
using ItemsBasket.AuthenticationService.Services.Interfaces;
using ItemsBasket.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ItemsBasket.AuthenticationService.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _usersRepository.List();
        }

        /// <summary>
        /// Retrieves the user details for the provided username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("{username},{password}", Name = "Get")]
        public async Task<UserResponse> Get(string username, string password)
        {
            return await _usersRepository.Get(username, password);
        }

        [HttpPost]
        public async Task<UserResponse> Post([FromBody]User user)
        {
            return await _usersRepository.Update(user);
        }

        [HttpPut]
        public async Task<UserResponse> Put([FromBody]User user)
        {
            return await _usersRepository.Create(user.UserName, user.Password);
        }

        [HttpDelete]
        public async Task<UserResponse> Delete([FromBody]User user)
        {
            return await _usersRepository.Delete(user);
        }
    }
}
