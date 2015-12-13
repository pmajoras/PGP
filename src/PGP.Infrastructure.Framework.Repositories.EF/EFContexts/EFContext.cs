﻿using HelperSharp;
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

        /// <summary>
        /// The m_current transaction
        /// </summary>
        private DbContextTransaction m_currentTransaction = null;

        #endregion

        #region Protected Properties

        protected Dictionary<Type, IRepository<IEntity>> m_registeredRepositories = new Dictionary<Type, IRepository<IEntity>>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EfContext"/> class.
        /// </summary>
        public EfContext()
        {
        }

        #endregion

        #region Interface Methods

        /// <summary>
        /// Registers the repository.
        /// </summary>
        /// <typeparam name="TRepository">The type of the repository.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <exception cref="System.ArgumentException">The repository of type  + key.Name +  is already registered.</exception>
        public virtual void RegisterRepository<TRepository>(IRepository<TRepository> repository) where TRepository : class, IEntity
        {
            ExceptionHelper.ThrowIfNull("repository", repository);

            var key = typeof(TRepository);
            if (m_registeredRepositories.ContainsKey(key))
            {
                throw new ArgumentException("The repository of type " + key.Name + " is already registered.");
            }

            m_registeredRepositories.Add(key, repository as IRepository<IEntity>);
        }

        /// <summary>
        /// Registers the new.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void RegisterNew<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            RegisterState(entity, EntityState.Added);
        }

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void Attach<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            ExceptionHelper.ThrowIfNull("entity", entity);

            var entry = Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Unchanged;
            }
        }

        /// <summary>
        /// Registers the deleted.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            RegisterState(entity, EntityState.Deleted);
        }

        /// <summary>
        /// Registers the changed.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public virtual void RegisterChanged<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            RegisterState(entity, EntityState.Modified);
        }

        /// <summary>
        /// Creates the query.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        public virtual IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : class, IEntity
        {
            return Set<TEntity>();
        }

        /// <summary>
        /// Gets an entity by identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual TEntity GetById<TEntity>(object id) where TEntity : class, IEntity
        {
            return Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public virtual void BeginTransaction()
        {
            m_currentTransaction = Database.BeginTransaction();
        }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public virtual void CommitTransaction()
        {
            if (m_currentTransaction == null)
            {
                throw new InvalidOperationException("A transaction must have been opened in order to commit the transaction");
            }

            m_currentTransaction.Commit();
            m_currentTransaction.Dispose();
            m_currentTransaction = null;
        }

        /// <summary>
        /// Roolbacks the transaction.
        /// </summary>
        public virtual void RoolbackTransaction()
        {
            if (m_currentTransaction == null)
            {
                throw new InvalidOperationException("A transaction must have been opened in order to roolback the transaction");
            }

            m_currentTransaction.Rollback();
            m_currentTransaction.Dispose();
            m_currentTransaction = null;
        }

        /// <summary>
        /// Saves the context changes.
        /// </summary>
        public virtual void SaveContextChanges()
        {
            var contextEntries = ChangeTracker.Entries().Where(x => x.State != EntityState.Detached && x.State != EntityState.Unchanged);
            var repositories = m_registeredRepositories
                .Where(x => x.Value.GetType().GetGenericArguments().Any())
                .Select(x => x.Value).ToList();

            foreach (var repository in repositories)
            {
                var entries = contextEntries.Where(x => x.Entity.GetType() == repository.GetType().GetGenericArguments()[0]);
                foreach (var entry in entries)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            repository.BeforePersistNewItem(entry.Entity as IEntity);
                            break;
                        case EntityState.Deleted:
                            repository.BeforeDeleteItem(entry.Entity as IEntity);
                            break;
                        case EntityState.Modified:
                            repository.BeforePersistUpdatedItem(entry.Entity as IEntity);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (m_currentTransaction != null)
            {
                try
                {
                    CommitTransaction();
                }
                catch (Exception)
                {
                    RoolbackTransaction();
                    throw;
                }
            }
            else
            {
                SaveChanges();
            }
        }

        /// <summary>
        /// Detaches all the DbEntityEntry from the context
        /// </summary>
        public virtual void ClearContext()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity != null)
                {
                    entry.State = EntityState.Detached;
                }
            }

            m_registeredRepositories.Clear();
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (m_currentTransaction != null)
            {
                m_currentTransaction.Dispose();
            }
            ClearContext();
            base.Dispose(disposing);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Registers the state.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="state">The state.</param>
        /// <param name="stateToDiff">The state to difference.</param>
        private void RegisterState<TEntity>(TEntity entity, EntityState state, EntityState? stateToDiff = null) where TEntity : class, IEntity
        {
            ExceptionHelper.ThrowIfNull("entity", entity);

            if (!stateToDiff.HasValue)
            {
                stateToDiff = state;
            }

            var entry = Entry(entity);
            if (entry.State != stateToDiff)
            {
                entry.State = state;
            }
        }

        #endregion
    }
}
