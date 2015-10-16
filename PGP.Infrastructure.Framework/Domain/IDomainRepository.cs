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
        /// Busca todas as instâncias da entidade.
        /// </summary>
        /// <param name="offset">O início do retorno.</param>
        /// <param name="limit">A quantidade máxima de instâncias.</param>
        /// <param name="filter">O filtro.</param>
        /// <param name="order">A função de ordenação.</param>
        /// <returns>O resultado da busca.</returns>
        IEnumerable<TEntity> FindAll(int offset, int limit, Expression<Func<TEntity, bool>> filter, Func<IEnumerable<TEntity>, IEnumerable<TEntity>> order);

        /// <summary>
        /// Get all the entities with the given filter
        /// </summary>
        /// <param name="filter">The filter, if is null will return all the entities</param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter = null);
    }
}
