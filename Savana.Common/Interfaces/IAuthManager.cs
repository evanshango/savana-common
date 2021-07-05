using System.Collections.Generic;

namespace Savana.Common.Interfaces
{
    public interface IAuthManager
    {
        string Authenticate(string firstName, string lastName, string email, IEnumerable<string> userRoles, int days);
    }
}