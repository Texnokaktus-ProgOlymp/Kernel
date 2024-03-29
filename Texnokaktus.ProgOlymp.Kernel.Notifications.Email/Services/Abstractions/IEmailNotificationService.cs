namespace Texnokaktus.ProgOlymp.Kernel.Notifications.Email.Services.Abstractions;

public interface IEmailNotificationService
{
    Task SendRegistrationSuccessfulNotificationAsync(string email, string contestUrl, string yandexIdLogin);
    Task SendInvalidEmailNotificationAsync(string email);
    Task SendIncorrectEmailDomainNotificationAsync(string email);
    Task SendYandexIdLoginDuplicateNotificationAsync(string email);
}
