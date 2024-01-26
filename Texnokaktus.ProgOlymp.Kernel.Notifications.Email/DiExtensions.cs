using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.Kernel.Notifications.Email.Services;
using Texnokaktus.ProgOlymp.Kernel.Notifications.Email.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.Notifications.Email;

public static class DiExtensions
{
    public static IServiceCollection AddEmailNotifications(this IServiceCollection services, bool useStubService)
    {
        services.AddScoped<IEmailClient, EmailClient>();

        if (useStubService)
            services.AddScoped<IEmailNotificationService, StubEmailNotificationService>();
        else
            services.AddScoped<IEmailNotificationService, EmailNotificationService>();

        return services;
    }
}
