using Quartz;
using Texnokaktus.ProgOlymp.Common.Contracts.Grpc.YandexContest;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Services.Abstractions;
using Texnokaktus.ProgOlymp.Kernel.Infrastructure.Clients.Abstractions;
using Texnokaktus.ProgOlymp.Kernel.Infrastructure.Exceptions;
using Texnokaktus.ProgOlymp.Kernel.Services.Abstractions;

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
                await unitOfWork.ApplicationTransactionRepository.UpdateAsync(transaction.Id, applicationTransaction => applicationTransaction.State = State.Failed);
                await unitOfWork.SaveChangesAsync();
                continue;
            }

            try
            {
                var contestUrl = await registrationServiceClient.RegisterParticipantAsync(transaction.Application.ContestStageId, 
                                                                                          transaction.Application.YandexIdLogin);
                await unitOfWork.ApplicationTransactionRepository.UpdateAsync(transaction.Id, applicationTransaction =>
                {
                    applicationTransaction.State = State.Completed;
                    applicationTransaction.ErrorCode = null;
                });
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
                await unitOfWork.ApplicationTransactionRepository.UpdateAsync(transaction.Id, applicationTransaction =>
                {
                    applicationTransaction.State = State.Failed;
                    applicationTransaction.ErrorCode = ErrorCode.InvalidUser;
                });
            }
            catch (RegistrationException e) when (e.ErrorType == ErrorType.UserIsAlreadyRegistered)
            {
                logger.LogWarning(e, "Handling duplicated Yandex user in application {TransactionId}", transaction.Id);
                await notificationService.SendYandexIdLoginDuplicateNotificationAsync(transaction.ApplicationId, 
                                                                                      transaction.Application.Email,
                                                                                      transaction.Application.YandexIdLogin);
                await unitOfWork.ApplicationTransactionRepository.UpdateAsync(transaction.Id, applicationTransaction =>
                {
                    applicationTransaction.State = State.Failed;
                    applicationTransaction.ErrorCode = ErrorCode.UserIsAlreadyRegistered;
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred when processing application {TransactionId}", transaction.Id);
                await unitOfWork.ApplicationTransactionRepository.UpdateAsync(transaction.Id, applicationTransaction =>
                {
                    applicationTransaction.State = State.Failed;
                    applicationTransaction.ErrorCode = ErrorCode.Generic;
                });
            }
            finally
            {
                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
