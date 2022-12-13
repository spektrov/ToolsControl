using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;

namespace ToolsControl.BLL.Interfaces;

public interface IWorkerService : ICrudService<WorkerModel>
{
    public Task<PagedList<WorkerModel>> GetWorkers(WorkerParameters parameters);
}