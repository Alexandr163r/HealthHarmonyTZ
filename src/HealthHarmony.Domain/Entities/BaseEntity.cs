namespace HealthHarmony.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public DateTime DateOfCreation { get; set; }

    public DateTime DateOfModification { get; set; }
}