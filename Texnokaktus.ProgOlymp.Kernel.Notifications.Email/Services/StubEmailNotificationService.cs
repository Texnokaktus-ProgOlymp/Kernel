using Microsoft.Extensions.Logging;
using Texnokaktus.ProgOlymp.Kernel.Notifications.Email.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.Notifications.Email.Services;

public class StubEmailNotificationService(ILogger<StubEmailNotificationService> logger) : IEmailNotificationService
{
    public Task SendRegistrationSuccessfulNotificationAsync(string email, string contestUrl, string yandexIdLogin)
    {
        logger.LogInformation("Sent an email about successful registration with data: {@EmailData}",
                              new
                              {
                                  Email = email,
                                  ContestUrl = contestUrl,
                                  YandexIdLogin = yandexIdLogin
                              });
        return Task.CompletedTask;
    }

    public Task SendInvalidEmailNotificationAsync(string email)
    {
        logger.LogInformation("Sent an email about invalid email to {Email}", email);
        return Task.CompletedTask;
    }

    public Task SendIncorrectEmailDomainNotificationAsync(string email)
    {
        logger.LogInformation("Sent an email about incorrect email domain to {Email}", email);
        return Task.CompletedTask;
    }

    public Task SendYandexIdLoginDuplicateNotificationAsync(string email)
    {
        logger.LogInformation("Sent an email about Yandex ID login duplication to {Email}", email);
        return Task.CompletedTask;
    }
}
