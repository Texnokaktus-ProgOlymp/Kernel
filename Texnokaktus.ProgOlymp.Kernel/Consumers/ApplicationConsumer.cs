using System.Collections.Frozen;
using MassTransit;
using Texnokaktus.ProgOlymp.Common.Contracts.Messages.GoogleForms;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Models;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Services.Abstractions;
using Texnokaktus.ProgOlymp.Kernel.Models;
using Texnokaktus.ProgOlymp.Kernel.Services.Abstractions;
using State = Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities.State;

namespace Texnokaktus.ProgOlymp.Kernel.Consumers;

public class ApplicationConsumer(ILogger<ApplicationConsumer> logger,
                                 IUnitOfWork unitOfWork,
                                 INotificationService notificationService) : IConsumer<ParticipantApplication>
{
    private static readonly FrozenSet<string> AllowedEmailDomains = new[]
    {
        "ya.ru",
        "yandex.by",
        "yandex.com",
        "yandex.kz",
        "yandex.ru"
    }.ToFrozenSet();

    public async Task Consume(ConsumeContext<ParticipantApplication> context)
    {
        var schoolInsertModel = context.Message.GetSchool();
        var school = unitOfWork.SchoolRepository.Add(schoolInsertModel);

        var participantEmail = context.Message.ParticipantEmail;

        var (yandexIdLogin, yandexLoginStatus) = GetYandexIdLogin(participantEmail);

        var applicationInsertModel = context.Message.GetApplication(yandexIdLogin, school);
        var application = unitOfWork.ApplicationRepository.Add(applicationInsertModel);

        var applicationState = State.Pending;

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (yandexLoginStatus)
        {
            case YandexLoginStatus.InvalidEmail:
                await notificationService.SendInvalidEmailNotificationAsync(context.Message.ApplicationId, participantEmail);
                applicationState = State.Failed;
                break;
            case YandexLoginStatus.IncorrectDomain:
                await notificationService.SendIncorrectEmailDomainNotificationAsync(context.Message.ApplicationId, participantEmail);
                applicationState = State.Failed;
                break;
        }

        unitOfWork.ApplicationTransactionRepository.Add(new(application, applicationState));

        await unitOfWork.SaveChangesAsync();
    }

    private (string? YandexIdLogin, YandexLoginStatus Status) GetYandexIdLogin(string email)
    {
        var emailParts = email.Split('@');
        if (emailParts.Length != 2)
        {
            logger.LogWarning("The email {ParticipantEmail} is invalid", email);
            return (null, YandexLoginStatus.InvalidEmail);
        }

        // ReSharper disable once InvertIf
        if (!AllowedEmailDomains.Contains(emailParts[2]))
        {
            logger.LogWarning("The email {ParticipantEmail} is not allowed by its domain", email);
            return (null, YandexLoginStatus.IncorrectDomain);
        }

        return (emailParts[0], YandexLoginStatus.Defined);
    }
}

file static class MappingExtensions
{
    public static ApplicationInsertModel GetApplication(this ParticipantApplication application,
                                                        string? yandexIdLogin,
                                                        School school) =>
        new(application.ApplicationId,
            application.SubmittedTime,
            yandexIdLogin,
            application.ParticipantGrade,
            application.AgeCategory,
            application.ContestStageId,
            application.ParticipantName,
            application.ParticipantEmail,
            school);

    public static SchoolInsertModel GetSchool(this ParticipantApplication application) =>
        new(application.School, application.SchoolRegion);
}
