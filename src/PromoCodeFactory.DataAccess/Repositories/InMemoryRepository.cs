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
        protected IEnumerable<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task AddAsync(T data)
        {
            return Task.FromResult(Data = Data.Append(data));
        }

        public Task RemoveAsync(Guid id)
        {
            return Task.FromResult(Data = Data.Where(x => x.Id != id));
        }

        public Task<T> UpdateAsync(Guid id, T data)
        {
            T savedData = GetByIdAsync(id).Result;

            if(savedData == null)
            {
                return null;
            }

            var list = Data.ToList();

            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].Id != id)
                {
                    continue;
                }

                list[i] = data;
                break;
            }

            Data = list;

            return Task.FromResult(data);
        }
    }
}