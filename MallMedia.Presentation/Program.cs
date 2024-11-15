
using MallMedia.Application.ConnectHubs;
using MallMedia.Presentation.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Xabe.FFmpeg;

var builder = WebApplication.CreateBuilder(args);
FFmpeg.SetExecutablesPath("C:\\Users\\TUANBQ\\AppData\\Local\\Microsoft\\WinGet\\Packages\\Gyan.FFmpeg_Microsoft.Winget.Source_8wekyb3d8bbwe\\ffmpeg-7.1-full_build\\bin");
builder.Services.AddHttpClient();
builder.Services.AddSignalR();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = "https://your-auth-server.com"; // Replace with your auth server
            options.Audience = "your-audience"; // Replace with your API audience

        });
builder.Services.AddAuthentication("Cookies")
        .AddCookie(options =>
        {
            options.LoginPath = "/Auth/Login"; // Path to your login page
            options.AccessDeniedPath = "/Auth/AccessDenied"; // Optional: Path for access-denied
        });


builder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor
builder.Services.AddSingleton<AuthenticationHelper>(); // Register your helper
builder.Services.AddHttpClient();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.MapHub<ScheduleHub>("/real-time-update_hub");
app.MapHub<ContentHub>("/real-time-update");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UsePathBase("/Client");
app.Run();

