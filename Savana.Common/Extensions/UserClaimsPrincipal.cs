using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Savana.Common.Extensions
{
    public static class UserClaimsPrincipal
    {
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Name);
        }

        public static IEnumerable<string> RetrieveRolesFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindAll(ClaimTypes.Role).Select(c => c.Value);
        }

        public static string RetrieveAppIdFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Actor);
        }
    }
}