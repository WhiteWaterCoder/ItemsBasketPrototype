using ItemsBasket.Client.Interfaces;
using System.Collections.Generic;

namespace ItemsBasket.Client
{
    public class EnvironmentService : IEnvironmentService
    {
        /// <summary>
        /// A dictionary of:
        ///     -   Key: A known service.
        ///     - Value: The endpoint of the service
        /// This is a crude implementation for the sake of the prototype. In the real world service discovery
        /// (like Consul) would be used. This would mean only 1 endpoint would be required (the load balanced
        /// service discovery endpoint).
        /// </summary>
        public IDictionary<KnownService, string> ServiceEndpoints { get; }

        public EnvironmentService()
        {
            ServiceEndpoints = new Dictionary<KnownService, string>
            {
                { KnownService.AuthenticationService, "http://localhost:57754/api/Authentication" },
                { KnownService.UserService, "http://localhost:57754/api/Users" },
                { KnownService.BasketService, "http://localhost:49860/api/BasketItems" },
            };
        }
    }
}