using System.Security.Claims;

namespace Savana.Common.Extensions
{
    public static class UserClaimsPrincipal
    {
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}