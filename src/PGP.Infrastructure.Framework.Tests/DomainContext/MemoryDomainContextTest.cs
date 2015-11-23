using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PGP.Infrastructure.Framework.DomainContext;
using PGP.Infrastructure.Framework.Repositories;
using Moq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace PGP.Infrastructure.Framework.Tests.DomainContext
{
    /// <summary>
    /// Test class for MemoryDomainContext
    /// </summary>
    [TestClass]
    public class MemoryDomainContextTest
    {
        #region Private properties

        MemoryDomainContext m_target;

        #endregion

        #region Initialization

        /// <summary>
        /// Initialize the Tests for the class MemoryDomainContext
        /// </summary>
        [TestInitialize]
        public void InitializeTests()
        {
            m_target = new MemoryDomainContext();
        }

        #endregion

        #region Tests

        /// <summary>
        /// MethodName_Parameter_Expected
        /// </summary>
        [TestMethod]
        public void RegisterRepository_Repository_Success()
        {
            var mock = new Mock<IRepository<EntityBase>>();
            m_target.RegisterRepository(mock.Object);
        }

        /// <summary>
        /// MethodName_Parameter_Expected
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterRepository_Null_ArgumentNullException()
        {
            m_target.RegisterRepository<EntityBase>(null);
            Assert.Fail();
        }

        /// <summary>
        /// MethodName_Parameter_Expected.
        /// </summary>
        [TestMethod]
        public void RegisterNew_EntityBase_Success()
        {
            m_target.RegisterNew(new EntityBase());
        }

        /// <summary>
        /// Tests the method RegisterNew with parameter Null and the expected result is ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterNew_Null_ArgumentNullException()
        {
            m_target.RegisterNew<EntityBase>(null);
            Assert.Fail();
        }

        /// <summary>
        /// Tests the method Attach with parameter EntityBase and the expected result is Success
        /// </summary>
        [TestMethod]
        public void Attach_EntityBase_Success()
        {
            m_target.Attach(new EntityBase());
        }

        /// <summary>
        /// Tests the method Attach with parameter Null and the expected result is ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Attach_Null_ArgumentNullException()
        {
            m_target.Attach<EntityBase>(null);
            Assert.Fail();
        }

        /// <summary>
        /// Tests the method RegisterDeleted with parameter EntityBase and the expected result is Success
        /// </summary>
        [TestMethod]
        public void RegisterDeleted_EntityBase_Success()
        {
            var entity = new EntityBase();
            m_target.Attach(entity);
            m_target.RegisterDeleted(entity);
        }

        /// <summary>
        /// Tests the method RegisterDeleted with parameter Null and the expected result is ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterDeleted_Null_ArgumentNullException()
        {
            m_target.RegisterDeleted<EntityBase>(null);
            Assert.Fail();
        }

        /// <summary>
        /// Tests the method RegisterChanged with parameter EntityBase and the expected result is Success
        /// </summary>
        [TestMethod]
        public void RegisterChanged_EntityBase_Success()
        {
            var entity = new EntityBase();
            m_target.Attach(entity);
            entity.Id = 5;
            m_target.RegisterChanged(entity);
        }

        /// <summary>
        /// Tests the method RegisterChanged with parameter Null and the expected result is ArgumentNullException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterChanged_Null_ArgumentNullException()
        {
            m_target.RegisterChanged<EntityBase>(null);
            Assert.Fail();
        }

        /// <summary>
        /// Tests the method CreateQuery with parameter Default and the expected result is Entities
        /// </summary>
        [TestMethod]
        public void CreateQuery_Default_Entities()
        {
            var initialList = m_target.CreateQuery<EntityBase>().ToList();

            var entityToAdd = new EntityBase();
            m_target.RegisterNew(entityToAdd);
            m_target.SaveContextChanges();


            var finalList = m_target.CreateQuery<EntityBase>().ToList();

            Assert.AreEqual(initialList.Count + 1, finalList.Count);
            Assert.IsTrue(finalList.Any(x => x.Id == entityToAdd.Id));
        }

        /// <summary>
        /// Tests the method CreateQuery with parameter UnexistingQuery and the expected result is None
        /// </summary>
        [TestMethod]
        public void CreateQuery_UnexistingQuery_None()
        {
            var initialList = m_target.CreateQuery<EntityBase>().ToList();

            var entityToAdd = new EntityBase();
            m_target.RegisterNew(entityToAdd);
            m_target.SaveContextChanges();


            var finalList = m_target.CreateQuery<EntityBase>()
                .Where(x => x.Id == -1).ToList();

            Assert.AreEqual(initialList.Count, finalList.Count);
        }

        #endregion
    }
}
