using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace mptc.dgc.sample.webapi.Controllers.V2025_06_22
{
    [ApiVersion("2025-06-22")]
    [Route("users")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController(IConfiguration configuration) : ControllerBase
    {
        [HttpPost("token")]
      
        public Task<IActionResult> Login()
        {
            var token = GenerateJwtToken();
            return Task.FromResult<IActionResult>(Ok(new { token }));
        }

        [HttpGet]
        [Authorize]
        [EnableRateLimiting("UserBased")]
        public IActionResult GetResponse()
        { 
            return Ok("Request Success");
        }

        private string GenerateJwtToken()
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"]));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                signingCredentials: credentials,
                expires: expires
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
