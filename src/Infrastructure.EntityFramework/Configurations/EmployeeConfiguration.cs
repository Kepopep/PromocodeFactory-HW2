using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromoCodeFactory.Core.Domain.Administration;

namespace PromoCodeFactory.EntityFramework.Configurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("EmployeeId");
        
        builder.Property(e => e.FirstName).HasMaxLength(15);
        builder.Property(e => e.LastName).HasMaxLength(15);
        builder.Property(e => e.Email).HasMaxLength(30);
        
        builder.HasMany(e => e.Roles)
            .WithMany(r => r.Employees)
            .UsingEntity(
                "EmployeeRole",
                e => e.HasOne(typeof(Role))
                    .WithMany()
                    .HasForeignKey("RolesId")
                    .HasPrincipalKey(nameof(Role.Id))
                    .IsRequired(),
                r => r.HasOne(typeof(Employee))
                    .WithMany()
                    .HasForeignKey("EmployeesId")
                    .HasPrincipalKey(nameof(Employee.Id))
                    .IsRequired(),
            j => j.HasKey("EmployeesId", "RolesId")
                );
    }
}