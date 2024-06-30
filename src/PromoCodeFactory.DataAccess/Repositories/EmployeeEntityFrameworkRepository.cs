using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.EntityFramework;

namespace PromoCodeFactory.DataAccess.Repositories;

public class EmployeeEntityFrameworkRepository : IRepository<Employee>
{
    private readonly DatabaseContext _context;
    
    public EmployeeEntityFrameworkRepository(DatabaseContext context)
    {
        _context = context;

    }
    
    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public Task<Employee> GetByIdAsync(Guid id)
    {
        return null;
    }

    public async Task AddAsync(Employee data)
    {
        await _context.Employees.AddAsync(data);
        
        await _context.SaveChangesAsync();
    }

    public Task RemoveAsync(Guid id)
    {
        return null;
    }

    public Task<Employee> UpdateAsync(Guid id, Employee data)
    {
        return null;
    }
}