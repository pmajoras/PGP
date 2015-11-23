using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skahal.Infrastructure.Framework.Repositories;
using System.Linq;
namespace PGP.Infrastructure.Framework.Tests.Domain
{
    [TestClass]
    public class DomainServiceBaseTest
    {
        [TestMethod]
        public void GetEntities_Args_Filtered()
        {
            var unitOfWork = new MemoryUnitOfWork();
            var repository = new MemoryDomainServiceBaseStubRepository(unitOfWork);
            repository.Add(new DomainEntityBaseStub());
            repository.Add(new DomainEntityBaseStub());
            unitOfWork.Commit();

            var target = new DomainServiceBaseStub(repository, unitOfWork);
            var actual = target.GetAll();
            Assert.AreEqual(2, actual.Count());
        }

        [TestMethod]
        public void Count_NoArgs_CountAllEntities()
        {
            var unitOfWork = new MemoryUnitOfWork();
            var repository = new MemoryDomainServiceBaseStubRepository(unitOfWork);
            repository.Add(new DomainEntityBaseStub());
            repository.Add(new DomainEntityBaseStub());
            unitOfWork.Commit();

            var target = new DomainServiceBaseStub(repository, unitOfWork);
            var actual = target.Count();
            Assert.AreEqual(2, actual);
        }
    }
}
