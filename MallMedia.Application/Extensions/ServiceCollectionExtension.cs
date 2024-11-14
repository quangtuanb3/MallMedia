using FluentValidation;
using FluentValidation.AspNetCore;
using MallMedia.Application.Common;
using Microsoft.Extensions.DependencyInjection;

namespace MallMedia.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddAutoMapper(applicationAssembly);

        services.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();

        services.AddHttpContextAccessor();

        services.AddSingleton<IBackgroundServiceQueue, BackgroundServiceQueue>();
        services.AddHostedService<FileMergeService>();
    }
}