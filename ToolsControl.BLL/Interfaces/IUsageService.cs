using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;

namespace ToolsControl.BLL.Interfaces;

public interface IUsageService : ICrudService<UsageModel>
{
    public Task<PagedList<UsageModel>> GetUsages(UsageParameters parameters);
}