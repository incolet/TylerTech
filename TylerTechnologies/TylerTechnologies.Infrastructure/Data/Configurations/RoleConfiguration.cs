using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TylerTechnologies.Core.Entities;

namespace TylerTechnologies.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.RoleName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasIndex(x => x.RoleName).IsUnique();
    }
}