using Microsoft.EntityFrameworkCore;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.DAL.Repositories;

public class WorkerRepository : Repository<Worker>, IWorkerRepository
{
    public WorkerRepository(ToolsDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Worker?> GetByIdAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id, false)
            .Include(x => x.Title)
            .Include(x => x.Usages)
            .Include(x => x.AllowedAccesses)
            .SingleOrDefaultAsync();
    }
}