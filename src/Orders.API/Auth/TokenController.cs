using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Orders.API.Auth;

[ApiController]
[Route("auth")]
public class TokenController : ControllerBase
{
    private readonly IConfiguration _config;

    public TokenController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("token")]
    public IActionResult GenerateToken()
    {
        var key = _config["Jwt:Key"]!;
        var issuer = _config["Jwt:Issuer"]!;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("sub", "b1c2d3e4-0000-1111-2222-333344445555"),
            new Claim("scope", "orders.write orders.read")
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: null,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = jwt });
    }
}
