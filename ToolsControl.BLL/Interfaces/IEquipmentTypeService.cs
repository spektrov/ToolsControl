using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;

namespace ToolsControl.BLL.Interfaces;

public interface IEquipmentTypeService : ICrudService<EquipmentTypeModel>
{
    public Task<PagedList<EquipmentTypeModel>> GetEquipmentTypes(EquipmentTypeParameters parameters);
}