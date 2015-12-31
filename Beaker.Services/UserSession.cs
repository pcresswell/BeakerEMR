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
using Beaker.Repository;

namespace Beaker.Services
{
    /// <summary>
    /// A User Session handles logging in and out of the system
    /// as well as providing a unit of work interface.
    /// </summary>
    public class UserSession
    {
        /// <summary>
        /// User's username.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// User's password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Is there a user logged in?
        /// </summary>
        public bool IsLoggedIn
        {
            get
            {
                return null != this.ActiveUser;
            }
        }

        private IUserRepository UserRepository { get; set; }
        private User ActiveUser { get; set; }
        private UnitOfWork UnitOfWork { get; set; }
        
        /// <summary>
        /// Creates a new UserSession.
        /// </summary>
        /// <param name="userRepository"></param>
        public UserSession(IUserRepository userRepository)
        {
            if (null == userRepository) throw new ArgumentNullException("userRepository");
            this.UserRepository = userRepository;  
        }

        /// <summary>
        /// Get a unit of work.
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork GetUnitOfWork()
        {
            if (null == this.UnitOfWork) throw new InvalidOperationException("Cannot get a UnitOfWork without a logged in User");

            return this.UnitOfWork;
        }

        /// <summary>
        /// Log into the system. Throws FailedToLoginException is
        /// the login fails.
        /// </summary>
        public void Login()
        {
            User user = this.UserRepository.FindByUsername(this.Username);

            if (null == user) throw new FailedToLoginException("User not found");
            if (user.Password != this.Password) throw new FailedToLoginException("Password does not match.");

            this.ActiveUser = user;
            this.UnitOfWork = new UnitOfWork(this.ActiveUser);
        }
    }

    /// <summary>
    /// Failed to login Exception. Thrown when login fails.
    /// </summary>
    public class FailedToLoginException : Exception
    {
        public FailedToLoginException() { }
        public FailedToLoginException(string message) : base(message) { }
        public FailedToLoginException(string message, Exception inner) : base(message, inner) { }
        
    }
}
