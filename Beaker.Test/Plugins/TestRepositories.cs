using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Composition;
using System.Composition.Hosting;
using Beaker.Core;
using Beaker.Repository;
using Beaker.Repository.SQLite;
using Beaker.Plugins.Repository.SQLite;
using SQLite;

namespace Beaker.Plugins.Test
{
    [TestFixture]
    public class TestRepositories
    {
        [Test]
        public void DynamicallyLoadPersonRepository()
        {
            SQLiteConnection sqlite = new SQLiteConnection(":memory:");
            BeakerSQLiteConnection connection = new BeakerSQLiteConnection(sqlite);
            SQLiteDatabase database = new SQLiteDatabase(connection);
            Assert.IsNull(database.Repository<IPersonRepository>());

            RepositoryLoader loader = new RepositoryLoader(database);

            loader.LoadByAssembly(typeof(Beaker.Plugins.Repository.SQLite.Plugin).Assembly);
            Assert.IsNotNull(database.Repository<IPersonRepository>());
        }
    }
}
