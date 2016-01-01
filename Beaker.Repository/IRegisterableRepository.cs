using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beaker.Repository
{
    public interface IRegisterableRepository
    {
        void Register(IRepositoryRegistrar registrar);
    }
}
