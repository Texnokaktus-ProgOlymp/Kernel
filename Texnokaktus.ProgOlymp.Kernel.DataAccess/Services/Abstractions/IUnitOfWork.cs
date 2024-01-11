using Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Services.Abstractions;

public interface IUnitOfWork
{
    IApplicationRepository ApplicationRepository { get; }
    IParentRepository ParentRepository { get; }
    IParticipantRepository ParticipantRepository { get; }
    ISchoolRepository SchoolRepository { get; }
    ITeacherRepository TeacherRepository { get; }
    Task SaveChangesAsync();
}
