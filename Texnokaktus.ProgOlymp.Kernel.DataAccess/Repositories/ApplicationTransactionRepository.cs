using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Context;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Exceptions;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Models;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories;

internal class ApplicationTransactionRepository(AppDbContext context) : IApplicationTransactionRepository
{
    public ApplicationTransaction Add(ApplicationTransactionInsertModel insertModel)
    {
        var applicationTransaction = new ApplicationTransaction()
        {
            Application = insertModel.Application,
            State = insertModel.State
        };
        return context.ApplicationTransactions.Add(applicationTransaction).Entity;
    }

    public async Task UpdateAsync(int id, Action<ApplicationTransaction> updateAction)
    {
        var transaction = await context.ApplicationTransactions.FindAsync(id)
                       ?? throw new EntityNotFoundException<ApplicationTransaction>($"Could not find the {nameof(ApplicationTransaction)} with ID {id}.");
        updateAction.Invoke(transaction);
    }

    public async Task<IList<ApplicationTransaction>> GetPendingTransactions() =>
        await context.ApplicationTransactions
                     .AsNoTracking()
                     .Include(transaction => transaction.Application)
                     .Where(transaction => transaction.State == State.Pending)
                     .ToListAsync();
}
