using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Skahal.Infrastructure.Framework.Domain;
using System.Collections.Generic;

namespace PGP.Infrastructure.Framework.Domain
{
    /// <summary>
    /// Define a interface básica de um serviço de domínio.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IDomainService<TEntity> where TEntity : DomainEntityBase, IAggregateRoot
    {
        /// <summary>
        /// Obtém a entidade pelo id informado.
        /// </summary>
        /// <param name="id">O id da entidade desejada.</param>
        /// <returns>A instância da entidade.</returns>
        TEntity GetById(int id);

        /// <summary>
        /// Remove a entidade com o id informado.
        /// </summary>
        /// <param name="id">O id da entidade a ser removida.</param>
        void Remove(int id);

        /// <summary>
        /// Salva a entidade informada
        /// </summary>
        /// <param name="entity">A entidade a ser salva.</param>
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
    }
}
