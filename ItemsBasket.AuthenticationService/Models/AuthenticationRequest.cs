namespace ItemsBasket.AuthenticationService.Models
{
    /// <summary>
    /// The contents of a user authentication request.
    /// </summary>
    public class AuthenticationRequest
    {
        /// <summary>
        /// The username of the user.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// The password associated with the username.
        /// </summary>
        public string Password { get; }

        public AuthenticationRequest(string userName, string password)
        {
            Username = userName;
            Password = password;
        }

        public override string ToString()
        {
            return Username;
        }
    }
}