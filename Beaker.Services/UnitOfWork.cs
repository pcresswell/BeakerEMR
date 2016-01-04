// /*
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
// */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core;
using Beaker.Authorize;
using Beaker.Repository;

namespace Beaker.Services
{
    /// <summary>
    /// Unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Services.UnitOfWork"/> class.
        /// </summary>
        /// <param name="userPermissions">User permissions.</param>
        /// <param name="database">Database.</param>
        public UnitOfWork(User currentUser, ICan userPermissions, Database database)
        {
            this.UserPermissions = userPermissions;
            this.Database = database;
            this.TransactionManager = database;
            this.Author = currentUser;
        }

        /// <summary>
        /// Gets or sets the user permissions.
        /// </summary>
        /// <value>The user permissions.</value>
        private ICan UserPermissions { get; set; }

        /// <summary>
        /// The author of all actions.
        /// </summary>
        /// <value>The author.</value>
        private User Author { get; set;}

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>The database.</value>
        private Database Database {get;set;}

        /// <summary>
        /// Gets or sets the transaction manager.
        /// </summary>
        /// <value>The transaction manager.</value>
        private ITransactable TransactionManager {get;set;}

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Beaker.Services.UnitOfWork"/> in transaction.
        /// </summary>
        /// <value><c>true</c> if in transaction; otherwise, <c>false</c>.</value>
        private bool InTransaction { get; set;}

        /// <summary>
        /// The transaction lock.
        /// </summary>
        private object transactionLock = new object();

        /// <summary>
        /// Create an instance.
        /// </summary>
        /// <typeparam name="TPersistable">The 1st type parameter.</typeparam>
        public TPersistable Create<TPersistable>() where TPersistable : IPersistable, new()
        {
            if (this.UserPermissions.Can(Actions.Create,typeof(TPersistable)) != true)
            {
                throw new ActionNotPermittedException("Cannot create the requested object as the user does not have permissions to do this action");
            }
                
            return new TPersistable() { AuthorID = this.Author.DomainObjectID };
        }

        /// <summary>
        /// Starts the transaction.
        /// </summary>
        public void StartTransaction()
        {
            lock (this.transactionLock)
            {
                if (this.InTransaction)
                {
                    return;
                }

                this.TransactionManager.StartTransaction();
                this.InTransaction = true;
            }

        }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public void CommitTransaction()
        {
            lock (this.transactionLock)
            {
                if (!this.InTransaction)
                {
                    return;
                }

                this.TransactionManager.CommitTransaction();
                this.InTransaction = false;
            }
        }

        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            lock (this.transactionLock)
            {
                if (!this.InTransaction)
                {
                    return;
                }

                this.TransactionManager.RollbackTransaction();
                this.InTransaction = false;
            }
        }

        /// <summary>
        /// Delete the specified persistable.
        /// </summary>
        /// <param name="persistable">Persistable.</param>
        /// <typeparam name="TPersistable">The 1st type parameter.</typeparam>
        public void Delete<TPersistable>(TPersistable persistable) where TPersistable : IPersistable
        {
            persistable.AuthorID = this.Author.DomainObjectID;
            this.Database.Delete<TPersistable>(persistable);
        }

        /// <summary>
        /// Save the specified persistable.
        /// </summary>
        /// <param name="persistable">Persistable.</param>
        /// <typeparam name="TPersistable">The 1st type parameter.</typeparam>
        public void Save<TPersistable>(TPersistable persistable) where TPersistable : IPersistable
        {
            persistable.AuthorID = this.Author.DomainObjectID;
            this.Database.Save<TPersistable>(persistable);
        }
    }
}