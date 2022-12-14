using System.Linq.Dynamic.Core;
using ToolsControl.BLL.Extensions.Utility;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.DAL.Entities;

namespace ToolsControl.BLL.Extensions;

public static class UserServiceExtension
{
    public static IQueryable<User> Filter(this IQueryable<User> users) => users;
    
    
    public static IQueryable<User> Search(
        this IQueryable<User> users, UserParameters parameters)
    {
        if (string.IsNullOrWhiteSpace(parameters.SearchTerm)) return users;

        var lowerCaseTerm = parameters.SearchTerm.Trim().ToLower();

        return users.Where(x => 
            x.Email.ToLower().Contains(lowerCaseTerm) 
            || x.FirstName.ToLower().Contains(lowerCaseTerm)
            || x.LastName.ToLower().Contains(lowerCaseTerm));
    }

    
    public static IQueryable<User> Sort(
        this IQueryable<User> users, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString)) return users;

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<User>(orderByQueryString);

        return string.IsNullOrWhiteSpace(orderQuery) ? users.OrderBy(e => e.Email) : users.OrderBy(orderQuery);
    }
}