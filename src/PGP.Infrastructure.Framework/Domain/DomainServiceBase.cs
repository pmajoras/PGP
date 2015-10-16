using System;
using System.Diagnostics.CodeAnalysis;
using HelperSharp;
using KissSpecifications;
using KissSpecifications.Commons;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using System.Collections.Generic;
using PGP.Infrastructure.Framework.Commons.Specs;

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
        /// Inicia uma nova instância da classe <see cref="DomainServiceBase{TEntity, TMainRepository}"/>.
        /// </summary>
        /// <param name="repository">O repositório.</param>
        /// <param name="unitOfWork">O unit of work.</param>
        protected DomainServiceBase(TMainRepository repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get an entity by id
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <returns></returns>
        public TEntity GetById(int id)
        {
            return MainRepository.FindBy(id);
        }

        /// <summary>
        /// Salva a entidade informada
        /// </summary>
        /// <param name="entity">A entidade a ser salva.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public virtual void Save(TEntity entity)
        {
            ExceptionHelper.ThrowIfNull("entity", entity);

            SpecService.Assert(
               entity,
               GetSaveSpecifications(entity));

            MainRepository[entity.Id] = entity;
        }

        /// <summary>
        /// Salva uma lista de entidades
        /// </summary>
        /// <param name="entityList">A entidade a ser salva.</param>
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
        /// Remove a entidade com o id informado.
        /// </summary>
        /// <param name="id">O id da entidade a ser removida.</param>
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
        /// Obtém as especificações que devem ser atentidas ao salvar a entidade.
        /// </summary>
        /// <param name="entity">A entidade.</param>
        /// <returns>As especificações.</returns>
        protected virtual ISpecification<TEntity>[] GetSaveSpecifications(TEntity entity)
        {
            return new ISpecification<TEntity>[]
            {
                new MustComplyWithMetadataSpecification<TEntity>()
            };
        }

        /// <summary>
        /// Obtém as especificações que devem ser atentidas ao remover a entidade.
        /// </summary>
        /// <param name="entity">A entidade.</param>
        /// <returns>As especificações.</returns>
        protected virtual ISpecification<TEntity>[] GetRemoveSpecifications(TEntity entity)
        {
            return new ISpecification<TEntity>[0];
        }


        #endregion
    }
}
