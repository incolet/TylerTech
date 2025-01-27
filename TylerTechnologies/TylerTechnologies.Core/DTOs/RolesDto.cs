using System.Linq.Expressions;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Core.DTOs;

public class RolesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    
    public static Expression<Func<Role, RolesDto>> Map() =>
        r => new RolesDto()
        {
            Id = r.Id,
            Name = r.RoleName
        };
}