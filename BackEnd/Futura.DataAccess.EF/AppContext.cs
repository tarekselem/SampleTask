using System.Data.Entity;

namespace Futura.DataAccess.EF
{
    public class AppContext : DbContext
    {
        #region Construcor
        public AppContext() : base("SQLConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        #endregion

        #region Entities
        public IDbSet<Entities.Customer> Customers { get; set; }
        public IDbSet<Entities.Order> Orders { get; set; }
        #endregion

        #region Overriden Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Database.CommandTimeout = 180;
        }
        #endregion
    }
}
