namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;

public record Application
{
    public int Id { get; set; }
    public DateTime Submitted { get; set; }
    // public string ContestLocation { get; set; }
    public string? YandexIdLogin { get; set; }
    public string Grade { get; set; }
    public bool PersonalDataConsent { get; set; }
    public int ContestStageId { get; set; }
    public Participant Participant { get; set; }
    public int ParticipantId { get; set; }
    public School School { get; set; }
    public int SchoolId { get; set; }
    public Parent Parent { get; set; }
    public int ParentId { get; set; }
    public Teacher? Teacher { get; set; }
    public int? TeacherId { get; set; }
}
