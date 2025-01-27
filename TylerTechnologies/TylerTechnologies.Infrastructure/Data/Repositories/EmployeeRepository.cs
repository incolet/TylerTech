using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TylerTechnologies.Core.Abstracts.Repositories;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Infrastructure.Data.Repositories;

public class EmployeeRepository(EmployeeDbContext dbContext) : IEmployeeRepository
{
    public async Task AddEmployeeAsync(Employee employee)
    {
        await dbContext.Employees.AddAsync(employee);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<TResult>> Find<TResult>(Expression<Func<Employee, TResult>> selector, Expression<Func<Employee, bool>>? predicate = null) where TResult : class
    {
        IQueryable<Employee> query = dbContext.Employees;

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return await query.Select(selector:selector).ToListAsync();
    }
}