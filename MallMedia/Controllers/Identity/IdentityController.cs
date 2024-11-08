using MallMedia.API.Extensions;
using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MallMedia.API.Controllers.Identity;

[Route("api/identity")]
[ApiController]
public class IdentityController(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IOptions<JwtSettings> jwtSettings,
    IDevicesRepository devicesRepository
    ) : ControllerBase
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

        // Get user roles (this retrieves all roles associated with the user)
        var userRoles = await userManager.GetRolesAsync(user);

        // Generate JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSettings.Value.SecretKey);

        // Add roles as claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        // Add roles to the claims
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role)); // Add role as a claim
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddYears(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return Ok(new { Token = jwtToken, Message = "Login successful." });
    }

    [HttpGet("currentUser")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<CurrentUser>> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var username = User.FindFirstValue(ClaimTypes.Name);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User is not authenticated.");
        }

        // Get all roles associated with the user as a list
        var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

        // Create CurrentUser model with the list of roles
        var currentUser = new CurrentUser
        {
            Id = userId,
            Username = username,
            Roles = roles
        };

        return Ok(currentUser);
    }

    [HttpGet("currentDevice")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<int>> GetCurrentDevice()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("Device is not authenticated.");
        }

        Device device = await devicesRepository.GetByUserIdAsync(userId);


        return Ok(device.Id);
    }

}
