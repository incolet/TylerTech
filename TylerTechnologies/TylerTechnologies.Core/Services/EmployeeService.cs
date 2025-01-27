using TylerTechnologies.Core.Abstracts.Repositories;
using TylerTechnologies.Core.Abstracts.Services;
using TylerTechnologies.Core.Common;
using TylerTechnologies.Core.DTOs;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Core.Services;

/// <summary>
/// Handles the business logic for Employee related operations
/// </summary>
/// <param name="repository"></param>
/// <param name="roleService"></param>
public sealed class EmployeeService(IEmployeeRepository repository, IRoleService roleService)
    : IEmployeeService
{
  
  /// <summary>
  /// Adds a new employee to the database
  /// </summary>
  /// <param name="addEmployeeDto"></param>
  /// <returns>
  /// A Result object containing the newly added Employee object if successful, otherwise a failure message
  /// </returns>
  public async Task<Result<Employee>> AddEmployeeAsync(AddEmployeeDto addEmployeeDto)
  {
      var validRoles = await roleService.GetRolesByIdsAsync(addEmployeeDto.Roles);

      if (validRoles.Count != addEmployeeDto.Roles.Count)
      {
          return Result<Employee>.Failure("Invalid Role Ids provided");
      }
  
      var employeeNumberExists = await repository
        .Find(predicate: x => x.EmployeeNumber == addEmployeeDto.EmployeeNumber, selector: x => x);
    
        if (employeeNumberExists.Count > 0)
        {
            return Result<Employee>.Failure("Employee Number already exists");
        }
        
        var manager = await repository
            .Find(predicate: x => x.Id == addEmployeeDto.ManagerId, selector: x => x);
        
        if (manager.Count == 0)
        {
            return Result<Employee>.Failure("Invalid Manager Id provided");
        }
        
      var employee = new Employee
      {
          LastName = addEmployeeDto.LastName,
          FirstName = addEmployeeDto.FirstName,
          ManagerId = addEmployeeDto.ManagerId,
          EmployeeNumber = addEmployeeDto.EmployeeNumber,
      };

      employee.Roles = validRoles.Select(r => new EmployeeRole { RoleId = r.Id, EmployeeId = employee.Id }).ToList();
      
      await repository.AddEmployeeAsync(employee);

      return Result<Employee>.Success(employee);
  }
  
  /// <summary>
  /// Handles the logic for retrieving employees from the database based on the provided query
  /// </summary>
  /// <param name="employeeQueryDto"></param>
  /// <returns></returns>
  public async Task<IReadOnlyCollection<GetEmployeeDto>> GetEmployeesAsync(EmployeeQueryDto employeeQueryDto) 
      => await repository.Find(predicate: EmployeeQueryDto.ToPredicate(employeeQueryDto), selector: GetEmployeeDto.Map());
  
}
 