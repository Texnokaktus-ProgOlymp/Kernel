using Texnokaktus.ProgOlymp.Kernel.DataAccess.Context;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Models;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories;

internal class ApplicationRepository(AppDbContext context) : IApplicationRepository
{
    public Application Add(ApplicationInsertModel insertModel)
    {
        var application = new Application
        {
            Submitted = insertModel.Submitted,
            // ContestLocation = insertModel.ContestLocation,
            YandexIdLogin = insertModel.YandexIdLogin,
            Grade = insertModel.Grade,
            PersonalDataConsent = insertModel.PersonalDataConsent,
            ContestStageId = insertModel.ContestStageId,
            Participant = insertModel.Participant,
            School = insertModel.School,
            Parent = insertModel.Parent,
            Teacher = insertModel.Teacher
        };
        return context.Applications.Add(application).Entity;
    }
}
