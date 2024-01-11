using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Context;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories.Abstractions;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Services;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess;

public static class DiExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services,
                                                   Action<DbContextOptionsBuilder> optionsAction) =>
        services.AddDbContext<AppDbContext>(optionsAction)
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IApplicationRepository, ApplicationRepository>()
                .AddScoped<IParentRepository, ParentRepository>()
                .AddScoped<IParticipantRepository, ParticipantRepository>()
                .AddScoped<ISchoolRepository, SchoolRepository>()
                .AddScoped<ITeacherRepository, TeacherRepository>();
}
