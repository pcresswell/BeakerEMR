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
using Beaker.Core.Authorize;

namespace Beaker.Repository
{
    public class AuthorizedDatabaseAdaptor : IDatabase
    {
        public AuthorizedDatabaseAdaptor(IDatabase database, ICan can)
        {
            this.Database = database;
            this.Can = can;
        }

        private IDatabase Database { get; set; }

        private ICan Can{ get; set; }

        #region IDatabase implementation

        void IDatabase.Save<TPersistable>(TPersistable persistable)
        {
            if (this.Can.Can(Actions.Update, persistable) != true)
            {
                throw new UnauthorizedAccessException("Cannot save the given object as permission is not granted for this action");
            }

            this.Database.Save<TPersistable>(persistable);
        }

        void IDatabase.Delete<TPersistable>(TPersistable persistable)
        {
            if (this.Can.Can(Actions.Delete, persistable) != true)
            {
                throw new UnauthorizedAccessException("Cannot delete the given object as permission is not granted for this action");
            }

            this.Database.Delete<TPersistable>(persistable);
        }

//        TPersistable IDatabase.Find<TPersistable>(Guid domainObjectID)
//        {
//            var persistable =  this.Database.Find<TPersistable>(domainObjectID);
//            if (this.Can.Can(Actions.Read, persistable) != true)
//            {
//                throw new UnauthorizedAccessException("Cannot read the given object as permission is not granted for this action");
//            }
//
//            return persistable;
//        }

//        TPersistable IDatabase.Find<TPersistable>(Guid domainObjectID, DateTime onDateTime)
//        {
//
//            var persistable =  this.Database.Find<TPersistable>(domainObjectID, onDateTime);
//            if (this.Can.Can(Actions.Read, persistable) != true)
//            {
//                throw new UnauthorizedAccessException("Cannot read the given object as permission is not granted for this action");
//            }
//
//            return persistable;
//        }

        bool IDatabase.IsPersisted<TPersistable>(TPersistable persistable)
        {
            if (this.Can.Can(Actions.Read, persistable) != true)
            {
                throw new UnauthorizedAccessException("Cannot read the given object as permission is not granted for this action");
            }

            return this.Database.IsPersisted(persistable);
        }

//        TPersistable IDatabase.Get<TPersistable>(Guid id)
//        {
//            var persistable =  this.Database.Get<TPersistable>(id);
//
//            if (this.Can.Can(Actions.Read, persistable) != true)
//            {
//                throw new UnauthorizedAccessException("Cannot read the given object as permission is not granted for this action");
//            }
//
//            return persistable;
//        }

        #endregion
    }
}

