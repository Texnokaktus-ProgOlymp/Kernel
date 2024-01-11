namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;

public record Participant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
}
