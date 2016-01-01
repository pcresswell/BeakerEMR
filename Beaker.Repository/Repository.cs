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
    public abstract class Repository<TPersistable> : IRepository<TPersistable> where TPersistable : IPersistable
    {
        public Repository()
        {
            
        }

        public abstract int Count
        {
            get;           
        }

        /// <summary>
        /// Returns true if the object is different from the previous version
        /// persisted to disk. Returns false otherwise.
        /// </summary>
        /// <param name="persistable">The persistable object to compare to disk.</param>
        /// <returns></returns>
        public virtual bool IsChanged(TPersistable persistable)
        {
            if (!this.IsPersisted(persistable))
            {
                // If it's not saved, then it's different.
                return true;
            }

            TPersistable otherPersistable = this.Get(persistable.ID);
            return (!otherPersistable.SameAs(persistable));
        }

        public abstract bool IsPersisted(TPersistable persistable);
     
        /// <summary>
        /// Deletes the persistable record. Override if more than one record needs to be deleted
        /// in the event of a delete.
        /// </summary>
        /// <param name="persistable"></param>
        protected virtual void Delete(TPersistable persistable)
        {
            persistable.ValidEndDateTime = DateTime.UtcNow;
            persistable.RecordEndDateTime = DateTime.UtcNow;
            if (this.IsPersisted(persistable))
            {
                this.Update(persistable);
            }
        }

        protected abstract TPersistable Find(Guid domainObjectID, DateTime onDateTime);

        // protected abstract void Persist(TPersistable persistable);

        protected abstract void Insert(TPersistable persistable);

        protected abstract void Update(TPersistable persistable);

        /// <summary>
        /// Save the persistable object. Inserts new record if there are any changes.
        /// </summary>
        /// <param name="persistable"></param>
        void IRepository<TPersistable>.Save(TPersistable persistable)
        {
            
            // First, discontinue the current version
            var currentVersion = this.Find(persistable.DomainObjectID, Beaker.Core.Dates.Infinity);
            if (currentVersion != null)
            {
                // Discontinue the current version
                // TODO: Consider the sitation of doing a sync from another system. This 
                // will need to change here as the Valid date time will NOT be set to now.
                this.Delete(currentVersion);
            }

            persistable.RecordStartDateTime = DateTime.UtcNow;
            persistable.ValidStartDateTime = DateTime.UtcNow;
            persistable.RecordEndDateTime = Beaker.Core.Dates.Infinity;
            persistable.ValidEndDateTime = Beaker.Core.Dates.Infinity;
            persistable.ID = Guid.NewGuid();
            this.Insert(persistable);
        }

        /// <summary>
        /// Delete the persistable object.
        /// </summary>
        /// <param name="persistable"></param>
        void IRepository<TPersistable>.Delete(TPersistable persistable)
        {
            if (Beaker.Core.Dates.Infinity.Equals(persistable.ValidEndDateTime) || DateTime.MinValue.Equals(persistable.ValidEndDateTime))
            {
                this.Delete(persistable);
            }
        }

        /// <summary>
        /// Find the current version of the entity.
        /// </summary>
        /// <param name="entityID">The entity id.</param>
        /// <returns></returns>
        TPersistable IRepository<TPersistable>.Find(Guid entityID)
        {
            return this.Find(entityID, Beaker.Core.Dates.Infinity);
        }

        /// <summary>
        /// Find the version of the entity as it was on the given date and time.
        /// </summary>
        /// <param name="entityID">The entity id.</param>
        /// <param name="onDateTime">The date and time for which the record was valid.</param>
        /// <returns></returns>
        TPersistable IRepository<TPersistable>.Find(Guid domainObjectID, DateTime onDateTime)
        {
            return this.Find(domainObjectID, onDateTime.ToUniversalTime());
        }

        public abstract void Initialize();

        /// <summary>
        /// Retrieve the object with the given ID from persistence.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        abstract protected TPersistable Get(Guid id);

        /// <summary>
        /// Register this repository against the registrar.
        /// </summary>
        /// <param name="registrar"></param>
        public abstract void Register(IRepositoryRegistrar registrar);
        
    }
}
