using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.Kernel.Infrastructure.Clients;
using Texnokaktus.ProgOlymp.Kernel.Infrastructure.Clients.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.Infrastructure;

public static class DiExtensions
{
    public static IServiceCollection AddGrpcClients(this IServiceCollection services) =>
        services.AddScoped<IRegistrationServiceClient, RegistrationServiceClient>();
}
