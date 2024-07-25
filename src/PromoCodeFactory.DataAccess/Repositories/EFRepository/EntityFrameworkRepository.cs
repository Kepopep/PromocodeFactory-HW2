using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
using PromoCodeFactory.EntityFramework;

namespace PromoCodeFactory.DataAccess.Repositories.EFRepository;

public abstract class EntityFrameworkRepository<T> : IRepository<T> where T: BaseEntity
{
    protected readonly DbSet<T> DbSet;
    protected readonly DatabaseContext Context;

    protected EntityFrameworkRepository(DatabaseContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public abstract Task<IEnumerable<T>> GetAllAsync();

    public abstract Task<T> GetByIdAsync(Guid id);

    public abstract Task AddAsync(T data);

    public abstract Task RemoveAsync(Guid id);

    public abstract Task<T> UpdateAsync(Guid id, T data);
}