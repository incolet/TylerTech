using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TylerTechnologies.Core.Abstracts.Repositories;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Infrastructure.Data.Repositories;

internal sealed class RoleRepository(EmployeeDbContext dbContext) : IRoleRepository
{
    public async Task<IReadOnlyCollection<Role>> FindAsync(Expression<Func<Role, bool>>? predicate = null)
    {
        if (predicate == null)
        {
            return await dbContext.Roles.ToListAsync();
        }
        return await dbContext.Roles.Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyCollection<TResult>> GetAllAsync<TResult>(Expression<Func<Role, TResult>> selector, Expression<Func<Role, bool>>? predicate = null) 
        where TResult : class
    {
        IQueryable<Role> query = dbContext.Roles;

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return await query.Select(selector).ToListAsync();
    }
}