using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Domain.Administration;

namespace PromoCodeFactory.EntityFramework;

public class DatabaseContext : DbContext
{
    public DbSet<Employee> Employees { get; } 
    
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
}