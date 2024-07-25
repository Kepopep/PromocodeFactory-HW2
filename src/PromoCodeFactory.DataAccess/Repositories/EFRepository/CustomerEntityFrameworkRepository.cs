using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Domain.PromoCodeManager;
using PromoCodeFactory.EntityFramework;

namespace PromoCodeFactory.DataAccess.Repositories.EFRepository;

public class CustomerEntityFrameworkRepository : EntityFrameworkRepository<Customer>
{
    public CustomerEntityFrameworkRepository(DatabaseContext context) : base(context)
    {
        
    }

    public override Task<IEnumerable<Customer>> GetAllAsync()
    {
        return Task.FromResult(DbSet.AsEnumerable());
    }

    public override Task<Customer> GetByIdAsync(Guid id)
    {
        return Task.FromResult(DbSet.FirstOrDefault(x => x.Id == id) ?? 
                               throw new NullReferenceException($"No element with {id}"));
    }

    public override async Task AddAsync(Customer data)
    {
        await DbSet.AddAsync(data);
        await Context.SaveChangesAsync();
    }

    public override async Task RemoveAsync(Guid id)
    {
        if (!await DbSet.AnyAsync(x => x.Id == id))
        {
            throw new NullReferenceException($"No element with {id}");
        }

        var promoCode = DbSet.Where(x => x.Id == id);

        Context.Set<PromoCode>().RemoveRange(promoCode.First().PromoCodes);
        
        await DbSet.Select(x => x.Id == id).ExecuteDeleteAsync();

        await Context.SaveChangesAsync();
    }

    public override async Task<Customer> UpdateAsync(Guid id, Customer data)
    {
        var existingCustomer = await DbSet.FindAsync(id);
        
        if (existingCustomer is null)
        {
            throw new NullReferenceException($"No element with {id}");
        }

        existingCustomer.FirstName = data.FirstName;
        existingCustomer.LastName = data.LastName;
        existingCustomer.Email = data.Email;
        existingCustomer.Preferences = data.Preferences;
        existingCustomer.PromoCodes = data.PromoCodes;
        await Context.SaveChangesAsync();

        return existingCustomer;
    }
}