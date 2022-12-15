using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.BLL.Models.Responses;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.BLL.Services;

public class StatisticService : IStatisticService
{
    private readonly IUnitOfWork _unitOfWork;

    public StatisticService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<MostUsedEquipmentResponse>> GetMostUsedEquipment(EquipmentParameters parameters)
    {
        if (parameters.StartPeriod == null || parameters.EndPeriod == null)
        {
           return PagedList<MostUsedEquipmentResponse>
               .ToPagedList(new List<MostUsedEquipmentResponse>(), parameters.PageNumber, parameters.PageSize);
        }
        
        var usages = _unitOfWork.UsageRepository
            .FindByCondition(x =>
                x.Start >= parameters.StartPeriod && x.Finish <= parameters.EndPeriod, false)
            .GroupBy(x => x.EquipmentId);

        var mostUsed = usages.Select(x => 
            new MostUsedEquipmentResponse
            {
                Name = x.Select(y => y.Equipment).FirstOrDefault()!.Name,
                TypeName = x.Select(y => y.Equipment).FirstOrDefault()!.Type!.Name,
                IsAbleToWork = x.Select(y => y.Equipment).FirstOrDefault()!.IsAbleToWork,
                LastInspection = x.Select(y => y.Equipment).FirstOrDefault()!.LastInspection,
                Time = x.Sum(r => r.Finish!.Value.Hour - r.Start.Hour)
            }
        )
            .OrderByDescending(x => x.Time).ToList();

        if (parameters.TypeName != null)
        {
            mostUsed = mostUsed.Where(x => x.TypeName == parameters.TypeName).ToList();
        }
        
        return PagedList<MostUsedEquipmentResponse>.ToPagedList(mostUsed, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<PagedList<RepairNeedResponse>> GetRepairNeed(EquipmentParameters parameters)
    {
        var usages = _unitOfWork.UsageRepository
            .FindAll(false)
            .Where(x => x.Start > x.Equipment!.LastInspection)
            .GroupBy(x => x.EquipmentId);
        
        var repairNeed = usages.Select(x =>
            new RepairNeedResponse
            {
                Name = x.Select(y => y.Equipment).FirstOrDefault()!.Name,
                TypeName = x.Select(y => y.Equipment).FirstOrDefault()!.Type!.Name,
                LastInspection = x.Select(y => y.Equipment).FirstOrDefault()!.LastInspection,
                RepairNeed = x
                    .Sum(r => r.Finish!.Value.Hour - r.Start.Hour) >
                             x.Select(y => y.Equipment).FirstOrDefault()!.Type!.InspectionPeriod
                            
            }
        ).ToList();
        
        if (parameters.TypeName != null)
        {
            repairNeed = repairNeed.Where(x => x.TypeName == parameters.TypeName).ToList();
        }
        
        return PagedList<RepairNeedResponse>.ToPagedList(repairNeed, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<PagedList<WorkersWorkTimeResponse>> GetWorkersWorkTime(WorkerParameters parameters)
    {
        var usages = _unitOfWork.UsageRepository
            .FindByCondition(x =>
                x.Start >= parameters.StartPeriod && x.Finish <= parameters.EndPeriod, false)
            .GroupBy(x => x.WorkerId);

        var mostWorked = usages.Select(x => 
                new WorkersWorkTimeResponse()
                {
                    FullName = x.Select(y => y.Worker).FirstOrDefault()!.User!.FirstName,
                    HiringDate = x.Select(y => y.Worker).FirstOrDefault()!.HiringDate,
                    SalaryValue = x.Select(y => y.Worker).FirstOrDefault()!.SalaryValue,
                    Title = x.Select(y => y.Worker).FirstOrDefault()!.Title!.Name,
                    Time = x.Sum(r => r.Finish!.Value.Hour - r.Start.Hour)
                }
            )
            .OrderByDescending(x => x.Time).ToList();

        if (parameters.JobTitle != null)
        {
            mostWorked = mostWorked.Where(x => x.Title == parameters.JobTitle).ToList();
        }
        
        return PagedList<WorkersWorkTimeResponse>.ToPagedList(mostWorked, parameters.PageNumber, parameters.PageSize);
    }
}