using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Repository.SQLite;
using System.Composition;
using Beaker.Repository;

namespace Beaker.Plugins.Repository.SQLite
{
    [Export(typeof(IPlugin))]
    public class Plugin : Beaker.Plugins.Plugin
    {
        private IList<ISQLiteRepository> RepositoryList { get; set; }

        public Plugin()
        {
            this.RepositoryList = new List<ISQLiteRepository>();
            this.RepositoryList.Add(new PersonRepository());
            this.RepositoryList.Add(new MedicationRepository());
            this.RepositoryList.Add(new PatientRepository());
        }

        protected override IEnumerable<IMigration> GetMigrations()
        {
            return new List<IMigration>();
        }

        protected override IEnumerable<IRepository> GetRepositories()
        {
            return this.RepositoryList;
        }
    }
}
