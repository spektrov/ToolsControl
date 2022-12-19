using ToolsControl.BLL.Extensions.Utility;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.DAL.Entities;
using System.Linq.Dynamic.Core;

namespace ToolsControl.BLL.Extensions;

public static class EquipmentServiceExtension
{
    
    public static IQueryable<Equipment> Search(
        this IQueryable<Equipment> equipments, string? query)
    {
        if (string.IsNullOrWhiteSpace(query)) return equipments;

        var lowerCaseTerm = query.Trim().ToLower();

        return equipments.Where(x => x.Name != null && x.Name.ToLower().Contains(lowerCaseTerm));
    }

    
    public static IQueryable<Equipment> Sort(
        this IQueryable<Equipment> equipments, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString)) return equipments;

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Equipment>(orderByQueryString);

        return string.IsNullOrWhiteSpace(orderQuery) 
            ? equipments.OrderBy(e => e.Name) 
            : equipments.OrderBy(orderQuery);
    }
}