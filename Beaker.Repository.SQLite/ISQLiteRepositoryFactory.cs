using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Repository.SQLite;
using Beaker.Repository;
using Beaker.Core;

namespace Beaker.Repository.SQLite
{
    public interface ISQLiteRepositoryFactory
    {
        IEnumerable<ISQLiteRepository> Repositories { get; }
    }
}
