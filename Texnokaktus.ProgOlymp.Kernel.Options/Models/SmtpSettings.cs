namespace Texnokaktus.ProgOlymp.Kernel.Options.Models;

public record SmtpSettings
{
    public required SmtpServer Server { get; init; }
    public required Credentials Credentials { get; init; }
    public required Sender Sender { get; init; }
}
