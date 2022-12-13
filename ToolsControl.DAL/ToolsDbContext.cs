using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToolsControl.DAL.Entities;

namespace ToolsControl.DAL;

public class ToolsDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ToolsDbContext(DbContextOptions options) : base(options) { } 
    
    
    public DbSet<AllowedAccess> AllowedAccesses { get; set; }
    
    public DbSet<Equipment> Equipments { get; set; }
    
    public DbSet<EquipmentType> EquipmentTypes { get; set; }
    
    public DbSet<JobTitle> JobTitles { get; set; }
    
    public DbSet<Usage> Usages { get; set; }
    
    public DbSet<Worker> Workers { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AllowedAccess>();

        builder.Entity<Equipment>();

        builder.Entity<EquipmentType>();

        builder.Entity<JobTitle>();

        builder.Entity<Usage>();

        builder.Entity<Worker>();

        builder.Entity<ApplicationRole>()
            .HasData(new List<ApplicationRole>
            {
                new () { 
                    Id = new Guid("D7D70154-A342-4BA3-95EC-D20079240B72"),
                    Name = "Worker", 
                },
                new()
                {
                    Id = new Guid("7510E33A-099F-48D9-8D01-F229DB155641"),
                    Name = "Administrator"
                }
            });
    }
}