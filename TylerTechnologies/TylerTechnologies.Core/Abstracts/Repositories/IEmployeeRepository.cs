using System.Linq.Expressions;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Core.Abstracts.Repositories;

/// <summary>
/// The employee repository interface
/// </summary>
public interface IEmployeeRepository
{
    /// <summary>
    /// Adds an employee to the database
    /// </summary>
    /// <param name="employee"></param>
    /// <returns>
    /// A task representing the asynchronous operation
    /// </returns>
    Task AddEmployeeAsync(Employee employee);
    
    /// <summary>
    /// Finds employees in the database based on the provided selector and predicate
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns>
    /// returns a task of a read-only collection of TResult 
    /// </returns>
    public Task<IReadOnlyCollection<TResult>> Find<TResult>(Expression<Func<Employee, TResult>> selector, Expression<Func<Employee, bool>>? predicate = null) 
        where TResult : class;

}