namespace ToolsControl.BLL.Models.RequestFeatures;

public class PagedList<T> : List<T>
{
    public Data MetaData { get; set; }

    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        MetaData = new Data
        {
            TotalCount = count, 
            PageSize = pageSize, 
            CurrentPage = pageNumber, 
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        }; 
        
        AddRange(items);
    } 
    
    public static PagedList<T> ToPagedList(ICollection<T> source, int pageNumber, int pageSize) { 
        var count = source.Count;
        var items = source
            .Skip((pageNumber - 1) * pageSize) 
            .Take(pageSize)
            .ToList();
        
        return new PagedList<T>(items, count, pageNumber, pageSize); 
    }
    
    
    public struct Data
    {
        public int CurrentPage { get; set; } 
        public int TotalPages { get; set; } 
        public int PageSize { get; set; } 
        public int TotalCount { get; set; } 
        public bool HasPrevious => CurrentPage > 1; 
        public bool HasNext => CurrentPage < TotalPages;
    }
}