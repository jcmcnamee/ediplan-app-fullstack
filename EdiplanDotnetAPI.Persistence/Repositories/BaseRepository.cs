using EdiplanDotnetAPI.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EdiplanDotnetAPI.Persistence.Repositories;

// Where type should be a class (where T : class)
public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
        private readonly EdiplanDbContext _dbContext;
    public BaseRepository(EdiplanDbContext dbContext)
    {
            _dbContext = dbContext;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<T> GetByIdAsync<TId>(TId id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}
