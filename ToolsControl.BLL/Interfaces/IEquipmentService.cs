using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;

namespace ToolsControl.BLL.Interfaces;

public interface IEquipmentService : ICrudService<EquipmentModel>
{
    public Task<PagedList<EquipmentModel>> GetEquipments(EquipmentParameters parameters);


    public Task<IEnumerable<EquipmentModel>> GetAvailableEquipments(string type);

    public Task<bool> VerifyAccess(Guid id, string cardNumber);
}