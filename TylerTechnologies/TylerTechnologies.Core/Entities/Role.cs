namespace TylerTechnologies.Core.Entities;

public class Role
{
    public Role()
    {
    }
    public int Id { get; set; }
    public string RoleName { get; set; } = default!;
    public ICollection<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();
}