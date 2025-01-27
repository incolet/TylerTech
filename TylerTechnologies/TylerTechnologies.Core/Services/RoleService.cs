using TylerTechnologies.Core.Abstracts.Repositories;
using TylerTechnologies.Core.Abstracts.Services;
using TylerTechnologies.Core.DTOs;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Core.Services;

/// <summary>
/// Service for managing roles.
/// </summary>
/// <param name="roleRepository"></param>
public class RoleService(IRoleRepository roleRepository) : IRoleService
{
    /// <summary>
    /// Get all roles.
    /// </summary>
    /// <returns>
    /// A read only collection of RolesDto objects representing all roles in the database.
    /// </returns>
    public async Task<IReadOnlyCollection<RolesDto>> GetAllAsync()
    {
        return await roleRepository.GetAllAsync(selector: RolesDto.Map());
    }

    /// <summary>
    /// Get roles by Ids.
    /// </summary>
    /// <param name="roleIds"></param>
    /// <returns>
    /// A read only collection of Role objects representing the roles with the specified Ids.
    /// </returns>
    public async Task<IReadOnlyCollection<Role>> GetRolesByIdsAsync(IReadOnlyCollection<int> roleIds)
        => await roleRepository.FindAsync(x => roleIds.Contains(x.Id));
}