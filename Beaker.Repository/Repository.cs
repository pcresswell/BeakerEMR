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

namespace Beaker.Repository
{
    public class Repository<TPersistable> : IRepository<TPersistable> where TPersistable : IPersistable
    {
        public Repository(IPersistentStore<TPersistable> store)
        {
            this.Store = store;
        }

        /// <summary>
        /// The storage facility that persists and retrieves the object to disk.
        /// </summary>
        protected IPersistentStore<TPersistable> Store { get; set; }

        public int Count
        {
            get
            {
                return this.Store.Count;
            }
        }

        public void Save(TPersistable persistable)
        {
            // First, discontinue the current version
            var currentVersion = this.Find(persistable.DomainObjectID);
            if (currentVersion != null)
            {
                // There is a current version
                this.Discontinue(currentVersion);
            }

            persistable.RecordStartDateTime = DateTime.UtcNow;
            persistable.ValidStartDateTime = DateTime.UtcNow;
            persistable.RecordEndDateTime = Beaker.Core.Dates.Infinity;
            persistable.ValidEndDateTime = Beaker.Core.Dates.Infinity;
            this.Store.PersistWithNewID(persistable);
        }

        public void Delete(TPersistable persistable)
        {
            if (Beaker.Core.Dates.Infinity.Equals(persistable.ValidEndDateTime) || DateTime.MinValue.Equals(persistable.ValidEndDateTime))
            {
                Discontinue(persistable);
            }
        }
 
        /// <summary>
        /// Find the current version of the entity.
        /// </summary>
        /// <param name="entityID">The entity id.</param>
        /// <returns></returns>
        public TPersistable Find(Guid entityID)
        {
            return this.Store.Find(entityID);
        }

        /// <summary>
        /// Find the version of the entity which was valid at the given date and time.
        /// </summary>
        /// <param name="entityID">The entity id.</param>
        /// <param name="onDateTime">The date and time for which the record was valid.</param>
        /// <returns></returns>
        public TPersistable Find(Guid entityID, DateTime onDateTime)
        {
            return this.Store.Find(entityID, onDateTime.ToUniversalTime());
        }

        public bool IsPersisted(TPersistable persistable)
        {
            return this.Store.IsPersisted(persistable);
        }

        private void Discontinue(TPersistable persistable)
        {
            persistable.ValidEndDateTime = DateTime.UtcNow;
            persistable.RecordEndDateTime = DateTime.UtcNow;
            this.Store.Persist(persistable);
        }
    }
}
