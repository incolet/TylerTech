using TylerTechnologies.Infrastructure.Data;

namespace TylerTechnologies.Api;

public static class DataSeeder
{
    internal static void EnsureRolesSeed(EmployeeDbContext dbContext)
    {
        if (dbContext.Roles.Any())
        {
            return;
        }

        dbContext.AddRangeAsync(RoleSeed.GetRoles());
        dbContext.SaveChanges();
    }
    
    internal static void EnsureEmployeeSeed(EmployeeDbContext dbContext)
    {
        if (dbContext.Employees.Any())
        {
            return;
        }
        
        dbContext.AddRangeAsync(EmployeeSeed.GetEmployees(dbContext));

        dbContext.SaveChanges();
    }
}