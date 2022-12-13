using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;

namespace ToolsControl.BLL.Services;

public interface IJobTitleService : ICrudService<JobTitleModel>
{
    public Task<PagedList<JobTitleModel>> GetJobTitles(JobTitleParameters parameters);
}