using Futura.DataAccess.Common;
using System;
using System.Data.Entity;

namespace Futura.DataAccess.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly DbContext _context;
        #endregion

        #region Constructor
        public UnitOfWork()
        {
            _context = new AppContext();
        }
        #endregion


        #region Interface Implementation
        public IRepository<TEntity> RepositoryFor<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_context);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();

        }
        #endregion
    }
}
