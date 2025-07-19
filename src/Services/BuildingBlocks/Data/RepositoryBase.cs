using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Data;

public abstract class RepositoryBase<T> : IRepository<T> where T : BaseModel
{
    protected DbContext _context { get; set; }

    public RepositoryBase(DbContext context)
    {
        _context = context;
    }

    public async Task<T> Create(T entity)
    {
        _context.Set<T>().Add(entity);

        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task Delete(T entity)
    {
        _context.Set<T>().Remove(entity);

        await _context.SaveChangesAsync();
    }

    public async Task<List<T>> FindByExpression(Func<T, bool> expression)
    {
        var query = _context.Set<T>().Where(expression).ToList();

        return query;
    }

    public async Task<T> FindById(Guid id)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T> FindByNameAsync(string name)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task Update(T entity)
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<T>> Search(string name)
    {
        return await _context.Set<T>().Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
    }

    public async Task<List<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }
}
