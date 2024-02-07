using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Models;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories.Abstractions;

public interface IApplicationTransactionRepository
{
    ApplicationTransaction Add(ApplicationTransactionInsertModel insertModel);
    Task UpdateAsync(int id, Action<ApplicationTransaction> updateAction);
    Task<IList<ApplicationTransaction>> GetPendingTransactions();
}
