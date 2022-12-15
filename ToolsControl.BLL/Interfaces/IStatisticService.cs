using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.BLL.Models.Responses;

namespace ToolsControl.BLL.Interfaces;

public interface IStatisticService
{
    public Task<PagedList<MostUsedEquipmentResponse>> GetMostUsedEquipment(EquipmentParameters parameters);

    public Task<PagedList<RepairNeedResponse>> GetRepairNeed(EquipmentParameters parameters);

    public Task<PagedList<WorkersWorkTimeResponse>> GetWorkersWorkTime(WorkerParameters parameters);
}