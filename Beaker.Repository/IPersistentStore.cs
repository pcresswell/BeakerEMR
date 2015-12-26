using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core;

namespace Beaker.Repository
{
    /// <summary>
    /// Any storage system must implement this interface to persist objects to disk.
    /// </summary>
    public interface IPersistentStore<TPersistable> where TPersistable : IPersistable
    {
        int Count {
            get;   
        }

        /// <summary>
        /// Persist the object to disk
        /// </summary>
        /// <param name="persistable"></param>
        void Persist(TPersistable persistable);
        
        /// <summary>
        /// Persist the object to disk
        /// </summary>
        /// <param name="persistable"></param>
        void PersistWithNewID(TPersistable persistable);

        /// <summary>
        /// Is the persistable object saved to disk.
        /// </summary>
        /// <param name="persistable"></param>
        /// <returns></returns>
        bool IsPersisted(TPersistable persistable);

        /// <summary>
        /// Find the version of the entity as it would be as of the dateTime
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="asOfDateTime"></param>
        /// <returns></returns>
        TPersistable Find(Guid entityID, DateTime asOfDateTime);

        /// <summary>
        /// Find the current version of the entity.
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        TPersistable Find(Guid entityID);
    }
}
