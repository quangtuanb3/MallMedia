using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Constants;
using MallMedia.API.Extensions;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace MallMedia.API.Controllers.Identity;

[Route("api/identity")]
[ApiController]
public class IdentityController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JwtSettings> jwtSettings) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] CustomLoginRequest login)
    {
        // Find user by username (not email)
        var user = await userManager.FindByNameAsync(login.Username);
        if (user == null)
        {
            return Unauthorized("Invalid login attempt.");
        }

        // Validate password
        var result = await signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            return Unauthorized("Invalid login attempt.");
        }

        // Generate JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSettings.ToString());
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    }),
            Expires = DateTime.UtcNow.AddYears(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return Ok(new { Token = jwtToken, Message = "Login successful." });
    }

}
