using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Savana.Common.Entities;
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

        public string GenerateToken(string firstName, string lastName, string email, IEnumerable<string> userRoles,
            int duration, string appId)
        {
            var expiresAt = appId.Equals(ApplicationIdConstants.Admin)
                ? DateTime.UtcNow.AddHours(duration)
                : DateTime.UtcNow.AddDays(duration);

            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, email),
                new(ClaimTypes.GivenName, $"{firstName} {lastName}"),
                new (ClaimTypes.Actor, appId)
            };
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                SigningCredentials = credentials,
                Issuer = _issuer,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GetEmailAndNameFromToken(string token)
        {
            var payload = new JwtSecurityTokenHandler().ReadJwtToken(token);

            return payload.Claims.Where(c => c.Type == TokenConstants.UniqueName).Select(c => c.Value).FirstOrDefault();
        }

        public string ValidateToken(string token, string secretKey, string email, string firstName, string lastName)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidIssuer = _issuer,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken) validatedToken;

                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512))
                    return TokenConstants.InvalidToken;

                return token;
            }
            catch (SecurityTokenException e)
            {
                return e.Message.Contains("Lifetime validation failed")
                    ? TokenConstants.GenerateToken
                    : TokenConstants.InvalidToken;
            }
        }
    }
}