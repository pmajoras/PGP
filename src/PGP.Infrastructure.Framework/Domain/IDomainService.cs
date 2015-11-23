using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Skahal.Infrastructure.Framework.Domain;
using System.Collections.Generic;

namespace PGP.Infrastructure.Framework.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IDomainService<TEntity> where TEntity : DomainEntityBase, IAggregateRoot
    {
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// Gets all the entities with the given filter.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Remove(int id);

        /// <summary>
        /// Saves the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Save(TEntity entity);

        /// <summary>
        /// Saves a list of entities
        /// </summary>
        /// <param name="entityList">The list of entities to save</param>
        void SaveEntityList(IEnumerable<TEntity> entityList);

        /// <summary>
        /// Commit the pending changes and roolback in case of error
        /// </summary>
        void Commit();

        /// <summary>
        /// Counts All the entities.
        /// </summary>
        long Count();
    }
}
