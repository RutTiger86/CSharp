using CSharp.Authorization.Token.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace CSharp.Authorization.Token.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(IEnumerable<Claim> claims, TokenType tokenType);
        string GenerateToken(string userId, TokenType tokenType);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token, bool ValidateLifetime = false);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly string secret;
        private readonly string issuer;
        private readonly string audience;

        private readonly int accessTokenExpriesAddMin = 30;
        private readonly int refrashTokenExpriesAddDay = 15;

        private const string userId = "userId";

        public JwtTokenService(IConfiguration configuration)
        {
            secret = configuration["Jwt:Secret"];
            issuer = configuration["Jwt:Issuer"];
            audience = configuration["Jwt:Audience"];
        }

        public string GenerateToken(IEnumerable<Claim> claims, TokenType tokenType)
        {
            string userId = claims.First(p => p.Type == JwtTokenService.userId).Value;
            return GenerateToken(userId, tokenType);
        }

        public string GenerateToken(string userId, TokenType tokenType)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            DateTime expiresTime = tokenType == TokenType.AccessToken ? DateTime.UtcNow.AddMinutes(accessTokenExpriesAddMin)
                : DateTime.UtcNow.AddDays(refrashTokenExpriesAddDay);
            
            var claims = new[]
            {
                new Claim(JwtTokenService.userId, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, issuer)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresTime,
                SigningCredentials = credentials,
                Issuer = issuer, // 발급자(issuer) 설정
                Audience = audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token, bool validateLifetime = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = validateLifetime,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = issuer,
                ValidAudience = audience
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is JwtSecurityToken jwtSecurityToken
                && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }

            throw new SecurityTokenException("Invalid token");
        }
    }
}
