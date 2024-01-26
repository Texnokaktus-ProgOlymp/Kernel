using Texnokaktus.ProgOlymp.Kernel.DataAccess.Context;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories.Abstractions;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Services;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal class UnitOfWork(AppDbContext context,
                          IApplicationRepository applicationRepository,
                          IApplicationTransactionRepository applicationTransactionRepository,
                          ISchoolRepository schoolRepository) : IUnitOfWork
{
    public IApplicationRepository ApplicationRepository { get; } = applicationRepository;

    public IApplicationTransactionRepository ApplicationTransactionRepository { get; } = applicationTransactionRepository;
    public ISchoolRepository SchoolRepository { get; } = schoolRepository;
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}
