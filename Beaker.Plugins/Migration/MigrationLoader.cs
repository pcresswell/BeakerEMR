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


namespace Beaker.Plugins.Repository.SQLite
{
    internal class MigrationLoader
    {
        private IMigratable Database { get; set; }
        
        public MigrationLoader(IMigratable database)
        {
            this.Database = database;
        }

        public void LoadByAssembly(Assembly assembly)
        {
            var configuration = new ContainerConfiguration().WithAssembly(assembly);

            using (var container = configuration.CreateContainer())
            {
                var plugin = container.GetExport<IPlugin>();

                foreach (var migration in plugin.Migrations)
                {
                    Database.Apply(migration);
                }
            }
        }
    }
}
