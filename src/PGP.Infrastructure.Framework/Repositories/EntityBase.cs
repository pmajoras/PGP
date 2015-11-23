using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGP.Infrastructure.Framework.Repositories
{
    public class EntityBase : IEntity
    {
        public int Id
        {
            get; set;
        }

        public bool IsNew
        {
            get { return Id == 0; }
        }
    }
}
