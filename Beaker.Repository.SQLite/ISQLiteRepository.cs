using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core;

namespace Beaker.Repository.SQLite
{
    public interface ISQLiteRepository<TPersistable> : IRepository<TPersistable>, ISQLiteRepository where TPersistable : IPersistable
    {
    }

    public interface ISQLiteRepository : IRepository
    {
        BeakerSQLiteConnection Connection { set; }
    }
}
