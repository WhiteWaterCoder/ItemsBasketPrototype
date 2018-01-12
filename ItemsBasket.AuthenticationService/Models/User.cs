namespace ItemsBasket.AuthenticationService.Models
{
    /// <summary>
    /// The details of a user as stored in the datastore.
    /// </summary>
    public class User
    {
        /// <summary>
        /// An empty instance of a user. To be used where no valid instance exists, instead of null.
        /// </summary>
        public static readonly User Empty = new User(-1, "", "");

        /// <summary>
        /// The ID of the user.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// The username of the user.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// The password of the user's account.
        /// </summary>
        public string Password { get; }

        public User(int userId, string userName, string password)
        {
            UserId = userId;
            Username = userName;
            Password = password;
        }

        public override string ToString()
        {
            return Username;
        }
    }
}