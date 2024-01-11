namespace Texnokaktus.ProgOlymp.Kernel.Models.Configuration;

public record JobConfiguration
{
    public required string Schedule { get; init; }

    public void Deconstruct(out string schedule)
    {
        schedule = Schedule;
    }
}
