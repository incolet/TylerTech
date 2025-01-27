using System.Linq.Expressions;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Core.DTOs;

/// <summary>
/// This class is used to query employees
/// </summary>
public class EmployeeQueryDto
{
    public Guid? ManagerId { get; set; }
    
    /// <summary>
    /// Static method to convert EmployeeQueryDto to a predicate
    /// </summary>
    /// <param name="employeeQueryDto"></param>
    /// <returns></returns>
    public static Expression<Func<Employee, bool>> ToPredicate(EmployeeQueryDto employeeQueryDto)
    {
        return employee => (employeeQueryDto.ManagerId == null || employee.ManagerId == employeeQueryDto.ManagerId);
    }
}