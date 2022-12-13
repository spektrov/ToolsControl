using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;

namespace ToolsControl.BLL.Services;

public interface IEquipmentTypeService : ICrudService<EquipmentTypeModel>
{
    public Task<PagedList<EquipmentTypeModel>> GetEquipmentTypes(EquipmentTypeParameters parameters);
}