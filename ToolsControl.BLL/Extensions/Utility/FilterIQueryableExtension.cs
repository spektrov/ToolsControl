using System.Linq.Expressions;

namespace ToolsControl.BLL.Extensions.Utility;

public static class FilterIQueryableExtension
{
    public static IQueryable<TSource> WhereIf<TSource>(
        this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
    {
        if (condition)
            return source.Where(predicate);

        return source;
    }
}