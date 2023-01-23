using HealthHarmony.Domain.Entities;
using HealthHarmony.Domain.Interfaces;

namespace HealthHarmony.BL.Services;

public class RecordServiceValidator : IRecordServiceValidator
{
    private readonly IRecordRepository _repository;


    public RecordServiceValidator(IRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> IsValidCreateAsync(Record? record)
    {
        if (record == null)
        {
            return false;
        }

        if (string.IsNullOrEmpty(record.Title))
        {
            return false;
        }

        var existTitle = await _repository.ExistByTitleAsync(record.Title);

        if (existTitle)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> IsValidIdAsync(Guid id)
    {
        return await _repository.ExistByIdAsync(id);
    }

    public async Task<bool> IsValidTitleAsync(string title)
    {
        return await _repository.ExistByTitleAsync(title);
    }
}