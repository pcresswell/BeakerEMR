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
        }

        protected abstract IEnumerable<IMigration> GetMigrations();

        protected abstract IEnumerable<IRepository> GetRepositories();

        IEnumerable<IRepository> IPlugin.Repositories
        {
            get
            {
                if (this.Repositories.Count == 0)
                {
                    foreach (var repo in this.GetRepositories())
                    {
                        this.Repositories.Add(repo);
                    }
                }

                return this.Repositories;
            }
        }

        IEnumerable<IMigration> IPlugin.Migrations
        {
            get
            {
                if (this.Migrations.Count == 0)
                {
                    foreach (var migration in this.GetMigrations())
                    {
                        this.Migrations.Add(migration);
                    }
                }

                return this.Migrations;
            }
        }
    }
}
