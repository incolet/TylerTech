using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Infrastructure.Data.Configurations;

public class EmployeeRoleConfiguration : IEntityTypeConfiguration<EmployeeRole>
{
    public void Configure(EntityTypeBuilder<EmployeeRole> builder)
    {
        
        builder
            .HasKey(er => new { er.EmployeeId, er.RoleId });

        // Configure relationships
        builder.HasOne(er => er.Employee)
            .WithMany(e => e.Roles)
            .HasForeignKey(er => er.EmployeeId);

        builder.HasOne(er => er.Role)
            .WithMany(r => r.EmployeeRoles)
            .HasForeignKey(er => er.RoleId);

    }
}