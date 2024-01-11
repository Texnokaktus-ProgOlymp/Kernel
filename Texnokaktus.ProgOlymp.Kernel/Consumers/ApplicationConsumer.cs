using MassTransit;
using Texnokaktus.ProgOlymp.Common.Contracts.Messages.GoogleForms;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Models;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.Consumers;

public class ApplicationConsumer(ILogger<ApplicationConsumer> logger, IUnitOfWork unitOfWork) : IConsumer<ParticipantApplication>
{
    public async Task Consume(ConsumeContext<ParticipantApplication> context)
    {
        logger.LogInformation("Consuming");

        var participantInsertModel = context.Message.GetParticipant();
        var participant = unitOfWork.ParticipantRepository.Add(participantInsertModel);

        var schoolInsertModel = context.Message.GetSchool();
        var school = unitOfWork.SchoolRepository.Add(schoolInsertModel);

        var parentInsertModel = context.Message.GetParent();
        var parent = unitOfWork.ParentRepository.Add(parentInsertModel);

        var teacherInsertModel = context.Message.GetTeacher();
        var teacher = teacherInsertModel is not null ? unitOfWork.TeacherRepository.Add(teacherInsertModel) : null;

        var applicationInsertModel = context.Message.GetApplication(participant, school, parent, teacher);
        unitOfWork.ApplicationRepository.Add(applicationInsertModel);
       
        await unitOfWork.SaveChangesAsync();
    }
}

file static class MappingExtensions
{
    public static ApplicationInsertModel GetApplication(this ParticipantApplication application,
                                                        Participant participant,
                                                        School school,
                                                        Parent parent,
                                                        Teacher? teacher) =>
        new(application.SubmittedTime,
            application.ContestLocation,
            application.YandexIdLogin,
            application.ParticipantGrade,
            application.PersonalDataConsent,
            application.ContestStageId,
            participant,
            school,
            parent,
            teacher);

    public static ParentInsertModel GetParent(this ParticipantApplication application) =>
        new(application.ParentName, application.ParentEmail, application.ParentPhone);

    public static ParticipantInsertModel GetParticipant(this ParticipantApplication application) =>
        new(application.ParticipantName, application.ParticipantEmail, application.BirthDate);

    public static SchoolInsertModel GetSchool(this ParticipantApplication application) =>
        new(application.School, application.SchoolRegion);

    public static TeacherInsertModel? GetTeacher(this ParticipantApplication application) =>
        application.TeacherName is not null
     || application.TeacherSchool is not null
     || application.TeacherEmail is not null
     || application.TeacherPhone is not null
            ? new(application.TeacherName, application.TeacherSchool, application.TeacherName, application.TeacherPhone)
            : null;
}
