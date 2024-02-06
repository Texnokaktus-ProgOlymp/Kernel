using Texnokaktus.ProgOlymp.Common.Contracts.Grpc.YandexContest;

namespace Texnokaktus.ProgOlymp.Kernel.Infrastructure.Exceptions;

public class RegistrationException(ErrorType errorType, string message) : Exception(message)
{
    public ErrorType ErrorType { get; } = errorType;
}
