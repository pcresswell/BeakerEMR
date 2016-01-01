using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beaker.Repository
{
    public interface IRepositoryRegistrar
    {
        void RegisterRepository<TRepository>(TRepository repository) where TRepository : IRepository;
        TRepository Repository<TRepository>() where TRepository : IRepository;
    }
}
