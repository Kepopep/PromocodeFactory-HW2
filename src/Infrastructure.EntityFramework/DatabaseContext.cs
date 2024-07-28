using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManager;
using PromoCodeFactory.EntityFramework.Configurations;

namespace PromoCodeFactory.EntityFramework;

public class DatabaseContext : DbContext
{
    #region DbSet

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Customer> Customers { get; set; }
    
    public DbSet<Preference> Preferences { get; set; }
    
    public DbSet<Role> Roles { get; set; }
    
    public DbSet<PromoCode> PromoCodes { get; set; }

    #endregion
    
    public DatabaseContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfiguration).Assembly);

        modelBuilder.Entity<Preference>()
            .HasKey(p => p.Id)
            .HasName("PreferenceId");
        
        modelBuilder.Entity<Role>()
            .HasKey(r => r.Id)
            .HasName("RoleId");
        
        modelBuilder.Entity<PromoCode>()
            .HasKey(p => p.Id)
            .HasName("PromoCodeId");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }
}