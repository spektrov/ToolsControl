using Microsoft.EntityFrameworkCore;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.DAL.Repositories;

public class AllowedAccessRepository  : Repository<AllowedAccess>, IAllowedAccessRepository
{
    public AllowedAccessRepository(ToolsDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<AllowedAccess?> GetByIdAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id, false)
            .Include(x => x.Equipment)
            .Include(x => x.Worker)
            .SingleOrDefaultAsync();
    }
}