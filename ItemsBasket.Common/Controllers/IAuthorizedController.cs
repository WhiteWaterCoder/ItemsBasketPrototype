using Microsoft.Extensions.Logging;
using System.Security.Principal;

namespace ItemsBasket.Common.Controllers
{
    public interface IAuthorizedController
    {
        ILogger GetLogger();

        IIdentity GetUserIdentity();
    }
}