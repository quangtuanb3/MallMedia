﻿using MallMedia.Domain.Entities;
using MallMedia.Domain.Interfaces;
using MallMedia.Domain.Repositories;
using MallMedia.Infrastructure.Persistence;
using MallMedia.Infrastructure.Repositories;
using MallMedia.Infrastructure.Seeders;
using MallMedia.Infrastructure.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MallMedia.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MallMediaDb");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());

        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = false;
            options.SignIn.RequireConfirmedEmail = false;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

        services.AddScoped<CategorySeeder>();
        services.AddScoped<LocationSeeder>();
        services.AddScoped<DeviceSeeder>();
        services.AddScoped<TimeFrameSeeder>();
        services.AddScoped<UserSeeder>();
        services.AddScoped<IInitialSeeder, InitialSeeder>();
        services.AddScoped<IMasterDataRepository, MasterDataRepository>();
        services.AddScoped<IMediaRepository, MediaRepository>();
        services.AddScoped<IContentRepository, ContentRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepostiroy>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<IDevicesRepository, DevicesRepository>();
        services.AddScoped<ITimeFramesRepository, TimeFramesRepository>();
    }
}
