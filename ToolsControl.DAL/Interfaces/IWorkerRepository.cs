using ToolsControl.DAL.Entities;

namespace ToolsControl.DAL.Interfaces;

public interface IWorkerRepository : IRepository<Worker>
{
    public Task<Worker>? GetById(Guid id);
}