/*
The MIT License (MIT)

Copyright (c) 2015 Peter Cresswell (pcresswell@gmail.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Beaker.Repository;
using Beaker.Core;
using Beaker.Services;
using SQLite;
using Beaker.Repository.SQLite;

namespace Beaker.Test.Services
{
    [TestFixture]
    public class TestUserSession
    {
        private UserSession UserSession { get; set; }

        [Test]
        public void LoginFailsIfUserIsNotFound()
        {
            using (SQLiteDatabase db = new SQLiteDatabase(":memory:"))
            {
                SQLiteRepositoryFactory factory = new SQLiteRepositoryFactory();
                factory.RegisterRepositoriesWithDatabase(db, new TestPermissions(), new TestAuthor());
                this.UserSession = new UserSession(db);
                UserSession.Username = "Peter";
                UserSession.Password = "password";
                Assert.Throws<FailedToLoginException>(() => this.UserSession.Login());
            }

        }

        [Test]
        public void LoginFailsIfPasswordIsWrong()
        {
            using (SQLiteDatabase db = new SQLiteDatabase(":memory:"))
            {
                SQLiteRepositoryFactory factory = new SQLiteRepositoryFactory();
                factory.RegisterRepositoriesWithDatabase(db, new TestPermissions(), new TestAuthor());
                this.UserSession = new UserSession(db);
                db.Save<User>(new User() { Username = "Peter", Password = "something" });
                UserSession.Username = "Peter";
                UserSession.Password = "password";
                Assert.Throws<FailedToLoginException>(() => this.UserSession.Login());
            }
        }

        [Test]
        public void ExceptionThrownIfUserNotLoggedInWhenGettingUnitOfWork()
        {
            using (SQLiteDatabase db = new SQLiteDatabase(":memory:"))
            {
                Assert.Throws<InvalidOperationException>(() =>
                    {
                        SQLiteRepositoryFactory factory = new SQLiteRepositoryFactory();
                        factory.RegisterRepositoriesWithDatabase(db, new TestPermissions(), new TestAuthor());
                        this.UserSession = new UserSession(db);
                        this.UserSession.GetUnitOfWork();
                    });
            }
        }

        [Test]
        public void ConstructorThrowsExceptionIfNullRepositoryPassedIn()
        {
            Assert.Throws<ArgumentNullException>(() =>
                {
                    this.UserSession = new UserSession(null);
                });
        }
    }
}
