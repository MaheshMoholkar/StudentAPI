using ERPLibrary.Data;
using ERPLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly IConfiguration _config;
    private readonly IUserData _data;

    public record AuthenticationData(string? UserName, string? Password, string? Role);

    public AuthenticationController(IConfiguration config, IUserData data)
    {
        _config = config;
        _data = data;
    }

    [HttpPost("token")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Authenticate([FromBody] AuthenticationData data)
    {
        var user = await ValidateCredentials(data);

        if (user.Id == null)
        {
            return Unauthorized();
        }

        string token = GenerateToken(user);

        return Ok(token);
    }

    private string GenerateToken(UserDataModel user)
    {
        var authenticationIssuer = Environment.GetEnvironmentVariable("Issuer");
        var authenticationAudience = Environment.GetEnvironmentVariable("Audience");
        var authenticationSecretKey = Environment.GetEnvironmentVariable("SecretKey");

        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(
                authenticationSecretKey));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new();
        claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName));
        claims.Add(new("FirstName", user.FirstName));
        claims.Add(new("LastName", user.LastName));
        claims.Add(new("Role", user.Role));

        var token = new JwtSecurityToken(
            authenticationIssuer,
            authenticationAudience,
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<UserDataModel> ValidateCredentials(AuthenticationData data)
    {
        return await _data.Authenticate(data.UserName, data.Password, data.Role);
    }
}
