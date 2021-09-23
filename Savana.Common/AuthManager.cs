using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Savana.Common.Entities;
using Savana.Common.Interfaces;
using static Savana.Common.Entities.ApplicationIdConstants;
using static Savana.Common.Entities.TokenConstants;

namespace Savana.Common
{
    /// <summary>
    /// Contains methods for token generation, user email retrieval from token passed and token validation
    /// </summary>
    public class AuthManager : IAuthManager
    {
        private readonly SymmetricSecurityKey _key;
        private readonly string _issuer;

        public AuthManager(SymmetricSecurityKey key, string issuer)
        {
            _key = key;
            _issuer = issuer;
        }
        
        /// <summary>
        /// Generates user token based on params passed
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="userRoles"></param>
        /// <param name="span"></param>
        /// <param name="appId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>

        public string GenerateToken(string firstName, string lastName, string email, IEnumerable<string> userRoles,
            int span, string appId, int? groupId)
        {
            var expiresAt = appId.Equals(Admin) ? DateTime.UtcNow.AddHours(span) : DateTime.UtcNow.AddDays(span);

            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, email),
                new(ClaimTypes.GivenName, $"{firstName} {lastName}"),
                new(ClaimTypes.Actor, appId),
            };
            if (groupId != null) claims.Add(new Claim(ClaimTypes.GroupSid, groupId.ToString()));

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

        /// <summary>
        /// Retrieve email address from the token passed token is valid
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetEmailAndNameFromToken(string token)
        {
            var payload = new JwtSecurityTokenHandler().ReadJwtToken(token);

            return payload.Claims.Where(c => c.Type == UniqueName).Select(c => c.Value).FirstOrDefault();
        }

        /// <summary>
        /// Validate the token passed by the user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="secretKey"></param>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
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
                    return InvalidToken;

                return token;
            }
            catch (SecurityTokenException e)
            {
                return e.Message.Contains(Validation) ? TokenConstants.GenerateToken : InvalidToken;
            }
        }
    }
}