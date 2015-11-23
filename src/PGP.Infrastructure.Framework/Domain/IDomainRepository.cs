using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;

namespace PGP.Infrastructure.Framework.Domain
{
    /// <summary>
    /// The domain repository interface
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IDomainRepository<TEntity> : IRepository<TEntity> where TEntity : IAggregateRoot
    {
        /// <summary>
        /// Finds all entities.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll(int offset, int limit, Expression<Func<TEntity, bool>> filter, Func<IEnumerable<TEntity>, IEnumerable<TEntity>> order);

        /// <summary>
        /// Finds all the entities with the given filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter = null);
    }
}
