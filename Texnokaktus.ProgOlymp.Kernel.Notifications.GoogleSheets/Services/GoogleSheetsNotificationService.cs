using MassTransit;
using Texnokaktus.ProgOlymp.Common.Contracts.Messages.GoogleSheets.Notifications;
using Texnokaktus.ProgOlymp.Kernel.Notifications.GoogleSheets.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.Notifications.GoogleSheets.Services;

internal class GoogleSheetsNotificationService(IPublishEndpoint bus) : IGoogleSheetsNotificationService
{
    public async Task SendRegistrationSuccessfulNotificationAsync(int applicationId, string yandexIdLogin) =>
        await bus.Publish(new SuccessfulRegistrationMessage(applicationId, yandexIdLogin));

    public async Task SendInvalidEmailNotificationAsync(int applicationId) =>
        await bus.Publish(new InvalidEmailMessage(applicationId));

    public async Task SendIncorrectEmailDomainNotificationAsync(int applicationId) =>
        await bus.Publish(new IncorrectEmailDomainMessage(applicationId));

    public async Task SendYandexIdLoginDuplicateNotificationAsync(int applicationId) =>
        await bus.Publish(new YandexIdLoginDuplicated(applicationId));
}
