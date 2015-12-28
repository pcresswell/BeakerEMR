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
using NUnit.Framework;
using Beaker.Services.Commands;
using Beaker.Core;
using Moq;

namespace Beaker.Services.Test
{
    [TestFixture]
    public class TestAddUserCommand
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAUserMustHaveAUsername()
        {
            AddUserCommand addUser = new AddUserCommand(string.Empty, string.Empty, string.Empty, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAUserMustHaveAnEmailAddress()
        {
            AddUserCommand addUser = new AddUserCommand("Peter", string.Empty, string.Empty, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAUserMustHaveAPassword()
        {
            AddUserCommand addUser = new AddUserCommand("Peter", "pcresswell@gmail.com", string.Empty, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateAUserMustHaveAUnitOfWork()
        {
            AddUserCommand addUser = new AddUserCommand("Peter", "pcresswell@gmail.com", "password", null);
        }

        [Test]
        public void AddingAUserAsksTheUnitOfWorkForTheUser()
        {
            var moqUOW = new Mock<IUnitOfWork>();
            moqUOW.Setup(f => f.Create<User>()).Returns(() => new User());
            AddUserCommand addUser = new AddUserCommand("Peter", "pcresswell@gmail.com", "password", moqUOW.Object);
            addUser.Run();
            moqUOW.Verify(f => f.Save<User>(It.Is<User>(
                u=> u.Username == "Peter" &&
                u.EmailAddress == "pcresswell@gmail.com" &&
                u.Password == "password")), Times.AtLeastOnce());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void UsernameCannotBeRoot()
        {
            var moq = new Mock<IUnitOfWork>();
            AddUserCommand addUser = new AddUserCommand("root", "something@somewhere.com", "password",moq.Object );
        }

    }
}
