namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;

public record Parent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
}
