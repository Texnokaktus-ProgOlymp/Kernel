using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.Kernel.Options.Models;

namespace Texnokaktus.ProgOlymp.Kernel.Options;

public static class DiExtensions
{
    public static IServiceCollection AddAppOptions(this IServiceCollection services)
    {
        services.AddOptions<SmtpSettings>().BindConfiguration(nameof(SmtpSettings));

        return services;
    }
}
