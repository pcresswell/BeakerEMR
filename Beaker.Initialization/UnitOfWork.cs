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
using System;
using Beaker.Repository;
using Beaker.Core;
using Beaker.Repository.SQLite;
using Beaker.Services;

namespace Beaker.Initialization
{
    class UnitOfWork : IUnitOfWork
    {
        private SQLiteDatabase Database { get; set; }
        private ITransactable TransactionManager {get;set;}

        public UnitOfWork(SQLiteDatabase database, User user)
        {
            this.Database = database;
            this.TransactionManager = database;
            this.SuperUser = user;
        }

        private User SuperUser { get; set; }


        #region IUnitOfWork implementation

        TPersistable IUnitOfWork.Create<TPersistable>()
        {
            return new TPersistable() { AuthorID = this.SuperUser.DomainObjectID };
        }

        void IUnitOfWork.Save<TPersistable>(TPersistable persistable)
        {
            persistable.AuthorID = this.SuperUser.DomainObjectID;
            this.Database.Save<TPersistable>(persistable);
        }

        void IUnitOfWork.Delete<TPersistable>(TPersistable persistable)
        {
            persistable.AuthorID = this.SuperUser.DomainObjectID;
            this.Database.Delete<TPersistable>(persistable);
        }

        #endregion

        #region ITransactable implementation

        void ITransactable.StartTransaction()
        {
            this.TransactionManager.StartTransaction();
        }

        void ITransactable.CommitTransaction()
        {
            this.TransactionManager.CommitTransaction();
        }

        void ITransactable.RollbackTransaction()
        {
            this.TransactionManager.RollbackTransaction();
        }

        #endregion
    }
}

