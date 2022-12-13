using ToolsControl.DAL.Entities;

namespace ToolsControl.DAL.Interfaces;

public interface IAllowedAccessRepository : IRepository<AllowedAccess>
{
    public Task<AllowedAccess>? GetById(Guid id);
}