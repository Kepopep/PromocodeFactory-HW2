using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Domain.PromoCodeManager;
using PromoCodeFactory.EntityFramework;

namespace PromoCodeFactory.DataAccess.Repositories.EFRepository;

public class PreferenceEntityFrameworkRepository : EntityFrameworkRepository<Preference>
{
    public PreferenceEntityFrameworkRepository(DatabaseContext context) : base(context)
    {
    }

    public override Task<IEnumerable<Preference>> GetAllAsync()
    {
        return Task.FromResult(DbSet.AsEnumerable());
    }

    public override Task<Preference> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public override Task AddAsync(Preference data)
    {
        throw new NotImplementedException();
    }

    public override Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public override Task<Preference> UpdateAsync(Guid id, Preference data)
    {
        throw new NotImplementedException();
    }
}