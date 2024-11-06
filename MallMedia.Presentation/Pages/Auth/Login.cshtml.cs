using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using MallMedia.Domain.Constants;
using MallMedia.Presentation.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.JSInterop;
namespace MallMedia.Presentation.Pages.Auth
{
    public class LoginModel(HttpClient httpClient, IJSRuntime jsRuntime) : PageModel
    {

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Create login request
            var request = new CustomLoginRequest
            {
                Username = Input.Username,
                Password = Input.Password
            };

            // Serialize request as JSON
            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Perform the login attempt
            var url = $"{Constants.ClientConstant.BaseURl}/api/identity/login";
            var response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                // Deserialize response data
                var contentJson = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<LoginResponse>(contentJson);

                // Assuming the token is returned in the responseContent object
                var token = responseContent?.Token;

                if (!string.IsNullOrEmpty(token))
                {
                    // Set the Authorization header for future requests
                    //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpContext.Response.Cookies.Append("authToken", token, new CookieOptions
                    {
                        HttpOnly = true, // Ensures cookie is inaccessible to client-side scripts
                        Secure = true,   // Requires HTTPS
                        SameSite = SameSiteMode.Strict, // Prevents CSRF attacks
                        Expires = DateTimeOffset.UtcNow.AddYears(1) // Set expiration as needed
                    });

                    //await jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", responseContent.Token);

                }

                // Assume successful login if response is successful
                return LocalRedirect(returnUrl);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Locked)
            {
                ErrorMessage = "This account has been locked out due to multiple failed login attempts. Please try again later.";
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }

    }
}