
using System.Linq.Expressions;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Core.Abstracts.Repositories;

/// <summary>
/// The role repository interface
/// </summary>
public interface IRoleRepository
{
    /// <summary>
    /// Finds roles in the database based on the provided predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns>
    /// returns a task of a read-only collection of roles
    /// </returns>
    public Task<IReadOnlyCollection<Role>> FindAsync(Expression<Func<Role, bool>>? predicate = null);

    /// <summary>
    /// Gets all roles in the database based on the provided selector and predicate
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns>
    /// returns a task of a read-only collection of TResult
    /// </returns>
    public Task<IReadOnlyCollection<TResult>> GetAllAsync<TResult>(Expression<Func<Role, TResult>> selector, Expression<Func<Role, bool>>? predicate = null)
        where TResult : class;
}