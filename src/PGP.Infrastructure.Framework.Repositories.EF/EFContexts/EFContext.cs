using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGP.Infrastructure.Framework.Repositories.EF.EFContexts
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class EfContext : DbContext, IDomainContext
    {
        #region Private Properties

        #endregion

        #region Constructors

        public EfContext()
        {

        }

        #endregion

        #region Interface Methods

        public void RegisterRepository<TRepository>(IRepository<TRepository> repository) where TRepository : IEntity
        {
            throw new NotImplementedException();
        }

        public void RegisterNew<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var entry = Entry(entity);
            if (entry.State == EntityState.Added)
            {
                entry.State = EntityState.Added;
            }
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var entry = Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Unchanged;
            }
        }

        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var entry = Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
        }

        public void RegisterChanged<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var entry = Entry(entity);
            if (entry.State != EntityState.Modified)
            {
                entry.State = EntityState.Modified;
            }
        }

        public IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : class, IEntity
        {
            return Set<TEntity>();
        }

        public TEntity GetById<TEntity>(object id) where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public void RoolbackTransaction()
        {
            throw new NotImplementedException();
        }

        public void SaveContextChanges()
        {
            throw new NotImplementedException();
        }

        public void ClearContext()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
