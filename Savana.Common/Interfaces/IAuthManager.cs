﻿using System.Collections.Generic;
using Savana.Common.Dtos;

namespace Savana.Common.Interfaces
{
    public interface IAuthManager
    {
        string GenerateToken(string firstName, string lastName, string email, IEnumerable<string> userRoles, int hours);

        string GetEmailAndNameFromToken(string token);
        JwtDto ValidateToken(string token, string secretKey, string firstName, string lastName, string email);
    }
}