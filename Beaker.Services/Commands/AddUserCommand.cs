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
    public class AddUserCommand : Command
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    

        public AddUserCommand(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override void Run()
        {
            if (string.IsNullOrEmpty(Username)) throw new ArgumentNullException("username");
            if (string.IsNullOrEmpty(EmailAddress)) throw new ArgumentNullException("emailAddress");
            if (string.IsNullOrEmpty(Password)) throw new ArgumentNullException("password");
            if ("root".Equals(Username)) throw new ArgumentException("Username cannot be root as this is a reserved username");

            User user = UnitOfWork.Create<User>();
            user.Username = this.Username;
            user.EmailAddress = this.EmailAddress;
            user.Password = this.Password;
            UnitOfWork.Save<User>(user);
        }
    }
}
