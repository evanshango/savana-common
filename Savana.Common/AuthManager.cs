using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Savana.Common.Dtos;
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

        public string GenerateToken(
            string firstName, string lastName, string email, IEnumerable<string> userRoles, int hours)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, email),
                new(ClaimTypes.GivenName, $"{firstName} {lastName}")
            };
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(hours),
                SigningCredentials = credentials,
                Issuer = _issuer,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GetEmailAndNameFromToken(string token)
        {
            var payload = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var email = payload.Claims.Where(c => c.Type == "unique_name").Select(c => c.Value).FirstOrDefault();
            var name = payload.Claims.Where(c => c.Type == "given_name").Select(c => c.Value).FirstOrDefault();

            return $"{email}//{name}";
        }

        public JwtDto ValidateToken(string token, string secretKey, string email, string displayName)
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
                    return new JwtDto {DisplayName = "", Email = "", Token = "", Message = "invalid_token"};

                return new JwtDto {DisplayName = displayName, Email = email, Token = token};
            }
            catch (SecurityTokenException e)
            {
                return e.Message.Contains("Lifetime validation failed")
                    ? new JwtDto {DisplayName = displayName, Email = email, Token = "", Message = "generate_token"}
                    : new JwtDto {DisplayName = "", Email = "", Token = "", Message = "invalid_token"};
            }
        }
    }
}