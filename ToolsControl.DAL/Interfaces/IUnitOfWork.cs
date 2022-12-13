namespace ToolsControl.DAL.Interfaces;

public interface IUnitOfWork
{
    public IAllowedAccessRepository AccessRepository { get; }
    
    public IEquipmentRepository EquipmentRepository { get; }
    
    public IEquipmentTypeRepository EquipmentTypeRepository { get; }
    
    public IJobTitleRepository JobTitleRepository { get; }
    
    public IUsageRepository UsageRepository { get; }
    
    public IWorkerRepository WorkerRepository { get; }
    
    public Task SaveAsync();

    public void ClearTracking();
}