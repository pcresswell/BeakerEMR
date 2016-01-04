using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Repository;

namespace Beaker.Plugins
{
    public interface IPlugin
    {
		IEnumerable<IRepository> Repositories { get; }
        IEnumerable<IMigration> Migrations { get; }
    } 
}
