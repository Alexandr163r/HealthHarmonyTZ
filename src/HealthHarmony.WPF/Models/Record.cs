using System;

namespace HealthHarmony.WPF.Models;

public class Record
{
    public string Title { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public Guid Id { get; set; }
    
    public DateTime DateOfCreation { get; set; }

    public DateTime DateOfModification { get; set; }
}