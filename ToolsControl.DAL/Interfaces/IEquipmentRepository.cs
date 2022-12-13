using ToolsControl.DAL.Entities;

namespace ToolsControl.DAL.Interfaces;

public interface IEquipmentRepository : IRepository<Equipment>
{
    public Task<Equipment>? GetById(Guid id);
}