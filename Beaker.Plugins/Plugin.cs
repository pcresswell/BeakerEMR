using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Repository;

namespace Beaker.Plugins
{
    public abstract class Plugin : IPlugin
    {
        private IList<IMigration> Migrations { get; set; }
        private IList<IRepository> Repositories { get; set; }

        public Plugin()
        {
            this.Migrations = new List<IMigration>();
            this.Repositories = new List<IRepository>();
            foreach (var repo in this.GetRepositories())
            {
                this.Repositories.Add(repo);
            }

            foreach (var migration in this.GetMigrations())
            {
                this.Migrations.Add(migration);
            }

        }
        protected abstract IEnumerable<IMigration> GetMigrations();
        protected abstract IEnumerable<IRepository> GetRepositories();

        IEnumerable<IRepository> IPlugin.Repositories
        {
            get
            {
                return this.Repositories;
            }
        }

        IEnumerable<IMigration> IPlugin.Migrations
        {
            get
            {
                return this.Migrations;
            }
        }
        
    }
}
