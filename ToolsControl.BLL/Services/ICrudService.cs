namespace ToolsControl.BLL.Services;

public interface ICrudService<T> where  T : class
{
    Task<T> GetByIdAsync(Guid id);

    Task<T> CreateAsync(T model); 
    Task<T> UpdateAsync(T model); 
    Task DeleteByIdAsync(Guid id);
}