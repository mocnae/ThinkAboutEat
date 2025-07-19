using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Data;

public interface IRepository<T>
{
    Task<T> FindById(Guid id);
    Task<T> FindByNameAsync(string name);
    Task<List<T>> Search(string name);
    Task<List<T>> FindByExpression(Func<T, bool> expression);
    Task<T> Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<List<T>> GetAll();
}
