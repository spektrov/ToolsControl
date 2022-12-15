namespace ToolsControl.BLL.Models.Responses;

public class WorkersWorkTimeResponse
{
    public string FullName { get; set; }

    public DateTime HiringDate { get; set; }
    
    public decimal SalaryValue { get; set; }
    
    public string Title { get; set; }
    
    public int Time { get; set; }
}