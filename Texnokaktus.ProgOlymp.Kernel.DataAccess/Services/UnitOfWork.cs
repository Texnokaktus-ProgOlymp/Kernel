using Texnokaktus.ProgOlymp.Kernel.DataAccess.Context;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories.Abstractions;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Services;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal class UnitOfWork(AppDbContext context,
                          IApplicationRepository applicationRepository,
                          IParentRepository parentRepository,
                          IParticipantRepository participantRepository,
                          ISchoolRepository schoolRepository,
                          ITeacherRepository teacherRepository) : IUnitOfWork
{
    public IApplicationRepository ApplicationRepository { get; } = applicationRepository;
    public IParentRepository ParentRepository { get; } = parentRepository;
    public IParticipantRepository ParticipantRepository { get; } = participantRepository;
    public ISchoolRepository SchoolRepository { get; } = schoolRepository;
    public ITeacherRepository TeacherRepository { get; } = teacherRepository;
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}
