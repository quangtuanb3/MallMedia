namespace MallMedia.Presentation.Helper;

using MallMedia.Domain.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;
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
            //throw new UnauthorizedAccessException("Authentication token is missing.");
            Console.WriteLine("Authentication token is missing.");
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
    public enum Roles
    {
        SuperAdmin,
        Admin,
        Basic
    }
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
        }
        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
        }
    }
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }
    }
    public static class DefaultUsers
    {
        public static async Task SeedBasicUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new IdentityUser
            {
                UserName = "basicuser@gmail.com",
                Email = "basicuser@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }
        /*
        public static async Task SeedSuperAdminAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new IdentityUser
            {
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
                await roleManager.SeedClaimsForSuperAdmin();
            }
        }*/
        /*
        private async static Task SeedClaimsForSuperAdmin(RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            await roleManager.AddPermissionClaim(adminRole, "Products");
        }*/
        public static async Task AddPermissionClaim(RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
        public class CurrentUser
        {
            public string Id { get; set; }
            public string Username { get; set; }
            public List<string> Roles { get; set; }
        }
    }
    /*
    public async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("app");
            try
            {
                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
                await Seeds.DefaultUsers.SeedBasicUserAsync(userManager, roleManager);
                await Seeds.DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);
                logger.LogInformation("Finished Seeding Default Data");
                logger.LogInformation("Application Starting");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "An error occurred seeding the DB");
            }
        }
        host.Run();
    }*/
}
