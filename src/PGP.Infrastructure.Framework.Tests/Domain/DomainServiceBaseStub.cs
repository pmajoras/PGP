using PGP.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;

namespace PGP.Infrastructure.Framework.Tests.Domain
{
    public class DomainServiceBaseStub : DomainServiceBase<DomainEntityBaseStub, IRepository<DomainEntityBaseStub>>
    {
        public DomainServiceBaseStub(IRepository<DomainEntityBaseStub> repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {


        }
    }
}
