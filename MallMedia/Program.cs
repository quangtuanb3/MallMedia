using MallMedia.API.Extensions;
using MallMedia.API.Middlewares;
using MallMedia.Application.Devices.Command.GetDeviceById;
using MallMedia.Application.Devices.Commands.UpdateDevice;
using MallMedia.Application.Devices.Queries.GetByIdDevice;
using MallMedia.Application.Devices.Queries.GetByIdDevices;
using MallMedia.Application.Extensions;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MallMedia.Infrastructure.Extensions;
using MallMedia.Infrastructure.Persistence;
using MallMedia.Infrastructure.Repositories;
using MallMedia.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
    builder.Services.AddMediatR(configuration =>
    {
        configuration.RegisterServicesFromAssembly(typeof(UpdateDeviceCommandHandler).Assembly);
        configuration.RegisterServicesFromAssemblyContaining<GetDevicesByIdQueryHandler>();
    });
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetDevicesByIdQueryHandler>());
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MallMediaDb")));

    builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
    builder.Services.AddMediatR(configuration =>
    {
        configuration.RegisterServicesFromAssembly(typeof(UpdateDeviceCommandHandler).Assembly);
        configuration.RegisterServicesFromAssemblyContaining<GetDevicesByIdQueryHandler>();
    });
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetDevicesByIdQueryHandler>());
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MallMediaDb")));

    var app = builder.Build();

    using var scope = app.Services.CreateScope();
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


    //app.UseCors(option=> option.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().AllowCredentials());
    app.UseHttpsRedirection();

    app.MapGroup("api/identity")
        .WithTags("Identity")
        .MapIdentityApi<User>();

    app.UseAuthorization();
    app.UseStaticFiles();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
{
    Log.CloseAndFlush();
}


public partial class Program { }