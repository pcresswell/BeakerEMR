using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core;
using Beaker.Repository.SQLite;
using SQLite;
using Beaker.Core.Medication;
using Beaker.Repository.SQLite.Tables.Medication;
using AutoMapper;
using Beaker.Test;
using Beaker.Core.Authorize;

namespace Beaker.Repository.SQLite.Test
{
    [TestFixture]
    public class TestUserRepository : TestHelper
    {

        [Test]
        public void SaveAUser()
        {
            using (SQLiteDatabase database = new SQLiteDatabase(":memory:"))
            {
                Factory.RegisterRepositoriesWithDatabase(database);

                User user = new User();
                user.EmailAddress = "pcresswell@gmail.com";
                user.Password = "something";
                user.Username = "Peter";

                Permission permission = new Permission();
                Read read = new Read();
                read.AddSubject(typeof(Patient));
                permission.AddAuthorization(read);
                Assert.IsTrue(permission.Can(Actions.Read, typeof(Patient)) == true);

                user.Permission = permission;

                database.Save<User>(user);

                var peter = database.Queries<IUserQueries>().FindByUsername("Peter");

                Assert.AreEqual(user.EmailAddress, peter.EmailAddress);
                Assert.IsTrue(peter.Permission.Can(Actions.Read, typeof(Patient)) == true);

            }

        }
    }
}
