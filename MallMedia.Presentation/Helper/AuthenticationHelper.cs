namespace MallMedia.Presentation.Helper;

using MallMedia.Domain.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

public class AuthenticationHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void AddBearerToken(HttpClient httpClient)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            throw new InvalidOperationException("No HttpContext available.");
        }

        // Retrieve the auth token from cookies
        var authToken = httpContext.Request.Cookies["authToken"];
        if (string.IsNullOrEmpty(authToken))
        {
            throw new UnauthorizedAccessException("Authentication token is missing.");
        }

        // Set the Authorization header with the token
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
    }


    public CurrentUser GetCurrentUser()
    {
        var user = _httpContextAccessor.HttpContext.User;
        if (!user.Identity.IsAuthenticated)
        {
            return null; // or return a default value or throw exception as needed
        }
        return new CurrentUser()
        {
            // Get user ID from claims (usually from NameIdentifier)
            Id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value,

            // Get username (typically from Name or username claim)
            Username = user.Identity?.Name ?? string.Empty,

            // Get roles (this assumes roles are available in claims)
            Roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
        };
    }

}
