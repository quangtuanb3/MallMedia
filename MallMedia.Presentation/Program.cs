
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
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



builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UsePathBase("/Client");
app.Run();

