using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGP.Infrastructure.Framework.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Save the changes of this <see cref="IUnitOfWork"/> instance to the database
        /// This method is supposed to rollback the changes from the database in case of an error
        /// </summary>
        void Commit();

        /// <summary>
        /// Discards the changes of this <see cref="IUnitOfWork"/> instance.
        /// </summary>
        void DiscardChanges();
    }
}
