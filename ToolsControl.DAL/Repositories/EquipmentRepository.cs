using Microsoft.EntityFrameworkCore;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.DAL.Repositories;

public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
{
    public EquipmentRepository(ToolsDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Equipment?> GetByIdAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id, false)
            .Include(x => x.Usages)
            .Include(x => x.AllowedAccesses)
            .Include(x => x.Type)
            .SingleOrDefaultAsync();
    }
}