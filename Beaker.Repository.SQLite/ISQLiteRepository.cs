using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beaker.Repository.SQLite
{
    public interface ISQLiteRepository : IRepository
    {
        BeakerSQLiteConnection Connection { set; }
    }
}
