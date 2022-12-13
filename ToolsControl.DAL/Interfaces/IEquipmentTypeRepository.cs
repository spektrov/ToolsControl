using ToolsControl.DAL.Entities;

namespace ToolsControl.DAL.Interfaces;

public interface IEquipmentTypeRepository : IRepository<EquipmentType>
{
    public Task<EquipmentType>? GetById(Guid id);
}