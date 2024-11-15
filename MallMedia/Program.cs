
using MallMedia.API.Extensions;
using MallMedia.API.Middlewares;
using MallMedia.Application.ConnectHubs;
using MallMedia.Application.Extensions;
using MallMedia.Infrastructure.Extensions;
using MallMedia.Infrastructure.Seeders;
using Serilog;
using System.Net;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddSignalR();

    // Add CORS policy to allow requests from localhost:7220
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowLocalhost", policy =>
            policy
            .WithOrigins("https://localhost:7220") 
            // Allow frontend origin
                  .AllowAnyHeader()  // Allow any headers
                  .AllowAnyMethod()); // Allow any HTTP method (GET, POST, etc.)
    });
    //builder.WebHost.ConfigureKestrel(options =>
    //{
    //    // This will use the default development certificate if available
    //    options.Listen(IPAddress.Any, 5001, listenOptions =>
    //    {
    //        listenOptions.UseHttps(); // No certificate path is needed here
    //    });
    //});     
    //builder.WebHost.ConfigureKestrel(options =>
    //{
    //    options.Listen(IPAddress.Parse("127.0.0.1"), 5001);   // Listen on localhost
    //    options.Listen(IPAddress.Parse("10.20.54.244"), 5057);  // Listen on LAN IP
    //});
    var app = builder.Build();
    // Enable CORS globally (apply to all controllers)
    app.UseCors("AllowLocalhost");
    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IInitialSeeder>();

    await seeder.Seed();
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestTimeLoggingMiddleware>();
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapHub<ScheduleHub>("/real-time-update_hub");
    app.MapHub<ContentHub>("/real-time-update");
    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseStaticFiles();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}


public partial class Program { }