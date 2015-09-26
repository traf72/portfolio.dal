using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Portfolio.Dal.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext context;
        private readonly IDbSet<TEntity> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            dbSet = this.context.Set<TEntity>();
        }

        public virtual TEntity FindById<TId>(TId id)
        {
            return dbSet.Find(id);
        }

        public virtual bool Exists<TId>(TId id)
        {
            return dbSet.Find(id) != null;
        }

        public virtual IQueryable<TEntity> Query(IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (includeProperties != null && includeProperties.Any())
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            return query;
        }

        public TEntity Insert(TEntity entity)
        {
            return dbSet.Add(entity);
        }

        public TEntity Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual void DeleteById<TId>(TId id)
        {
            var entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }
    }
}