namespace HealthHarmony.Models;

public class RecordReqeustModel
{
    public string Title { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;
    
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public DateTime DateOfCreation { get; set; }

    public DateTime DateOfModification { get; set; }
}