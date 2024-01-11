namespace Texnokaktus.ProgOlymp.Kernel.Models.Configuration;

public record JobSettings
{
    public required JobConfiguration ApplicationTransactionProcessor { get; init; }

    public void Deconstruct(out JobConfiguration applicationTransactionProcessor)
    {
        applicationTransactionProcessor = ApplicationTransactionProcessor;
    }
}
