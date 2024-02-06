namespace Texnokaktus.ProgOlymp.Kernel.Infrastructure.Clients.Abstractions;

public interface IRegistrationServiceClient
{
    Task<string> RegisterParticipantAsync(int contestStageId, string yandexIdLogin);
    Task UnregisterParticipantAsync(int contestStageId, string yandexIdLogin);
}
