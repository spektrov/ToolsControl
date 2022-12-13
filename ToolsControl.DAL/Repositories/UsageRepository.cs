using Microsoft.EntityFrameworkCore;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.DAL.Repositories;

public class UsageRepository : Repository<Usage>, IUsageRepository
{
    public UsageRepository(ToolsDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Usage?> GetByIdAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id, false)
            .Include(x => x.Equipment)
            .Include(x => x.Worker)
            .SingleOrDefaultAsync();
    }
}