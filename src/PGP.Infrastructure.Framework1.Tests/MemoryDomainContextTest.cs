using System.Collections.Generic;
using PGP.Infrastructure.Framework.Repositories;
// <copyright file="MemoryDomainContextTest.cs">Copyright ©  2015</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PGP.Infrastructure.Framework.DomainContext;

namespace PGP.Infrastructure.Framework.DomainContext.Tests
{
    /// <summary>This class contains parameterized unit tests for MemoryDomainContext</summary>
    [TestClass]
    [PexClass(typeof(MemoryDomainContext))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class MemoryDomainContextTest
    {

        /// <summary>Test stub for .ctor()</summary>
        [PexMethod]
        public MemoryDomainContext ConstructorTest()
        {
            MemoryDomainContext target = new MemoryDomainContext();
            return target;
        }

        /// <summary>Test stub for Attach(!!0)</summary>
        [PexMethod]
        [PexGenericArguments(typeof(EntityBase))]
        [PexAllowedException(typeof(ArgumentNullException))]
        public void AttachTest<TEntity>([PexAssumeUnderTest]MemoryDomainContext target, TEntity entity)
            where TEntity : IEntity
        {
            target.Attach(entity);
            // TODO: add assertions to method MemoryDomainContextTest.AttachTest(MemoryDomainContext, !!0)
        }

        /// <summary>Test stub for BeginTransaction()</summary>
        [PexMethod]
        public void BeginTransactionTest([PexAssumeUnderTest]MemoryDomainContext target)
        {
            target.BeginTransaction();
            Assert.IsTrue(target.HasPendingTransaction);
        }

        /// <summary>Test stub for ClearContext()</summary>
        [PexMethod]
        public void ClearContextTest([PexAssumeUnderTest]MemoryDomainContext target)
        {
            target.ClearContext();
        }

        /// <summary>Test stub for CommitTransaction()</summary>
        [PexMethod]
        public void CommitTransactionTest([PexAssumeUnderTest]MemoryDomainContext target)
        {
            target.CommitTransaction();
            Assert.IsFalse(target.HasPendingTransaction);
        }

        /// <summary>Test stub for CreateQuery()</summary>
        [PexGenericArguments(typeof(EntityBase))]
        [PexMethod]
        public IEnumerable<TEntity> CreateQueryTest<TEntity>([PexAssumeUnderTest]MemoryDomainContext target)
            where TEntity : IEntity
        {
            IEnumerable<TEntity> result = target.CreateQuery<TEntity>();
            return result;
        }

        /// <summary>Test stub for Dispose()</summary>
        [PexMethod]
        public void DisposeTest([PexAssumeUnderTest]MemoryDomainContext target)
        {
            target.Dispose();
            // TODO: add assertions to method MemoryDomainContextTest.DisposeTest(MemoryDomainContext)
        }

        /// <summary>Test stub for RegisterChanged(!!0)</summary>
        [PexGenericArguments(typeof(EntityBase))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public void RegisterChangedTest<TEntity>([PexAssumeUnderTest]MemoryDomainContext target, TEntity entity)
            where TEntity : IEntity
        {
            target.RegisterChanged<TEntity>(entity);
            // TODO: add assertions to method MemoryDomainContextTest.RegisterChangedTest(MemoryDomainContext, !!0)
        }

        /// <summary>Test stub for RegisterDeleted(!!0)</summary>
        [PexGenericArguments(typeof(EntityBase))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public void RegisterDeletedTest<TEntity>([PexAssumeUnderTest]MemoryDomainContext target, TEntity entity)
            where TEntity : IEntity
        {
            target.RegisterDeleted<TEntity>(entity);
            // TODO: add assertions to method MemoryDomainContextTest.RegisterDeletedTest(MemoryDomainContext, !!0)
        }

        /// <summary>Test stub for RegisterNew(!!0)</summary>
        [PexGenericArguments(typeof(EntityBase))]
        [PexMethod]
        [PexAllowedException(typeof(ArgumentNullException))]
        public void RegisterNewTest<TEntity>([PexAssumeUnderTest]MemoryDomainContext target, TEntity entity)
            where TEntity : IEntity
        {
            target.RegisterNew<TEntity>(entity);
            // TODO: add assertions to method MemoryDomainContextTest.RegisterNewTest(MemoryDomainContext, !!0)
        }

        /// <summary>Test stub for RoolbackTransaction()</summary>
        [PexMethod]
        public void RoolbackTransactionTest([PexAssumeUnderTest]MemoryDomainContext target)
        {
            target.RoolbackTransaction();
            Assert.IsFalse(target.HasPendingTransaction);
        }

        /// <summary>Test stub for SaveContextChanges()</summary>
        [PexMethod]
        public void SaveContextChangesTest([PexAssumeUnderTest]MemoryDomainContext target)
        {
            target.SaveContextChanges();
            // TODO: add assertions to method MemoryDomainContextTest.SaveContextChangesTest(MemoryDomainContext)
        }
    }
}
