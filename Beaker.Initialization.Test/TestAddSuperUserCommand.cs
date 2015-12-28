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

using System;
using Beaker.Initialization;
using NUnit.Framework;
using Moq;
using Beaker.Core;
using Beaker.Services;
using Beaker.Authorize;

namespace Beaker.Initialization.Test
{
    [TestFixture]
    public class TestAddSuperUserCommand
    {
        private Mock<IUnitOfWork> moq;

        [SetUp]
        public void Setup()
        {
            this.moq = new Mock<IUnitOfWork>();
            moq.Setup(f => f.Create<User>()).Returns(() => new User());
            moq.Setup(f => f.Create<UserPermission>()).Returns(() => new UserPermission());
            moq.Setup(f => f.Save<User>(It.Is<User>(u => u.Username == "root")));

        }
        [Test]
        public void AddSuperUserCommand()
        {
            var addUser = new AddSuperUserCommand("pcresswell@gmail.com", "password", moq.Object);
            addUser.Run();
            moq.Verify(u => u.Save<User>(It.IsAny<User>()), Times.AtLeastOnce());
        }
        
        [Test]
        public void SuperUserCanManageEverything()
        {
            var addUser = new AddSuperUserCommand("pcresswell@gmail.com", "password", moq.Object);
            addUser.Run();
            moq.Verify(u => u.Save<UserPermission>(It.Is<UserPermission>(up => up.Can(Actions.Manage,typeof(object)) == true)), Times.AtLeastOnce());
            moq.Verify(u => u.Save<User>(It.Is<User>(us => us.EmailAddress == "pcresswell@gmail.com" && us.Password == "password")), Times.AtLeastOnce());
        }
    }
}
