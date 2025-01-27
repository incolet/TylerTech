using TylerTechnologies.Core.DTOs;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Core.Abstracts.Services;

/// <summary>
/// Interface for Role Service
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Get all roles
    /// </summary>
    /// <returns>
    /// returns a task of IReadOnlyCollection of RolesDto
    /// </returns>
    public Task<IReadOnlyCollection<RolesDto>> GetAllAsync();
    
    /// <summary>
    /// Get roles by ids
    /// </summary>
    /// <param name="roleIds"></param>
    /// <returns>
    /// returns a task of IReadOnlyCollection of Role
    /// </returns>
    public Task<IReadOnlyCollection<Role>> GetRolesByIdsAsync(IReadOnlyCollection<int> roleIds);
}