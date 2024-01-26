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
            GoogleServiceApplicationId = insertModel.GoogleServiceApplicationId,
            Submitted = insertModel.Submitted,
            // ContestLocation = insertModel.ContestLocation,
            YandexIdLogin = insertModel.YandexIdLogin,
            Grade = insertModel.Grade,
            AgeCategory = insertModel.AgeCategory,
            ContestStageId = insertModel.ContestStageId,
            Name = insertModel.Name,
            Email = insertModel.Email,
            School = insertModel.School
        };
        return context.Applications.Add(application).Entity;
    }
}
