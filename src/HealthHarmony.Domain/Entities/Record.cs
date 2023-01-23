namespace HealthHarmony.Domain.Entities;

public class Record : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;
}