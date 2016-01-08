// 
// The MIT License (MIT)
//
// Copyright (c) 2015 Peter Cresswell (pcresswell@gmail.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 

namespace Beaker.Initialization
{
    using System;
    using Beaker.Repository.SQLite;
    using Beaker.Repository;
    using Beaker.Core;
    using Beaker.Services;

    /// <summary>
    /// Initialize system on first install.
    /// </summary>
    public class Initialize
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Initialization.Initialize"/> class.
        /// </summary>
        /// <param name="database">Database.</param>
        /// <param name="emailAddress">Email address of super user</param>
        /// <param name="password">Password for super user.</param>
        /// <param name="session">Session.</param>
        public Initialize(SQLiteDatabase database, string emailAddress, string password, UserSession session)
        {
            this.Database = database;
            this.EmailAddress = emailAddress;
            this.Password = password;
            this.Session = session;
            // Create a super user. This is the system's master user.
            this.SuperUser = new User() { EmailAddress = this.EmailAddress, Password = this.Password, Username = "root" };
            // Author is itself.
            this.SuperUser.AuthorID = this.SuperUser.DomainObjectID;

            this.UnitOfWork = new Beaker.Initialization.UnitOfWork(this.Database, this.SuperUser);
        }

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>The database.</value>
        private SQLiteDatabase Database { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        private string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        private string Password { get; set; }

        /// <summary>
        /// Gets or sets the unit of work.
        /// </summary>
        /// <value>The unit of work.</value>
        private IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// Gets or sets the super user.
        /// </summary>
        /// <value>The super user.</value>
        private User SuperUser { get; set; }

        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>The session.</value>
        private UserSession Session { get; set; }

        /// <summary>
        /// Run this instance.
        /// </summary>
        public void Run()
        {
            // First, initialize the database. This adds repositories and creates initial tables.
            InitializeSQLiteDatabase sqlDatabase = new InitializeSQLiteDatabase(this.Database, this.Session, this.Session);
            sqlDatabase.Run();

            // Next, save the super user.
            ((ISQLiteDatabase)this.Database).Save<User>(this.SuperUser);

            // now, log in as the super user.
            this.Session.Login(this.SuperUser);

            // now, install initial medications
            AddInitialMedication medications = new AddInitialMedication(this.Database);
            medications.Run();
        }
    }
}

