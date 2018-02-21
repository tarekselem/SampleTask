using Futura.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Futura.DataAccess.EF
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entitySet;
        #endregion

        #region Constructor
        public Repository(DbContext context)
        {
            _context = context;
            _context.Database.CommandTimeout = 180;
            _entitySet = _context.Set<TEntity>();
        }
        #endregion

        #region Interface Implementation

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IList<Expression<Func<TEntity, object>>> includedProperties = null, int? pageIndex = null, int? pageSize = null)
        {
            IQueryable<TEntity> entities = _entitySet;
            //Filtering
            if (filter != null) entities = entities.Where(filter);

            //Sorting
            orderBy?.Invoke(entities);

            //Including
            if (includedProperties != null)
            {
                foreach (var property in includedProperties)
                    entities = entities.Include(property);
            }

            //Paging
            if (orderBy != null && pageIndex.HasValue && pageSize.HasValue) entities = orderBy(entities).Skip(pageSize.Value * pageIndex.Value).Take(pageSize.Value);

            return entities;
        }

        public virtual async Task<ICollection<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IList<Expression<Func<TEntity, object>>> includedProperties = null, int? pageIndex = null, int? pageSize = null)
        {
            IQueryable<TEntity> entities = _entitySet;
            //Filtering
            if (filter != null) entities = entities.Where(filter);

            //Sorting
            orderBy?.Invoke(entities);

            //Including
            if (includedProperties != null)
            {
                foreach (var property in includedProperties)
                    entities = entities.Include(property);
            }

            //Paging
            if (pageIndex.HasValue && pageSize.HasValue) entities = orderBy(entities).Skip(pageSize.Value * pageIndex.Value).Take(pageSize.Value);

            return await entities.Select(selector).ToListAsync();
        }

        public virtual TEntity GetById(object id)
        {
            return _entitySet.Find(id);
        }

        public virtual async Task<TEntity> GetAsyncByIdAsync(object id)
        {
            return await _entitySet.FindAsync(id);
        }

        public virtual void BulkDelete(IQueryable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                    _entitySet.Attach(entity);
            }

            _entitySet.RemoveRange(entities.AsEnumerable());
        }

        public virtual IEnumerable<TEntity> BulkInsert(IEnumerable<TEntity> entities)
        {
            return _entitySet.AddRange(entities);
        }

        public virtual bool Delete(object id)
        {
            var entity = _entitySet.Find(id);
            if (entity == null) return false;

            if (_context.Entry(entity).State == EntityState.Detached)
                _entitySet.Attach(entity);

            _entitySet.Remove(entity);
            return true;
        }

        public virtual void Detach(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return _entitySet.Add(entity);
        }

        public virtual bool Update(TEntity entity)
        {
            var attchedEntity = _entitySet.Attach(entity);
            if (attchedEntity == null) return false;
            
            _context.Entry(entity).State = EntityState.Modified;
            return true;
        }

        #endregion

        #region IDisposable Support
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
