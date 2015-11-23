﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGP.Infrastructure.Framework.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Open and commit a transaction of this <see cref="IUnitOfWork"/> instance to the database
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks the openned transaction
        /// </summary>
        void Rollback();

        /// <summary>
        /// Discards the changes of this <see cref="IUnitOfWork"/> instance.
        /// </summary>
        void DiscardChanges();
    }
}
