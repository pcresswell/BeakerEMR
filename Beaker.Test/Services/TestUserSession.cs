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

namespace Beaker.Services.Test
{
    [TestFixture]
    public class TestUserSession
    {
        private UserSession UserSession { get; set; }

        [Test]
        public void LoginFailsIfUserIsNotFound()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(m => m.FindByUsername(It.IsAny<string>())).Returns<User>(null);
            this.UserSession = new UserSession(mock.Object);
            UserSession.Username = "Peter";
            UserSession.Password = "password";
            Assert.Throws<FailedToLoginException>(() => this.UserSession.Login());
        }

        [Test]
        public void LoginFailsIfPasswordIsWrong()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(m => m.FindByUsername(It.IsAny<string>())).Returns(new User() { Username = "Peter", Password = "something" });
            this.UserSession = new UserSession(mock.Object);
            UserSession.Username = "Peter";
            UserSession.Password = "password";
            Assert.Throws<FailedToLoginException>(() => this.UserSession.Login());
        }

        [Test]
        public void ExceptionThrownIfUserNotLoggedInWhenGettingUnitOfWork()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var mock = new Mock<IUserRepository>();
                this.UserSession = new UserSession(mock.Object);
                this.UserSession.GetUnitOfWork();
            });
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
