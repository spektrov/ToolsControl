namespace ToolsControl.DAL.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}