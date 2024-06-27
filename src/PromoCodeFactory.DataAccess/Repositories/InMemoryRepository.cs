using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>: IRepository<T> where T: BaseEntity
    {
        protected List<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data.ToList();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data.AsEnumerable());
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id) ?? 
                         throw new NullReferenceException($"No element with {id}"));
        }

        public Task AddAsync(T data)
        {
            Data.Add(data);
            
            return Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid id)
        {
            var removeData = await GetByIdAsync(id);
            Data.Remove(removeData);
        }

        public async Task<T> UpdateAsync(Guid id, T data)
        {
            var savedData = await GetByIdAsync(id);

            Data[Data.IndexOf(savedData)] = data;

            return data;
        }
    }
}