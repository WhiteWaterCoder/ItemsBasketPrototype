using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ItemsBasket.Common.Controllers
{
    public static class AuthorizedControllerExtensions
    {
        public static async Task<T> ExecuteAuthorizedAction<T>(this IAuthorizedController controller,
            Func<int, Task<T>> func, 
            Func<string, T> errorFunc,
            string genericErrorMessage)
        {
            try
            {
                bool isAuthorized = TryGetAuthorizedUserId(controller, out int nameIdentifier);

                if (!isAuthorized)
                {
                    return errorFunc.Invoke("The user is not authorized to execute this action.");
                }

                return await func(nameIdentifier);
            }
            catch(Exception e)
            {
                controller.GetLogger().LogError(e, e.Message);
                return errorFunc.Invoke(genericErrorMessage);
            }
        }

        private static bool TryGetAuthorizedUserId(IAuthorizedController controller,
            out int nameIdentifier)
        {
            var identity = controller.GetUserIdentity() as ClaimsIdentity;

            if (identity == null)
            {
                nameIdentifier = -1;
                return false;
            }

            foreach(var claim in identity.Claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier)
                {
                    return int.TryParse(claim.Value, out nameIdentifier);
                }
            }

            nameIdentifier = -1;
            return false;
        }
    }
}