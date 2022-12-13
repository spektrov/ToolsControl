using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;

namespace ToolsControl.BLL.Interfaces;

public interface IAllowedAccessService : ICrudService<AllowedAccessModel>
{
    public Task<PagedList<AllowedAccessModel>> GetAccesses(AllowedAccessParameters parameters);
}