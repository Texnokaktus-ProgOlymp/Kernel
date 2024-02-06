using Quartz;
using Texnokaktus.ProgOlymp.Common.Contracts.Grpc.YandexContest;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Services.Abstractions;
using Texnokaktus.ProgOlymp.Kernel.Infrastructure.Clients.Abstractions;
using Texnokaktus.ProgOlymp.Kernel.Infrastructure.Exceptions;
using Texnokaktus.ProgOlymp.Kernel.Services.Abstractions;
using State = Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities.State;

namespace Texnokaktus.ProgOlymp.Kernel.Jobs;

public class ApplicationTransactionProcessor(ILogger<ApplicationTransactionProcessor> logger,
                                             IUnitOfWork unitOfWork,
                                             IRegistrationServiceClient registrationServiceClient,
                                             INotificationService notificationService) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        foreach (var transaction in await unitOfWork.ApplicationTransactionRepository.GetPendingTransactions())
        {
            logger.LogInformation("Processing the application {ApplicationId}", transaction.Application.Id);

            if (transaction.Application.YandexIdLogin == null)
            {
                logger.LogError("The application {ApplicationId} does not have a YandexIdLogin", transaction.Application.Id);
                await unitOfWork.ApplicationTransactionRepository.SetStateAsync(transaction.Id, State.Failed);
                await unitOfWork.SaveChangesAsync();
                continue;
            }

            try
            {
                var contestUrl = await registrationServiceClient.RegisterParticipantAsync(transaction.Application.ContestStageId, 
                                                                                          transaction.Application.YandexIdLogin);
                await unitOfWork.ApplicationTransactionRepository.SetStateAsync(transaction.Id, State.Completed);
                await notificationService.SendRegistrationSuccessfulNotificationAsync(transaction.ApplicationId,
                                                                                      transaction.Application.Email,
                                                                                      contestUrl,
                                                                                      transaction.Application.YandexIdLogin);
            }
            catch (RegistrationException e) when (e.ErrorType == ErrorType.InvalidUser)
            {
                logger.LogWarning(e, "Handling invalid Yandex user in transaction {TransactionId}", transaction.Id);
                await notificationService.SendInvalidEmailNotificationAsync(transaction.ApplicationId,
                                                                            transaction.Application.Email);
                await unitOfWork.ApplicationTransactionRepository.SetStateAsync(transaction.Id, State.Failed);
            }
            catch (RegistrationException e) when (e.ErrorType == ErrorType.UserIsAlreadyRegistered)
            {
                logger.LogWarning(e, "Handling duplicated Yandex user in application {TransactionId}", transaction.Id);
                await notificationService.SendYandexIdLoginDuplicateNotificationAsync(transaction.ApplicationId, 
                                                                                      transaction.Application.Email,
                                                                                      transaction.Application.YandexIdLogin);
                await unitOfWork.ApplicationTransactionRepository.SetStateAsync(transaction.Id, State.Failed);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred when processing application {TransactionId}", transaction.Id);
                await unitOfWork.ApplicationTransactionRepository.SetStateAsync(transaction.Id, State.Failed);
            }
            finally
            {
                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
