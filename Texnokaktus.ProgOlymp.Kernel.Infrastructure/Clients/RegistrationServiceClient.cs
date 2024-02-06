using Texnokaktus.ProgOlymp.Common.Contracts.Grpc.YandexContest;
using Texnokaktus.ProgOlymp.Kernel.Infrastructure.Clients.Abstractions;
using Texnokaktus.ProgOlymp.Kernel.Infrastructure.Exceptions;

namespace Texnokaktus.ProgOlymp.Kernel.Infrastructure.Clients;

internal class RegistrationServiceClient(RegistrationService.RegistrationServiceClient client) : IRegistrationServiceClient
{
    public async Task<string> RegisterParticipantAsync(int contestStageId, string yandexIdLogin)
    {
        var response = await client.RegisterParticipantAsync(new()
        {
            ContestStageId = contestStageId,
            YandexIdLogin = yandexIdLogin
        });

        if (response.Error is { } error)
            throw new RegistrationException(error.Type, error.Message);

        if (response.ContestUrl is null)
            throw new RegistrationException(ErrorType.Generic, $"No {nameof(RegisterParticipantResponse.ContestUrl)} value is provided in response.");

        return response.ContestUrl;
    }

    public async Task UnregisterParticipantAsync(int contestStageId, string yandexIdLogin)
    {
        var response = await client.UnregisterParticipantAsync(new()
        {
            ContestStageId = contestStageId,
            YandexIdLogin = yandexIdLogin
        });

        if (response.Error is { } error)
            throw new RegistrationException(error.Type, error.Message);
    }
}
