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
        public void CreateAUserMustHaveAUsername()
        {
            Assert.Throws<ArgumentNullException>(()=>
            {
                ICommand addUser = new AddUserCommand(null);
                addUser.Run();
            });
        }

        [Test]
        public void CreateAUserMustHaveAnEmailAddress()
        {
            Assert.Throws<ArgumentNullException>(() => {
                ICommand addUser = new AddUserCommand(null) { Username = "Peter" };
            });
        }

        [Test]
        public void CreateAUserMustHaveAPassword()
        {
            Assert.Throws<ArgumentNullException>(() => 
            {
            ICommand addUser = new AddUserCommand(null) {
                Username = "Peter",
                EmailAddress = "pcresswell@gmail.com" };
            });
            
        }

        [Test]
        public void CreateAUserMustHaveAUnitOfWork()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ICommand addUser = new AddUserCommand(null)
                {
                    Username = "Peter",
                    EmailAddress = "pcresswell@gmail.com",
                    Password = "password"
                };
                addUser.Run();
            });
        }

        [Test]
        public void AddingAUserAsksTheUnitOfWorkForTheUser()
        {
            var moqUOW = new Mock<IUnitOfWork>();
            moqUOW.Setup(f => f.Create<User>()).Returns(() => new User());
            ICommand addUser = new AddUserCommand(moqUOW.Object)
            {
                Username = "Peter",
                EmailAddress = "pcresswell@gmail.com",
                Password = "password"
            };

            addUser.Run();
            moqUOW.Verify(f => f.Save<User>(It.Is<User>(
                u=> u.Username == "Peter" &&
                u.EmailAddress == "pcresswell@gmail.com" &&
                u.Password == "password")), Times.AtLeastOnce());
        }

        [Test]
        public void UsernameCannotBeRoot()
        {
            Assert.Throws<ArgumentException>(() => {
                var moq = new Mock<IUnitOfWork>();
                ICommand addUser = new AddUserCommand(moq.Object)
                {
                    Username = "root",
                    EmailAddress = "pcresswell@gmail.com",
                    Password = "password"
                };
                addUser.Run();
            });
        }

    }
}
