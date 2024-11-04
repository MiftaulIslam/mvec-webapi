using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Entities;
using Server.Repositories.Interfaces;
using Server.Specifications;
using Server.Specifications.Base;

namespace Server.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DataContext _context;

    public GenericRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<T>> ListEntity()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> ListEntity(ISpecification<T> specification)
    {
        IQueryable<T> query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<IReadOnlyList<TResult>> ListEntity<TResult>(ISpecification<T, TResult> specification)
    {
        IQueryable<TResult> query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specification);
        return await query.ToListAsync();
    }

    public async Task<T?> GetEntity(ISpecification<T> specification)
    {
        IQueryable<T> query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
        return await query.AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<T?> GetEntity(int entityId)
    {
        return await _context.Set<T>().FindAsync(entityId);
    }

    public async Task AddEntity(T entity)
    {
        _ = await _context.Set<T>().AddAsync(entity);
    }

    public void UpdateEntity(T entity)
    {
        _ = _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void RemoveEntity(T entity)
    {
        _ = _context.Set<T>().Remove(entity);
    }

    public async Task<bool> Exists(int entityId)
    {
        return await _context.Set<T>().AnyAsync(e => e.Id == entityId);
    }

    public async Task<bool> Save()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}