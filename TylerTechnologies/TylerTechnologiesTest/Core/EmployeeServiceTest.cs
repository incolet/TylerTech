using System.Linq.Expressions;
using Moq;
using TylerTechnologies.Core.Abstracts.Repositories;
using TylerTechnologies.Core.Abstracts.Services;
using TylerTechnologies.Core.DTOs;
using TylerTechnologies.Core.Entities;
using TylerTechnologies.Core.Services;
using TylerTechnologies.Infrastructure.Data;

namespace TylerTechnologiesTest.Core;

public class EmployeeServiceTest
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly Mock<IRoleService> _roleServiceMock;
    private readonly IEmployeeService _employeeService;

    public EmployeeServiceTest()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();

        _roleServiceMock = new Mock<IRoleService>();
        _employeeService = new EmployeeService(
            _employeeRepositoryMock.Object,
            _roleServiceMock.Object
        );
    }

    [Fact]
    public async Task AddEmployeeAsync_WhenEmployeeIsAdded_ShouldReturnSuccessResult()
    {
        // Arrange
        var roleIds = new List<int> { 3 };
        var roles = new List<Role>
        {
            new Role { Id = 3}
        };

        var manager = new Employee()
        {
            EmployeeNumber = "JD12345",
            FirstName = "John",
            LastName = "Doe",
            ManagerId = Guid.NewGuid(),
            Roles = new List<EmployeeRole>
            {
                new EmployeeRole { RoleId = 1 }
            }
        };
        
        var validDto = new AddEmployeeDto
        {
            EmployeeNumber = "AB12345",
            FirstName = "John",
            LastName = "Terry",
            ManagerId = manager.ManagerId,
            Roles = roleIds
        };
        
        // Mock role lookup
        _roleServiceMock.Setup(x => x.GetRolesByIdsAsync(roleIds))
            .ReturnsAsync(roles);

        // Mock employee does not exist and manager exists
        _employeeRepositoryMock.SetupSequence(x => x.Find(
                It.IsAny<Expression<Func<Employee, Employee>>>(),
                It.IsAny<Expression<Func<Employee, bool>>>()))
            .ReturnsAsync(new List<Employee>())
            .ReturnsAsync(new List<Employee> { manager });
        
        // Act
        var result = await _employeeService.AddEmployeeAsync(validDto);

        // Assert
        Assert.True(result.IsSuccess);
        _employeeRepositoryMock.Verify(x => 
            x.AddEmployeeAsync(It.Is<Employee>(e => 
                e.EmployeeNumber == validDto.EmployeeNumber &&
                e.FirstName == validDto.FirstName &&
                e.LastName == validDto.LastName &&
                e.ManagerId == validDto.ManagerId &&
                e.Roles.Count == roles.Count
            )),
            Times.Once
        );
    }
    
    [Fact]
    public async Task AddEmployeeAsync_WhenRoleIsInvalid_ShouldReturnFailureResult()
    {
        // Arrange
        var roleIds = new List<int> { 1,4 };
        var roles = new List<Role>
        {
            new Role { Id = 1, RoleName = Roles.Director }
        };

        var invalidDto = new AddEmployeeDto
        {
            EmployeeNumber = "AB12345",
            FirstName = "John",
            LastName = "Terry",
            ManagerId = Guid.NewGuid(),
            Roles = roleIds
        };

        // Mock role lookup
        _roleServiceMock.Setup(x => x.GetRolesByIdsAsync(roleIds))
            .ReturnsAsync(roles);

        // Act
        var result = await _employeeService.AddEmployeeAsync(invalidDto);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Invalid Role Ids provided", result.Error);
        _employeeRepositoryMock.Verify(x => 
            x.AddEmployeeAsync(It.IsAny<Employee>()),
            Times.Never
        );
    }
    
    [Fact]
    public async Task AddEmployeeAsync_WhenManagerIdIsInvalid_ShouldReturnFailureResult()
    {
        // Arrange
        var roleIds = new List<int> { 1 };
        var roles = new List<Role>
        {
            new Role { Id = 1, RoleName = Roles.Director}
        };

        var invalidDto = new AddEmployeeDto
        {
            EmployeeNumber = "AB12345",
            FirstName = "John",
            LastName = "Terry",
            ManagerId = Guid.NewGuid(),
            Roles = roleIds
        };

        // Mock role lookup
        _roleServiceMock.Setup(x => x.GetRolesByIdsAsync(roleIds))
            .ReturnsAsync(roles);
        
        // Mock employee does not exist
        _employeeRepositoryMock.Setup(x => x.Find(It.IsAny<Expression<Func<Employee, Employee>>>(), 
                It.IsAny<Expression<Func<Employee, bool>>>()))
            .ReturnsAsync(new List<Employee>());
        
        // Act
        var result = await _employeeService.AddEmployeeAsync(invalidDto);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Invalid Manager Id provided", result.Error);
        _employeeRepositoryMock.Verify(x => 
                x.AddEmployeeAsync(It.IsAny<Employee>()),
            Times.Never
        );
    }
    
    [Fact]
    public async Task AddEmployeeAsync_WhenDuplicateEmployeeNumber_ShouldReturnFailureResult()
    {
        // Arrange
        var roleIds = new List<int> { 2 };
        var roles = new List<Role>
        {
            new Role { Id = 2, RoleName = "Employee" }
        };

        var validDto = new AddEmployeeDto
        {
            EmployeeNumber = "AB12345",
            FirstName = "John",
            LastName = "Terry",
            ManagerId = Guid.NewGuid(),
            Roles = roleIds
        };

        // Mock role lookup
        _roleServiceMock.Setup(x => x.GetRolesByIdsAsync(roleIds))
            .ReturnsAsync(roles);

        // Mock duplicate employee number
        _employeeRepositoryMock.Setup(x => x.Find(It.IsAny<Expression<Func<Employee, Employee>>>(), 
                It.IsAny<Expression<Func<Employee, bool>>>()))
            .ReturnsAsync(new List<Employee> { new Employee() });

        // Act
        var result = await _employeeService.AddEmployeeAsync(validDto);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Employee Number already exists", result.Error);
        _employeeRepositoryMock.Verify(x => 
            x.AddEmployeeAsync(It.IsAny<Employee>()),
            Times.Never
        );
    }
    
    [Fact]
    public async Task GetEmployeesAsync_WhenCalled_ShouldReturnEmployees()
    {
        // Arrange
        var employeeQueryDto = new EmployeeQueryDto
        {
            ManagerId = null
        };

        var employees = new List<Employee>
        {
            new Employee
            {
                EmployeeNumber = "AB12345",
                FirstName = "John",
                LastName = "Terry",
                ManagerId = Guid.NewGuid(),
                Roles = new List<EmployeeRole>
                {
                    new EmployeeRole { RoleId = 2 }
                }
            }
        };
        
        var employeeDto = employees.Select(e => new GetEmployeeDto
        {
            EmployeeNumber = e.EmployeeNumber,
            FirstName = e.FirstName,
            LastName = e.LastName,
        }).ToList();

        // Mock employee repository
        _employeeRepositoryMock.Setup(x => x.Find(It.IsAny<Expression<Func<Employee, GetEmployeeDto>>>(), 
                It.IsAny<Expression<Func<Employee, bool>>>()))
            .ReturnsAsync(employeeDto);

        // Act
        var result = await _employeeService.GetEmployeesAsync(employeeQueryDto);

        // Assert
        Assert.Single(result);
        Assert.Equal(employees[0].EmployeeNumber, result.First().EmployeeNumber);
        Assert.Equal(employees[0].FirstName, result.First().FirstName);
        Assert.Equal(employees[0].LastName, result.First().LastName);
    }
    
}

