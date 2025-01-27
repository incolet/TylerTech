namespace TylerTechnologies.Core.Entities;

public class EmployeeRole
{
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}