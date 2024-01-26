using Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Services.Abstractions;

public interface IUnitOfWork
{
    IApplicationRepository ApplicationRepository { get; }
    IApplicationTransactionRepository ApplicationTransactionRepository { get; }
    ISchoolRepository SchoolRepository { get; }
    Task SaveChangesAsync();
}
