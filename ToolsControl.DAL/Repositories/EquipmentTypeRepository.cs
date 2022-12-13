using Microsoft.EntityFrameworkCore;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.DAL.Repositories;

public class EquipmentTypeRepository : Repository<EquipmentType>, IEquipmentTypeRepository
{
    public EquipmentTypeRepository(ToolsDbContext dbContext) : base(dbContext)
    {
    }
    
    public override async Task<EquipmentType?> GetByIdAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id, false)
            .Include(x => x.Equipments)
            .SingleOrDefaultAsync();
    }
}