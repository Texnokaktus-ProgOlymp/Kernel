namespace Texnokaktus.ProgOlymp.Kernel.Infrastructure.Clients.Abstractions;

public interface IRegistrationServiceClient
{
    Task RegisterParticipantAsync(int contestStageId, string yandexIdLogin);
    Task UnregisterParticipantAsync(int contestStageId, string yandexIdLogin);
}
