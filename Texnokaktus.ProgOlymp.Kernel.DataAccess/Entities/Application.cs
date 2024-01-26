namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;

public record Application
{
    public int Id { get; init; }
    public required int GoogleServiceApplicationId { get; init; }
    public required DateTime Submitted { get; init; }
    // public required string ContestLocation { get; set; }
    public required string? YandexIdLogin { get; init; }
    public required string Grade { get; init; }
    public required string AgeCategory { get; init; }
    public required int ContestStageId { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public School School { get; init; }
    public int SchoolId { get; init; }
}
