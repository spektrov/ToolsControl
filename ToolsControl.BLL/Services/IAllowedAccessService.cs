using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;

namespace ToolsControl.BLL.Services;

public interface IAllowedAccessService : ICrudService<AllowedAccessModel>
{
    public Task<PagedList<AllowedAccessModel>> GetAccesses(AllowedAccessParameters parameters);
}