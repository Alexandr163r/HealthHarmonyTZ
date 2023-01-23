using HealthHarmony.Domain.Entities;
using HealthHarmony.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthHarmony.DAL;

public class RecordRepository : IRecordRepository
{
    private readonly SQLiteDBContext _dbContext;

    public RecordRepository(SQLiteDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Record>> GetAllAsync()
    {
        return await _dbContext.Records.ToListAsync();
    }

    public async Task<bool> ExistByIdAsync(Guid id)
    {
        return await _dbContext.Records.AnyAsync(r => r.Id == id);
    }

    public async Task<Record?> GetByIdAsync(Guid id)
    { 
        var record = await _dbContext.FindAsync<Record>(id);
        
        return record;
    }

    public async Task<Record> CreateAsync(Record entityRecord)
    {
        entityRecord.DateOfCreation = DateTime.Now;
        
        var newRecord = await _dbContext.Records.AddAsync(entityRecord);

        await _dbContext.SaveChangesAsync();

        return newRecord.Entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var record = await _dbContext.Records.FindAsync(id);

        if (record != null) _dbContext.Records.Remove(record);

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(Guid id, Record entityRecord)
    {
        var record = await _dbContext.FindAsync<Record>(id);
        
        if (record == null)
        {
            return false;
        }

        record.Text = entityRecord.Text;
        record.Title = entityRecord.Title;
        record.DateOfModification = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Record> GetByTitleAsync(string title)
    {
        var record = await _dbContext.Records.FirstAsync(e => e.Title.ToLower() == title.ToLower());
        
        return record;
    }

    public async Task<bool> ExistByTitleAsync(string title)
    {
        return await _dbContext.Records.AnyAsync(r => r.Title == title);
    }
}