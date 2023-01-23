using HealthHarmony.Domain.Entities;

namespace HealthHarmony.Domain.Interfaces;

public interface IRecordService
{
    Task<List<Record>> GetAllAsync();
    
    Task<bool> DeleteRecordAsync(Guid id);
    
    Task<bool> UpdateRecordAsync(Guid id, Record record);
    
    Task<Record> CreateRecordAsync(Record record);
    
    Task<Record> GetRecordByIdAsync(Guid id);
}