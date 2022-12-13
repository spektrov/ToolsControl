using ToolsControl.DAL.Interfaces;

namespace ToolsControl.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly ToolsDbContext _dbContext;
    
    public IAllowedAccessRepository AccessRepository { get; }
    
    public IEquipmentRepository EquipmentRepository { get; }
    
    public IEquipmentTypeRepository EquipmentTypeRepository { get; }
    
    public IJobTitleRepository JobTitleRepository { get; }
    
    public IUsageRepository UsageRepository { get; }
    
    public IWorkerRepository WorkerRepository { get; }
    
    public ITokenRepository TokenRepository { get; }
    
    public IUserRepository UserRepository { get; }


    public UnitOfWork(ToolsDbContext dbContext,
        IAllowedAccessRepository accessRepository,
        IEquipmentRepository equipmentRepository,
        IEquipmentTypeRepository equipmentTypeRepository,
        IJobTitleRepository jobTitleRepository, 
        IUsageRepository usageRepository,
        IWorkerRepository workerRepository, 
        ITokenRepository tokenRepository, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        AccessRepository = accessRepository;
        EquipmentRepository = equipmentRepository;
        EquipmentTypeRepository = equipmentTypeRepository;
        JobTitleRepository = jobTitleRepository;
        UsageRepository = usageRepository;
        WorkerRepository = workerRepository;
        TokenRepository = tokenRepository;
        UserRepository = userRepository;
    }


    public async Task SaveAsync() => await _dbContext.SaveChangesAsync();

    public void ClearTracking() => _dbContext.ChangeTracker.Clear();
    
}