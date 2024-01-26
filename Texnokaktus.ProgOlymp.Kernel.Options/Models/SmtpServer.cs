namespace Texnokaktus.ProgOlymp.Kernel.Options.Models;

public record SmtpServer
{
    public required string Host { get; init; }
    public required int Port { get; init; }
}
