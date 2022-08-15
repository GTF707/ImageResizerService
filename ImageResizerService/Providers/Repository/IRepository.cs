using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ImageResizerService.Providers.Repository.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Get(Expression<Func<T, bool>> selector);
        IQueryable<T> GetAll();
        void Create(T item);
        Task CreateAsync(T item);
        Task AddRange(IEnumerable<T> newEntities);
        void Delete(T entity);
        void Remove(T entity);
        Task RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        Task UpdateRange(IEnumerable<T> entities);
        Task<int> SaveChangesAsync();
        int SaveChanges();
       
        
    }
}
