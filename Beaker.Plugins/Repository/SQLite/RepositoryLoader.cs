using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Repository.SQLite;
using SQLite;
using System.Composition;
using System.Composition.Hosting;
using Beaker.Repository;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Beaker.Test")]
namespace Beaker.Plugins.Repository.SQLite
{
    internal class RepositoryLoader
    {
        private SQLiteDatabase Database { get; set; }
        
        public RepositoryLoader(SQLiteDatabase database)
        {
            this.Database = database;
        }

        public void LoadByAssembly(Assembly assembly)
        {
            var configuration = new ContainerConfiguration().WithAssembly(assembly);

            using (var container = configuration.CreateContainer())
            {
                var plugin = container.GetExport<IPlugin>();

                foreach (var repository in plugin.Repositories)
                {
                    repository.Register(this.Database); 
                }
            }
        }
    }
}
