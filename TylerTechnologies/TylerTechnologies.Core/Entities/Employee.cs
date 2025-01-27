namespace TylerTechnologies.Core.Entities;

public class Employee
{
    public Employee()
    {
    }
    
    public Guid Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string EmployeeNumber { get; set; } = null!;
    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
    
    public Guid? ManagerId { get; set; }
    public Employee? Manager { get; set; }
    public ICollection<EmployeeRole> Roles { get; set; } = new List<EmployeeRole>();
}