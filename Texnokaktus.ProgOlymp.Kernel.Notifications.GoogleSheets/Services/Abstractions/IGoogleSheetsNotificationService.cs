namespace Texnokaktus.ProgOlymp.Kernel.Notifications.GoogleSheets.Services.Abstractions;

public interface IGoogleSheetsNotificationService
{
    Task SendRegistrationSuccessfulNotificationAsync(int applicationId, string yandexIdLogin);
    Task SendInvalidEmailNotificationAsync(int applicationId);
    Task SendIncorrectEmailDomainNotificationAsync(int applicationId);
    Task SendYandexIdLoginDuplicateNotificationAsync(int applicationId, string yandexIdLogin);
}
