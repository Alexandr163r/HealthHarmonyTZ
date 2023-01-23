using HealthHarmony.Domain.Entities;
using HealthHarmony.Domain.Interfaces;

namespace HealthHarmony.BL.Services;

public class RecordService : IRecordService
{
    private readonly IRecordRepository _repository;

    public RecordService(IRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Record>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<bool> DeleteRecordAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<bool> UpdateRecordAsync(Guid id, Record record)
    {
        return await _repository.UpdateAsync(id, record);
    }

    public async Task<Record> CreateRecordAsync(Record record)
    {
        return await _repository.CreateAsync(record);
    }

    public async Task<Record> GetRecordByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }
}