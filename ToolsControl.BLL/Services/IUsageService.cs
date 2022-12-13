using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;

namespace ToolsControl.BLL.Services;

public interface IUsageService : ICrudService<UsageModel>
{
    public Task<PagedList<UsageModel>> GetUsages(UsageParameters parameters);
}