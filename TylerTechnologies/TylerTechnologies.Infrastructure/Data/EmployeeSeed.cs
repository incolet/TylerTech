using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Infrastructure.Data;

public static class EmployeeSeed
{
    
    public static List<Employee> GetEmployees(EmployeeDbContext context)
    {
        var employees = new List<Employee>();
        var roles = context.Roles.ToList();
        
        var jeffery = new Employee
        {
            Id = Guid.NewGuid(),
            EmployeeNumber = "JW12345",
            FirstName = "Jeffery",
            LastName = "Wells",
            Roles = new List<EmployeeRole>()
            {
                new EmployeeRole
                {
                    Role = roles.First(r => r.RoleName == Roles.Director)
                }
            }
        };
        
        var victor = new Employee
        {
            Id = Guid.NewGuid(),
            EmployeeNumber = "VA12345",
            FirstName = "Victor",
            LastName = "Atkins",
            ManagerId = jeffery.Id,
            Roles = new List<EmployeeRole>()
            {
                new EmployeeRole
                {
                    Role = roles.First(r => r.RoleName == Roles.Director)
                }
            }
        };

        var kelli = new Employee
        {
            Id = Guid.NewGuid(),
            EmployeeNumber = "KH12345",
            FirstName = "Kelli",
            LastName = "Hamilton",
            ManagerId = jeffery.Id,
            Roles = new List<EmployeeRole>()
            {
                new EmployeeRole
                {
                    Role = roles.First(r => r.RoleName == Roles.Director)
                }
            }
        };

        var adam = new Employee
        {
            EmployeeNumber = "AB12345",
            FirstName = "Adam",
            LastName = "Braun",
            ManagerId = victor.Id,
            Roles = new List<EmployeeRole>()
            {
                new()
                {
                    Role = roles.First(r => r.RoleName == Roles.It)
                },
                new()
                {
                    Role = roles.First(r => r.RoleName == Roles.Support)
                }
            }
        };

        var brian = new Employee
        {
            EmployeeNumber = "BC12345",
            FirstName = "Brian",
            LastName = "Cruz",
            ManagerId = victor.Id,
            Roles = new List<EmployeeRole>
            {
                new()
                {
                    Role = roles.First(r => r.RoleName == Roles.Accounting)
                }
            }
        };

        var kristen = new Employee
        {
            EmployeeNumber = "KF12345",
            FirstName = "Kristen",
            LastName = "Floyd",
            ManagerId = victor.Id,
            Roles = new List<EmployeeRole>
            {
                new()
                {
                    Role = roles.First(r => r.RoleName == Roles.Analyst)
                }, new ()
                {
                    Role = roles.First(r => r.RoleName == Roles.Sales)
                }
            }
        };
        
        var lois = new Employee
        {
            EmployeeNumber = "LM12345",
            FirstName = "Lois",
            LastName = "Martinez",
            ManagerId = kelli.Id,
            Roles = new List<EmployeeRole>
            {
                new()
                {
                    Role = roles.First(r => r.RoleName == Roles.Support)
                }
            }
        };
        
        var micheal = new Employee
        {
            EmployeeNumber = "ML12345",
            FirstName = "Micheal",
            LastName = "Lind",
            ManagerId = kelli.Id,
            Roles = new List<EmployeeRole>
            {
                new()
                {
                    Role = roles.First(r => r.RoleName == Roles.Analyst)
                }
            }
        };

        var eric = new Employee
        {
            EmployeeNumber = "EB12345",
            FirstName = "Eric",
            LastName = "Bay",
            ManagerId = kelli.Id,
            Roles = new List<EmployeeRole>
            {
                new()
                {
                    Role = roles.First(r => r.RoleName == Roles.It)
                },
                new()
                {
                    Role = roles.First(r => r.RoleName == Roles.Sales)
                }
            }
        };

        var brandon = new Employee
        {
            EmployeeNumber = "BY12345",
            FirstName = "Brandon",
            LastName = "Young",
            ManagerId = kelli.Id,
            Roles = new List<EmployeeRole>
            {
                new()
                {
                    Role = roles.First(r => r.RoleName == Roles.Accounting)
                }
            }
        };
        
        employees.Add(jeffery);
        employees.Add(victor);
        employees.Add(kelli);
        employees.Add(adam);
        employees.Add(brian);
        employees.Add(kristen);
        employees.Add(lois);
        employees.Add(micheal);
        employees.Add(eric);
        employees.Add(brandon);
        
        return employees;
    }
}