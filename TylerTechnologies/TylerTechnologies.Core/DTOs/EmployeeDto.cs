using System.Linq.Expressions;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Core.DTOs;

public class GetEmployeeDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string EmployeeNumber { get; set; } = null!;

    public static Expression<Func<Employee, GetEmployeeDto>> Map() =>
        e => new GetEmployeeDto
        {
            Id = e.Id,
            LastName = e.LastName,
            FirstName = e.FirstName,
            EmployeeNumber = e.EmployeeNumber,
        };
}