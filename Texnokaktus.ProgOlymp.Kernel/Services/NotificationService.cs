using Texnokaktus.ProgOlymp.Kernel.Notifications.Email.Services.Abstractions;
using Texnokaktus.ProgOlymp.Kernel.Notifications.GoogleSheets.Services.Abstractions;
using Texnokaktus.ProgOlymp.Kernel.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.Services;

internal class NotificationService(IEmailNotificationService emailNotificationService,
                                   IGoogleSheetsNotificationService googleSheetsNotificationService)
    : INotificationService
{
    public async Task SendRegistrationSuccessfulNotificationAsync(int applicationId,
                                                                  string email,
                                                                  string contestUrl,
                                                                  string yandexIdLogin) =>
        await Task.WhenAll(emailNotificationService.SendRegistrationSuccessfulNotificationAsync(email, contestUrl, yandexIdLogin),
                           googleSheetsNotificationService.SendRegistrationSuccessfulNotificationAsync(applicationId, yandexIdLogin));

    public async Task SendInvalidEmailNotificationAsync(int applicationId, string email) =>
        await Task.WhenAll(emailNotificationService.SendInvalidEmailNotificationAsync(email),
                           googleSheetsNotificationService.SendInvalidEmailNotificationAsync(applicationId));

    public async Task SendIncorrectEmailDomainNotificationAsync(int applicationId, string email) =>
        await Task.WhenAll(emailNotificationService.SendIncorrectEmailDomainNotificationAsync(email),
                           googleSheetsNotificationService.SendIncorrectEmailDomainNotificationAsync(applicationId));

    public async Task SendYandexIdLoginDuplicateNotificationAsync(int applicationId, string email, string yandexIdLogin) =>
        await Task.WhenAll(emailNotificationService.SendYandexIdLoginDuplicateNotificationAsync(email),
                           googleSheetsNotificationService.SendYandexIdLoginDuplicateNotificationAsync(applicationId, yandexIdLogin));
}
