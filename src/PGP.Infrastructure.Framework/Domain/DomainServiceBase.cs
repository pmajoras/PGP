using System;
using System.Diagnostics.CodeAnalysis;
using HelperSharp;
using KissSpecifications;
using KissSpecifications.Commons;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using System.Collections.Generic;
using PGP.Infrastructure.Framework.Commons.Specs;
using System.Linq;
using System.Linq.Expressions;

namespace PGP.Infrastructure.Framework.Domain
{
    /// <summary>
    /// Classe base para serviços de domínio.
    /// </summary>
    /// <typeparam name="TEntity">A entidade que o serviço trabalha primariamente.</typeparam>
    /// <typeparam name="TMainRepository">O repositório principal.</typeparam>
    public abstract class DomainServiceBase<TEntity, TMainRepository> : ServiceBase<TEntity, TMainRepository, IUnitOfWork>, IDomainService<TEntity>
        where TEntity : DomainEntityBase, IAggregateRoot
        where TMainRepository : IRepository<TEntity>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainServiceBase{TEntity, TMainRepository}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        protected DomainServiceBase(TMainRepository repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public TEntity GetById(int id)
        {
            return MainRepository.FindBy(id);
        }

        /// <summary>
        /// Gets all the entities with the given filter.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            IEnumerable<TEntity> entitiesToReturn = null;

            if (predicate != null)
            {
                entitiesToReturn = MainRepository.FindAll(predicate);
            }
            else
            {
                entitiesToReturn = MainRepository.FindAll();
            }

            return entitiesToReturn;
        }

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Save(TEntity entity)
        {
            ExceptionHelper.ThrowIfNull("entity", entity);

            SpecService.Assert(
               entity,
               GetSaveSpecifications(entity));

            MainRepository[entity.Id] = entity;
        }

        /// <summary>
        /// Saves a list of entities
        /// </summary>
        /// <param name="entityList">The list of entities to save</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public virtual void SaveEntityList(IEnumerable<TEntity> entityList)
        {
            ExceptionHelper.ThrowIfNull("entityList", entityList);

            foreach (var entity in entityList)
            {
                SpecService.Assert(
                 entity,
                 GetSaveSpecifications(entity));

                MainRepository[entity.Id] = entity;
            }
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Remove(int id)
        {
            var entity = MainRepository.FindBy(id);

            SpecService.Assert(
                entity,
                new MustNotBeNullSpecification<TEntity>());

            SpecService.Assert(entity, GetRemoveSpecifications(entity));

            MainRepository.Remove(entity);
        }

        /// <summary>
        /// Counts All the entities.
        /// </summary>
        /// <returns></returns>
        public long Count()
        {
            return MainRepository.CountAll();
        }

        /// <summary>
        /// Commit the pending changes and roolback in case of error
        /// </summary>
        public void Commit()
        {
            try
            {
                UnitOfWork.Commit();
            }
            catch (Exception)
            {
                UnitOfWork.Rollback();
                throw;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the save specifications.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        protected virtual ISpecification<TEntity>[] GetSaveSpecifications(TEntity entity)
        {
            return new ISpecification<TEntity>[]
            {
                new MustComplyWithMetadataSpecification<TEntity>()
            };
        }

        /// <summary>
        /// Gets the remove specifications.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        protected virtual ISpecification<TEntity>[] GetRemoveSpecifications(TEntity entity)
        {
            return new ISpecification<TEntity>[0];
        }

        #endregion
    }
}
