using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.Kernel.Notifications.GoogleSheets.Services;
using Texnokaktus.ProgOlymp.Kernel.Notifications.GoogleSheets.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.Notifications.GoogleSheets;

public static class DiExtensions
{
    public static IServiceCollection AddGoogleSheetsNotifications(this IServiceCollection services, bool useStubService)
    {
        if (useStubService)
            services.AddScoped<IGoogleSheetsNotificationService, StubGoogleSheetsNotificationService>();
        else
            services.AddScoped<IGoogleSheetsNotificationService, GoogleSheetsNotificationService>();

        return services;
    }
}
