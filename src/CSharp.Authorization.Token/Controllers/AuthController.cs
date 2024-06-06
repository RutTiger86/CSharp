using CSharp.Authorization.Token.Enums;
using CSharp.Authorization.Token.Models.Auth;
using CSharp.Authorization.Token.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace CSharp.Authorization.Token.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IJwtTokenService jwtTokenService;

        public AuthController(IJwtTokenService jwtTokenService)
        {
            this.jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // 유효한 사용자 확인 (예: 데이터베이스에서 확인)
            if (model.Username != "test" || model.Password != "password")
            {
                return Unauthorized();
            }

            var accessToken = jwtTokenService.GenerateToken(model.Username, TokenType.AccessToken);
            var refreshToken = jwtTokenService.GenerateToken(model.Username, TokenType.RefrashToken);

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] TokenModel model)
        {
            var accessPrincipal = jwtTokenService.GetPrincipalFromExpiredToken(model.AccessToken);
            if (accessPrincipal == null)
            {
                return BadRequest("access token Verification fail");
            }

            var refrashPrincipal = jwtTokenService.GetPrincipalFromExpiredToken(model.RefreshToken, true);
            if (refrashPrincipal == null)
            {
                return BadRequest("access token  Verification completed. but refresh token Verification fail. Please log in again");
            }

            var newAccessToken = jwtTokenService.GenerateToken(accessPrincipal.Claims, TokenType.AccessToken);

            return Ok(new
            {
                AccessToken = newAccessToken
            });
        }
    }
}
