using System.Linq.Expressions;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Core.DTOs;

public class GetManagerDto
{
    public Guid Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    
    public static Expression<Func<Employee, GetManagerDto>> Map() =>
        e => new GetManagerDto()
        {
            Id = e.Id,
            LastName = e.LastName,
            FirstName = e.FirstName,
        };
}
