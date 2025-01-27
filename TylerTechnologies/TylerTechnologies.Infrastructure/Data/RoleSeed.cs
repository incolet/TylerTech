using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Infrastructure.Data;

public static class RoleSeed
{
    public static List<Role> GetRoles()
    {
        var roles = new List<Role>
        {
            new Role
            {
                RoleName = Roles.Director
            },
            new Role
            {
                RoleName = Roles.It
            },
            new Role
            {
                RoleName = Roles.Support
            },
            new Role
            {
                RoleName = Roles.Accounting
            },
            new Role
            {
                RoleName = Roles.Analyst
            },
            new Role
            {
                RoleName = Roles.Sales
            }
        };

        return roles;
    }
}