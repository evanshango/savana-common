using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Savana.Common.Interfaces;

namespace Savana.Common
{
    public class AuthManager : IAuthManager
    {
        private readonly SymmetricSecurityKey _key;
        private readonly string _issuer;

        public AuthManager(SymmetricSecurityKey key, string issuer)
        {
            _key = key;
            _issuer = issuer;
        }

        public string Authenticate(
            string firstName, string lastName, string email, IEnumerable<string> userRoles, int days)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, email),
                new(ClaimTypes.GivenName, $"{firstName} {lastName}")
            };
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(days),
                SigningCredentials = credentials,
                Issuer = _issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}