namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;

public class ApplicationTransaction
{
    public int Id { get; set; }
    public Application Application { get; set; }
    public int ApplicationId { get; set; }
    public State State { get; set; }
}
