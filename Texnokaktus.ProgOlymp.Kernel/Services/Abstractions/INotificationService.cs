namespace Texnokaktus.ProgOlymp.Kernel.Services.Abstractions;

public interface INotificationService
{
    Task SendRegistrationSuccessfulNotificationAsync(int applicationId,
                                                     string email,
                                                     string contestUrl,
                                                     string yandexIdLogin);

    Task SendInvalidEmailNotificationAsync(int applicationId, string email);
    Task SendIncorrectEmailDomainNotificationAsync(int applicationId, string email);
    Task SendYandexIdLoginDuplicateNotificationAsync(int applicationId, string email);
}
