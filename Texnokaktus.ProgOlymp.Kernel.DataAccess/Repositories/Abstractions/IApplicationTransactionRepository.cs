using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Models;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories.Abstractions;

public interface IApplicationTransactionRepository
{
    ApplicationTransaction Add(ApplicationTransactionInsertModel insertModel);
    Task SetStateAsync(int id, State state);
    Task<IList<ApplicationTransaction>> GetPendingTransactions();
}
