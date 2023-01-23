using HealthHarmony.Domain.Entities;

namespace HealthHarmony.Domain.Interfaces;

public interface IRecordRepository : IBaseRepository<Record>
{
    Task<Record> GetByTitleAsync(string title);
    
    Task<bool> ExistByTitleAsync(string title);
}