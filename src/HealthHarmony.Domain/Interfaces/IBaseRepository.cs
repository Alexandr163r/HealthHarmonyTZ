namespace HealthHarmony.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();

    Task<bool> ExistByIdAsync(Guid id);
    
    Task<T> GetByIdAsync(Guid id);
    
    Task<T> CreateAsync(T entity);
    
    Task<bool> DeleteAsync(Guid id);
    
    Task<bool> UpdateAsync(Guid id, T entity);
}