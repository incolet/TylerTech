using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TylerTechnologies.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EmployeeDbContext>
{
    public EmployeeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EmployeeDbContext>();
        optionsBuilder.UseSqlServer("DefaultConnection",
            x => x.MigrationsAssembly(typeof(EmployeeDbContext).Assembly.FullName));

        return new EmployeeDbContext(optionsBuilder.Options);
    }
}