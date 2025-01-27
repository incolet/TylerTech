using System.Linq.Expressions;
using Moq;
using TylerTechnologies.Core.Abstracts.Repositories;
using TylerTechnologies.Core.Abstracts.Services;
using TylerTechnologies.Core.DTOs;
using TylerTechnologies.Core.Entities;
using TylerTechnologies.Core.Services;

namespace TylerTechnologiesTest.Core;

public class RoleServiceTest
{

    [Fact]
    public async Task GetAllAsync_WhenCalled_ShouldReturnAllRoles()
    {
        // Arrange
        var roles = new List<Role>
        {
            new Role { Id = 1, RoleName = "Admin" },
            new Role { Id = 2, RoleName = "Manager" },
            new Role { Id = 3, RoleName = "Employee" }
        };

        Mock<IRoleRepository> roleRepositoryMock = new Mock<IRoleRepository>();
        roleRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Role, RolesDto>>>(), It.IsAny<Expression<Func<Role, bool>>>()))
            .ReturnsAsync(() => roles.Select(r => new RolesDto { Id = r.Id, Name = r.RoleName }).ToList());

        IRoleService roleService = new RoleService(roleRepositoryMock.Object);

        // Act
        var result = await roleService.GetAllAsync();

        // Assert
        Assert.Equal(3, result.Count);
    }
    
    [Fact]
    public async Task GetAllAsync_WhenCalled_ShouldReturnAllRolesExceptAdmin()
    {
        // Arrange
        var roles = new List<Role>
        {
            new Role { Id = 1, RoleName = "Admin" },
            new Role { Id = 2, RoleName = "Manager" },
            new Role { Id = 3, RoleName = "Employee" }
        };

        Mock<IRoleRepository> roleRepositoryMock = new Mock<IRoleRepository>();
        roleRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Role, RolesDto>>>(), It.IsAny<Expression<Func<Role, bool>>>()))
            .ReturnsAsync(() => roles.Where(r => r.RoleName != "Admin").Select(r => new RolesDto { Id = r.Id, Name = r.RoleName }).ToList());

        IRoleService roleService = new RoleService(roleRepositoryMock.Object);

        // Act
        var result = await roleService.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public async Task GetRolesByIdsAsync_WhenCalled_ShouldReturnRolesByIds()
    {
        // Arrange
        var roleIds = new List<int> { 2, 3 };
        var roles = new List<Role>
        {
            new Role { Id = 2, RoleName = "Manager" },
            new Role { Id = 3, RoleName = "Employee" }
        };

        Mock<IRoleRepository> roleRepositoryMock = new Mock<IRoleRepository>();
        roleRepositoryMock.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Role, bool>>>()))
            .ReturnsAsync(() => roles.Where(r => roleIds.Contains(r.Id)).ToList());

        IRoleService roleService = new RoleService(roleRepositoryMock.Object);

        // Act
        var result = await roleService.GetRolesByIdsAsync(roleIds);

        // Assert
        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public async Task GetRolesByIdsAsync_WhenCalledWithEmptyIds_ShouldReturnEmptyList()
    {
        // Arrange
        var roleIds = new List<int>();
        var roles = new List<Role>
        {
            new Role { Id = 2, RoleName = "Manager" },
            new Role { Id = 3, RoleName = "Employee" }
        };

        Mock<IRoleRepository> roleRepositoryMock = new Mock<IRoleRepository>();
        roleRepositoryMock.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Role, bool>>>()))
            .ReturnsAsync(() => roles.Where(r => roleIds.Contains(r.Id)).ToList());

        IRoleService roleService = new RoleService(roleRepositoryMock.Object);

        // Act
        var result = await roleService.GetRolesByIdsAsync(roleIds);

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task GetRolesByIdsAsync_WhenCalledWithNonExistingIds_ShouldReturnEmptyList()
    {
        // Arrange
        var roleIds = new List<int> { 4, 5 };
        var roles = new List<Role>
        {
            new Role { Id = 2, RoleName = "Manager" },
            new Role { Id = 3, RoleName = "Employee" }
        };

        Mock<IRoleRepository> roleRepositoryMock = new Mock<IRoleRepository>();
        roleRepositoryMock.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Role, bool>>>()))
            .ReturnsAsync(() => roles.Where(r => roleIds.Contains(r.Id)).ToList());

        IRoleService roleService = new RoleService(roleRepositoryMock.Object);

        // Act
        var result = await roleService.GetRolesByIdsAsync(roleIds);

        // Assert
        Assert.Empty(result);
    }
    
    
}