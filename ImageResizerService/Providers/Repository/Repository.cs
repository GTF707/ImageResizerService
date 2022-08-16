using ImageResizerService.Domen;
using ImageResizerService.Providers.Repository.Repository;
using ImageResizerService.Storage;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace ImageResizerService.Providers.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : PersistentObject
    {
        private readonly AppDbContext context;

        public Repository(AppDbContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> selector)
        {

            return context.Set<T>().Where(selector).Where(x => !CheckDeletable.IsDeleted(x)).AsQueryable();
        }

        public virtual async Task CreateAsync(T newEntity)
        {
            newEntity.CreateDate = DateTime.Now;
            newEntity.UpdateDate = DateTime.Now;
            await context.Set<T>().AddAsync(newEntity);
        }

        public virtual async Task AddRange(IEnumerable<T> newEntities)
        {
            await context.Set<T>().AddRangeAsync(newEntities);
        }

        public virtual async Task RemoveRange(IEnumerable<T> entities)
        {
            await Task.Run(() => context.Set<T>().RemoveRange(entities));
        }

        public virtual void Update(T entity)
        {
            entity.UpdateDate = DateTime.Now;
            context.Set<T>().Update(entity);
        }

        public virtual async Task UpdateRange(IEnumerable<T> entities)
        {
            await Task.Run(() => context.Set<T>().UpdateRange(entities));
        }



        public virtual async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public virtual IQueryable<T> GetAll()
        {
            return context.Set<T>().Where(x => !x.IsDeleted);

        }

        public virtual void Create(T item)
        {
            item.CreateDate = DateTime.Now;
            item.UpdateDate = DateTime.Now;
            context.Set<T>().Add(item);
        }

        public virtual void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DateDelete = DateTime.UtcNow;
            context.Update(entity);
        }

        public virtual void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public virtual int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}