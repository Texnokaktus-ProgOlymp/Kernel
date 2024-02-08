using Microsoft.Extensions.Logging;
using Texnokaktus.ProgOlymp.Kernel.Notifications.GoogleSheets.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.Notifications.GoogleSheets.Services;

internal class StubGoogleSheetsNotificationService(ILogger<StubGoogleSheetsNotificationService> logger) : IGoogleSheetsNotificationService
{
    public Task SendRegistrationSuccessfulNotificationAsync(int applicationId, string yandexIdLogin)
    {
        logger.LogInformation("Wrote to a table that registration is successful: {@EmailData}",
                              new
                              {
                                  ApplicationId = applicationId,
                                  YandexIdLogin = yandexIdLogin
                              });
        return Task.CompletedTask;
    }

    public Task SendInvalidEmailNotificationAsync(int applicationId)
    {
        logger.LogInformation("Wrote to a table that email in application {ApplicationId} is invalid", applicationId);
        return Task.CompletedTask;
    }

    public Task SendIncorrectEmailDomainNotificationAsync(int applicationId)
    {
        logger.LogInformation("Wrote to a table that email in application {ApplicationId} is in incorrect domain", applicationId);
        return Task.CompletedTask;
    }

    public Task SendYandexIdLoginDuplicateNotificationAsync(int applicationId, string yandexIdLogin)
    {
        logger.LogInformation("Wrote to a table that Yandex ID login in application is duplicated: {@EmailData}",
                              new
                              {
                                  ApplicationId = applicationId,
                                  YandexIdLogin = yandexIdLogin
                              });
        return Task.CompletedTask;
    }
}
