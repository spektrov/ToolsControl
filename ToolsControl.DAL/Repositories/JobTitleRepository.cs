using Microsoft.EntityFrameworkCore;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.DAL.Repositories;

public class JobTitleRepository : Repository<JobTitle>, IJobTitleRepository
{
    public JobTitleRepository(ToolsDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<JobTitle?> GetByIdAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id, false)
            .Include(x => x.Workers)
            .SingleOrDefaultAsync();
    }
}