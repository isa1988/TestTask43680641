using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask.Core.Contracts;
using TestTask.Core.DataBase;
using TestTask.DAL.DataBase;

namespace TestTask.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity
    {
        public Repository(DbContextTestTask contextSpaTestTask)
        {
            this.contextSpaTestTask = contextSpaTestTask;
            dbSet = contextSpaTestTask.Set<T>();
        }
        protected DbContextTestTask contextSpaTestTask;
        protected DbSet<T> dbSet { get; set; }

        protected IQueryable<T> DbSetInclude { get; set; }

        public virtual async Task<T> CreateAsync(T entity)
        {
            var entry = await dbSet.AddAsync(entity);

            return entry.Entity;
        }
        protected IQueryable<T> GetInclude()
        {
            return DbSetInclude != null ? DbSetInclude : dbSet;
        }

        public virtual async Task<List<T>> GetPageAsync(int pageNumber, int rowCount)
        {
            int startIndex = (pageNumber - 1) * rowCount;
            return await GetInclude()
                   .Skip(startIndex)
                   .Take(rowCount)
                   .ToListAsync();
        }
        //identity
        public async Task<List<T>> GetAllAsync()
        {
            return await GetInclude().ToListAsync();
        }
        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteALot(List<T> entityList)
        {
            for (int i = 0; i < entityList.Count; i++)
            {
                dbSet.Remove(entityList[i]);
            }
        }

        public void Save()
        {
            contextSpaTestTask.SaveChanges();
        }
    }

    public abstract class Repository<T, TId> : Repository<T>, IRepository<T, TId>
        where T : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        private readonly bool isIdAutoIncrement;
        public Repository(DbContextTestTask contextSpaTestTask, bool isIdAutoIncrement = false)
            : base(contextSpaTestTask)
        {
            this.isIdAutoIncrement = isIdAutoIncrement;
        }
        public virtual async Task<T> GetByIdAsync(TId id)
        {
            var retVal = await GetInclude().FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (retVal == null) throw new NullReferenceException("Не найдеена запись");
            return retVal;
        }

        protected abstract TId GetNewId();

        public override async Task<T> CreateAsync(T entity)
        {
            if (!isIdAutoIncrement)
            {
                entity.Id = GetId(GetNewId());
            }

            return await base.CreateAsync(entity);
        }

        private TId GetId(TId value)
        {
            if (dbSet.Any(p => p.Id.Equals(value)))
            {
                
                value = GetNewId();
                return GetId(value);
            }

            return value;
        }
    }
}
