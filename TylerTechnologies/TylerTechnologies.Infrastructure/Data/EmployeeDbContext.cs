using Microsoft.EntityFrameworkCore;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Infrastructure.Data;

public class EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : DbContext(options)
{
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
