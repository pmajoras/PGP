using System;
using Skahal.Infrastructure.Framework.Domain;
using PGP.Infrastructure.Framework.Domain;

namespace PGP.Infrastructure.Framework.Tests.Domain
{
    public class DomainEntityBaseStub : DomainEntityBase, IAggregateRoot
    {
        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
