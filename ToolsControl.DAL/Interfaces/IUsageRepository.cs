using ToolsControl.DAL.Entities;

namespace ToolsControl.DAL.Interfaces;

public interface IUsageRepository : IRepository<Usage>
{
    public Task<Usage>? GetById(Guid id);
}