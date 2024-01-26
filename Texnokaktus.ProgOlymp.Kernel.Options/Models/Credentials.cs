namespace Texnokaktus.ProgOlymp.Kernel.Options.Models;

public record Credentials
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}
