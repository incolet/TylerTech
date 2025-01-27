using TylerTechnologies.Core.Common;
using TylerTechnologies.Core.DTOs;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Core.Abstracts.Services;

/// <summary>
/// Interface for Employee Service
/// </summary>
public interface IEmployeeService
{
    /// <summary>
    /// Add Employee
    /// </summary>
    /// <param name="addEmployeeDto"></param>
    /// <returns>
    /// returns a task of Result of Employee
    /// </returns>
    public Task<Result<Employee>> AddEmployeeAsync(AddEmployeeDto addEmployeeDto);
    
    public Task<IReadOnlyCollection<GetEmployeeDto>> GetEmployeesAsync(EmployeeQueryDto employeeQueryDto);
    
}