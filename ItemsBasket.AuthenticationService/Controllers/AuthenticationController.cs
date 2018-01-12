using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ItemsBasket.AuthenticationService.Models;
using ItemsBasket.AuthenticationService.Responses;
using ItemsBasket.AuthenticationService.Services.Interfaces;
using ItemsBasket.Common.Configuration;
using ItemsBasket.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ItemsBasket.AuthenticationService.Controllers
{
    /// <summary>
    /// Has only got a single Get method which validates users.
    /// </summary>
    [Produces("application/json")]
    [Route("api/Authentication")]
    public class AuthenticationController : Controller
    {
        private readonly IUsersRepository _usersRepository;

        public AuthenticationController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        /// <summary>
        /// Retrieves the user details for the provided username and password.
        /// </summary>
        /// <param name="request">The request for the user to authenticate.</param>
        /// <returns>An authentication response with a token if successful.</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<AuthenticationResponse> Get([FromBody]AuthenticationRequest request)
        {
            (bool isAuthenticated, int userId, string errorMessage) = await _usersRepository.IsAuthenticated(request.Username, request.Password);

            if (isAuthenticated)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, request.Username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigProvider.SecurityKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "localhost:57754",
                    audience: "localhost:49860",
                    claims: claims,
                    expires: DateTime.Now.AddMonths(1),
                    signingCredentials: creds);

                var authenticatedUser = new AuthenticatedUser(userId, request.Username, new JwtSecurityTokenHandler().WriteToken(token));

                return AuthenticationResponse.CreateSuccessfulResult(authenticatedUser);
            }

            return AuthenticationResponse.CreateFailedResult(errorMessage);
        }
    }
}