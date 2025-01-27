namespace TylerTechnologies.Core.DTOs;

public class AddEmployeeDto
{
    public Guid? ManagerId { get; set; }
    public string LastName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string EmployeeNumber { get; set; } = default!;
    public IReadOnlyCollection<int> Roles { get; set; } = new List<int>();
}