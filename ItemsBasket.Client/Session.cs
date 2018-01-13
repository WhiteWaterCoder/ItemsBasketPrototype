namespace ItemsBasket.Client
{
    /// <summary>
    /// Contains session details for the current user.
    /// </summary>
    internal static class Session
    {
        /// <summary>
        /// The authentication token for the user to be used where authorization 
        /// is required by the API/
        /// </summary>
        internal static string Token { get; set; }
    }
}