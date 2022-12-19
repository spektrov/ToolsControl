using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.BLL.Models.Responses;

namespace ToolsControl.BLL.Interfaces;

public interface IEquipmentReportGenerator<out T>
{
    public T Generate(IEnumerable<MostUsedEquipmentResponse> models, EquipmentParameters parameters);
}