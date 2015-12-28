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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core;


namespace Beaker.Services.Commands
{
    public class AddUserCommand
    {
        private string Username { get; set; }
        private string EmailAddress { get; set; }
        private string Password { get; set; }
        private IUnitOfWork UnitOfWork { get; set; }

        public AddUserCommand(string username, string emailAddress, string password, IUnitOfWork unitOfWork)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("username");
            if (string.IsNullOrEmpty(emailAddress)) throw new ArgumentNullException("emailAddress");
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password");
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            if ("root".Equals(username)) throw new ArgumentException("Username cannot be root as this is a reserved username");

            this.Username = username;
            this.EmailAddress = emailAddress;
            this.Password = password;
            this.UnitOfWork = unitOfWork;
        }

        public void Run()
        {
            User user = UnitOfWork.Create<User>();
            user.Username = this.Username;
            user.EmailAddress = this.EmailAddress;
            user.Password = this.Password;
            UnitOfWork.Save<User>(user);
        }
    }
}
