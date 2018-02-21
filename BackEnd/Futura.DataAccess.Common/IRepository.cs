using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Futura.DataAccess.Common
{
    public interface IRepository<TEntity> : IDisposable
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IList<Expression<Func<TEntity, object>>> includedProperties = null, int? pageIndex = null, int? pageSize = null);
        Task<ICollection<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IList<Expression<Func<TEntity, object>>> includedProperties = null, int? pageIndex = null, int? pageSize = null);
        TEntity GetById(object id);
        Task<TEntity> GetAsyncByIdAsync(object id);
        TEntity Insert(TEntity entity);
        IEnumerable<TEntity> BulkInsert(IEnumerable<TEntity> entities);
        bool Update(TEntity entity);
        bool Delete(object id);
        void BulkDelete(IQueryable<TEntity> entities);
        void Detach(TEntity entity);
    }
}
