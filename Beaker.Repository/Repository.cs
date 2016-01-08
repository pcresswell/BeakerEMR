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


namespace Beaker.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Beaker.Core;
    using Beaker.Core.Authorize;

    /// <summary>
    /// Repository base class. All repositories must be a sub class.
    /// </summary>
    public abstract class Repository<TPersistable> :  IRepository<TPersistable> where TPersistable : IPersistable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Repository.Repository`1"/> class.
        /// </summary>
        public Repository()
        {
            ((ITransactionTimestamp)this).TransactionDateTime = DateTime.MinValue;
        }


        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
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

        /// <summary>
        /// Determines whether this instance is persisted.
        /// </summary>
        /// <returns><c>true</c> if this instance is persisted the specified persistable; otherwise, <c>false</c>.</returns>
        /// <param name="persistable">Persistable.</param>
        public abstract bool IsPersisted(TPersistable persistable);

        /// <summary>
        /// Deletes the persistable record. Override if more than one record needs to be deleted
        /// in the event of a delete.
        /// </summary>
        /// <param name="persistable"></param>
        protected virtual void Delete(TPersistable persistable)
        {
            if (DateTime.MinValue.Equals(((ITransactionTimestamp)this).TransactionDateTime))
            {
                persistable.RecordEndDateTime = DateTime.UtcNow;
                persistable.ValidEndDateTime = DateTime.UtcNow;
            }
            else
            {
                persistable.RecordEndDateTime = ((ITransactionTimestamp)this).TransactionDateTime;
                persistable.ValidEndDateTime = ((ITransactionTimestamp)this).TransactionDateTime;
            }

            if (this.IsPersisted(persistable))
            {
                this.Update(persistable);
            }
        }

        /// <summary>
        /// Find the specified domainObjectID on the given DateTime.
        /// </summary>
        /// <param name="domainObjectID">Domain object I.</param>
        /// <param name="onDateTime">On date time.</param>
        public abstract TPersistable Find(Guid domainObjectID, DateTime onDateTime);

        /// <summary>
        /// Insert the specified persistable.
        /// </summary>
        /// <param name="persistable">Persistable.</param>
        protected abstract void Insert(TPersistable persistable);

        /// <summary>
        /// Update the specified persistable.
        /// </summary>
        /// <param name="persistable">Persistable.</param>
        protected abstract void Update(TPersistable persistable);

        /// <summary>
        /// Save the persistable object. Inserts new record if there are any changes.
        /// </summary>
        /// <param name="persistable"></param>
        void IPersister<TPersistable>.Save(TPersistable persistable)
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

            this.Timestamp(persistable);

            persistable.RecordEndDateTime = Beaker.Core.Dates.Infinity;
            persistable.ValidEndDateTime = Beaker.Core.Dates.Infinity;
            persistable.ID = Guid.NewGuid();
            this.Insert(persistable);
        }

        /// <summary>
        /// Delete the persistable object.
        /// </summary>
        /// <param name="persistable"></param>
        void IPersister<TPersistable>.Delete(TPersistable persistable)
        {
            if (Beaker.Core.Dates.Infinity.Equals(persistable.ValidEndDateTime) || DateTime.MinValue.Equals(persistable.ValidEndDateTime))
            {
                this.Delete(persistable);
            }
        }


        public TPersistable Find(Guid entityID)
        {
            return this.Find(entityID, Beaker.Core.Dates.Infinity);
        }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Retrieve the object with the given ID from persistence.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract TPersistable Get(Guid id);

        /// <summary>
        /// Register this repository against the registrar.
        /// </summary>
        /// <param name="registrar"></param>
        public abstract void Register(IRepositoryRegistrar registrar);

        DateTime ITransactionTimestamp.TransactionDateTime
        {
            get;
            set;
        }

        private void Timestamp(TPersistable persistable)
        {
            if (DateTime.MinValue.Equals(((ITransactionTimestamp)this).TransactionDateTime))
            {
                persistable.RecordStartDateTime = DateTime.UtcNow;
                persistable.ValidStartDateTime = DateTime.UtcNow;
            }
            else
            {
                persistable.RecordStartDateTime = ((ITransactionTimestamp)this).TransactionDateTime;
                persistable.ValidStartDateTime = ((ITransactionTimestamp)this).TransactionDateTime;
            }
        }
    }
}
