
using MallMedia.API.Extensions;
using MallMedia.API.Middlewares;
using MallMedia.Application.Devices.Commands.UpdateDevice;
using MallMedia.Application.Extensions;
using MallMedia.Application.MasterData.Queries.GetDeviceById;
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
        configuration.RegisterServicesFromAssemblyContaining<GetDeviceByIdQueryHandler>();
    });
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetDeviceByIdQueryHandler>());
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlDatabase")));

    var app = builder.Build();

    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IInitialSeeder>();

    await seeder.Seed();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseHttpsRedirection();

    app.MapGroup("api/identity")
        .WithTags("Identity")
        .MapIdentityApi<User>();

    app.UseAuthorization();

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