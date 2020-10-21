using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask.Core.DataBase;

namespace TestTask.Core.Contracts
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> CreateAsync(T entity);
        void Update(T entity);
        
        void Delete(List<T> entities);

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetPageAsync(int numberPage, int size);
    }

    public interface IRepository<T, TId> : IRepository<T>
        where T : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        Task<T> GetByIdAsync(TId id);
    }
}
