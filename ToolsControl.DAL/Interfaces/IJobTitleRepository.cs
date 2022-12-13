using ToolsControl.DAL.Entities;

namespace ToolsControl.DAL.Interfaces;

public interface IJobTitleRepository : IRepository<JobTitle>
{
    public Task<JobTitle>? GetById(Guid id);
}