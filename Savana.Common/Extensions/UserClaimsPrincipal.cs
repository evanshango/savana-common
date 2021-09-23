using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Savana.Common.Extensions
{
    public static class UserClaimsPrincipal
    {
        /// <summary>
        /// Retrieves an email of the signed in user from the ClaimsPrincipal
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Name);
        }

        /// <summary>
        /// Retrieves a list of the currently signed in user roles from the ClaimsPrincipal
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static IEnumerable<string> RetrieveRolesFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindAll(ClaimTypes.Role).Select(c => c.Value);
        }

        /// <summary>
        /// Retrieves the current ApplicationId from the ClaimsPrincipal
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string RetrieveAppIdFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Actor);
        }

        /// <summary>
        /// Retrieves the currently signed in user's groupId from the ClaimsPrincipal
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int? RetrieveGroupIdFromPrincipal(this ClaimsPrincipal user)
        {
            try
            {
                return int.Parse(user.FindFirstValue(ClaimTypes.GroupSid));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}