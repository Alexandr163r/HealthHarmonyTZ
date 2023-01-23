using HealthHarmony.Domain.Entities;

namespace HealthHarmony.Domain.Interfaces;

public interface IRecordServiceValidator
{
    Task<bool> IsValidCreateAsync(Record? record);
    
    Task<bool> IsValidIdAsync(Guid id);
    
    Task<bool> IsValidTitleAsync(string title);
}