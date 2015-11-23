using PGP.Infrastructure.Framework.DomainContext;
// <copyright file="MemoryDomainContextFactory.cs">Copyright ©  2015</copyright>

using System;
using Microsoft.Pex.Framework;

namespace PGP.Infrastructure.Framework.DomainContext
{
    /// <summary>A factory for PGP.Infrastructure.Framework.DomainContext.MemoryDomainContext instances</summary>
    public static partial class MemoryDomainContextFactory
    {
        /// <summary>A factory for PGP.Infrastructure.Framework.DomainContext.MemoryDomainContext instances</summary>
        [PexFactoryMethod(typeof(MemoryDomainContext))]
        public static MemoryDomainContext Create()
        {
            MemoryDomainContext memoryDomainContext = new MemoryDomainContext();
            return memoryDomainContext;
        }

        [PexFactoryMethod(typeof(MemoryDomainContext))]
        public static MemoryDomainContext CreateWithTransaction()
        {
            MemoryDomainContext memoryDomainContext = new MemoryDomainContext();
            memoryDomainContext.BeginTransaction();
            return memoryDomainContext;
        }
    }
}
