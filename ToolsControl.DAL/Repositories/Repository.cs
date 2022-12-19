using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.DAL.Repositories;

public abstract class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly ToolsDbContext ToolsDbContext;

    public Repository(ToolsDbContext dbContext)
    {
        ToolsDbContext = dbContext;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await ToolsDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ? 
            ToolsDbContext.Set<T>()
                .AsNoTracking() : 
            ToolsDbContext.Set<T>();
    
    
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) => 
        !trackChanges ? 
            ToolsDbContext.Set<T>()
                .Where(expression)
                .AsNoTracking() :
            ToolsDbContext.Set<T>()
                .Where(expression); 
    
    public void Create(T entity) => 
        ToolsDbContext.Set<T>().Add(entity); 
    
    public void Update(T entity) => 
        ToolsDbContext.Set<T>().Update(entity); 
    
    public void Delete(T entity) => 
        ToolsDbContext.Set<T>().Remove(entity);
}