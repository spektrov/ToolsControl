namespace ToolsControl.BLL.Models;

public class WorkerModel
{    
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public UserModel? User { get; set; }
    
    public Guid TitleId { get; set; }
    public JobTitleModel? Title { get; set; }
    
    public DateTime HiringDate { get; set; }
    
    public decimal SalaryValue { get; set; }
    
    public string CardNumber { get; set; } = string.Empty;

    public virtual ICollection<AllowedAccessModel>? AllowedAccesses { get; set; }
    
    public virtual ICollection<UsageModel>? Usages { get; set; }
}