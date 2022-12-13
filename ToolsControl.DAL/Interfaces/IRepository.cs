﻿using System.Linq.Expressions;

namespace ToolsControl.DAL.Interfaces;

public interface IRepository<T>
{
    public Task<T?> GetByIdAsync(Guid id);
    IQueryable<T> FindAll(bool trackChanges); 
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    void Create(T entity); 
    void Update(T entity); 
    void Delete(T entity);
}