
using ItemsBasket.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemsBasket.UI
{
    public static class SillyServiceLocator
    {
        public static LogInOutViewModel LogInOutViewModel { get; private set; }

        static SillyServiceLocator()
        {
            var authenticationService = new ItemsBasket.Client.AuthenticationService();
        }
    }
}