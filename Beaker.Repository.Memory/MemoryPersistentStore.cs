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

namespace Beaker.Repository.Memory
{
    /// <summary>
    /// A persistent storage. In memory only for testing purposes. Adds a row for each persist call.
    /// </summary>
    /// <typeparam name="TPersistable"></typeparam>
    public abstract class MemoryPersistentStore<TPersistable> : IPersistentStore<TPersistable> where TPersistable : IPersistable, new()
    {
        internal IDictionary<int, TPersistable> PersistedList { get; set; }
           
        public MemoryPersistentStore()
        {
            this.PersistedList = new Dictionary<int, TPersistable>();
        }

        private object persistentListLock = new object();

        public void Persist(TPersistable persistable)
        {
            lock (persistentListLock)
            {
                TPersistable p = new TPersistable();
               
                p.ID = persistable.ID;
                p.DomainObjectID = persistable.DomainObjectID;
                p.RecordEndDateTime = persistable.RecordEndDateTime;
                p.RecordStartDateTime = persistable.RecordStartDateTime;
                p.ValidEndDateTime = persistable.ValidEndDateTime;
                p.ValidStartDateTime = persistable.ValidStartDateTime;

                // Copy the data from persistable to p
                this.Copy(persistable, p);
                this.PersistedList[p.ID] = p;
            }
        }

        public int Count
        {
            get
            {
                lock (this.persistentListLock)
                {
                    return this.PersistedList.Count;
                }
            }
        }

        public bool IsPersisted(TPersistable persistable)
        {
            return this.PersistedList.ContainsKey(persistable.ID);
        }

        public void Clear()
        {
            lock (this.persistentListLock)
            {
                this.PersistedList.Clear();
            }
        }

        public TPersistable Get(int id)
        {
            lock (this.persistentListLock)
            {
                return this.PersistedList[id];
            }
        }

        public void PersistWithNewID(TPersistable persistable)
        {
            persistable.ID = this.GetNextID();
            this.Persist(persistable);
        }

        public TPersistable Find(Guid entityID, DateTime dateTime)
        {
            lock (this.persistentListLock)
            {
                foreach (var p in this.PersistedList.Values)
                {
                    if (p.RecordEndDateTime > dateTime && p.RecordStartDateTime < dateTime)
                    {
                        return p;
                    }
                }
            }

            return default(TPersistable);
        }

        public TPersistable Find(Guid entityID)
        {
            lock (this.persistentListLock)
            {
                foreach (var p in this.PersistedList.Values)
                {
                    if (Beaker.Core.Dates.Infinity.Equals(p.RecordEndDateTime))
                    {
                        return p;
                    }
                }
            }

            return default(TPersistable);
        }

        /// <summary>
        /// Copies data from the source object to the destination object.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        protected abstract void Copy(TPersistable source, TPersistable destination);

        private int GetNextID()
        {
            return this.Count + 1;
        }

        
       
    }
}
