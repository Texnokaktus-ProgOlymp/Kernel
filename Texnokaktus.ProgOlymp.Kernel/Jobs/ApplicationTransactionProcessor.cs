using MassTransit;
using Quartz;
using Texnokaktus.ProgOlymp.Common.Contracts.Messages.YandexContest;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Services.Abstractions;
using State = Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities.State;

namespace Texnokaktus.ProgOlymp.Kernel.Jobs;

public class ApplicationTransactionProcessor(ILogger<ApplicationTransactionProcessor> logger,
                                             IUnitOfWork unitOfWork,
                                             IPublishEndpoint bus) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        foreach (var transaction in await unitOfWork.ApplicationTransactionRepository.GetPendingTransactions())
        {
            logger.LogInformation("Processing the application {ApplicationId}", transaction.Application.Id);
            await unitOfWork.ApplicationTransactionRepository.SetStateAsync(transaction.Id, State.Processing);
            await bus.Publish<RegisterUser>(new(transaction.Id,
                                                transaction.Application.ContestStageId,
                                                transaction.Application.YandexIdLogin));
            await unitOfWork.SaveChangesAsync();
        }
    }
}
