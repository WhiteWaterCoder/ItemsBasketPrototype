using ItemsBasket.AuthenticationService.Responses;
using ItemsBasket.AuthenticationService.Services.Interfaces;
using ItemsBasket.Common.Exceptions;
using ItemsBasket.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemsBasket.AuthenticationService.Services
{
    /// <summary>
    /// The implementation of this class assumes only a single instance of the service will 
    /// be running. Most of the code would be redundant with a SQL (or any) db, specially
    /// for key creation.
    /// </summary>
    public class UsersRepository : IUsersRepository
    {
        private static readonly object _newUserLock = new object();

        private readonly IUsersValidator _usersValidator;

        public UsersRepository(IUsersValidator usersValidator)
        {
            _usersValidator = usersValidator;
        }

        private int _currentMaxUserId = 1;
        private readonly IDictionary<int, User> _context = new Dictionary<int, User>
        {
            { 1, new User(1, UsersValidator.AdminUserName, "pass", Guid.Empty.ToString()) }
        };

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IEnumerable<User>> List()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return _context.Select(c => c.Value);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<UserResponse> Get(string username, string password)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var user = _context.Values.FirstOrDefault(c => string.Equals(c.UserName, username));

            if (user == null)
            {
                return UserResponse.CreateFailedResult($"No user found with username = {username}");
            }

            if (!string.Equals(user.Password, password))
            {
                return UserResponse.CreateFailedResult($"The password provided for user with username = {username} is wrong. Please try again.");
            }

            return UserResponse.CreateSuccessfulResult(user);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<UserResponse> Create(string userName, string password)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            int nextUserId;
            User newUser;

            lock(_newUserLock)
            {
                if (!_usersValidator.IsPasswordValid(password, out string errorMessage))
                {
                    return UserResponse.CreateFailedResult(errorMessage);
                }
                if (!_usersValidator.IsUsernameUnique(password, _context, out errorMessage))
                {
                    return UserResponse.CreateFailedResult(errorMessage);
                }

                _currentMaxUserId++;
                nextUserId = _currentMaxUserId;
            }

            newUser = new User(nextUserId, userName, password, Guid.NewGuid().ToString());

            _context.Add(newUser.Id, newUser);

            return UserResponse.CreateSuccessfulResult(newUser);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<UserResponse> Update(User user)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (!_context.TryGetValue(user.Id, out User existingUser))
            {
                return UserResponse.CreateFailedResult($"Could not find user with ID = {user.Id} to update.");
            }

            if (!_usersValidator.IsAuthorizedToModify(user, existingUser, out string notAuthorizedMessage))
            {
                return UserResponse.CreateFailedResult(notAuthorizedMessage);
            }

            _context[user.Id] = user;

            return UserResponse.CreateSuccessfulResult(user);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<UserResponse> Delete(User user)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (!_context.TryGetValue(user.Id, out User existingUser))
            {
                return UserResponse.CreateFailedResult($"Could not find user with ID = {user.Id} to update.");
            }

            if (!_usersValidator.IsAuthorizedToModify(user, existingUser, out string notAuthorizedMessage))
            {
                return UserResponse.CreateFailedResult(notAuthorizedMessage);
            }

            _context.Remove(user.Id);

            return UserResponse.CreateSuccessfulResult(User.Empty);
        }
    }
}