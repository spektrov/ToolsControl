namespace ToolsControl.DAL.Entities;

public class Worker : BaseEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public Guid TitleId { get; set; }
    public JobTitle? Title { get; set; }
    
    public DateTime HiringDate { get; set; }
    
    public decimal SalaryValue { get; set; }

    public virtual ICollection<AllowedAccess>? AllowedAccesses { get; set; }
    
    public virtual ICollection<Usage>? Usages { get; set; }
}