using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core;
using Beaker.Repository;

namespace Beaker.Repository.Memory
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private object persistentListLock = new object();

        private IDictionary<Guid, Patient> PersistentList { get; set; }

        public PatientRepository()
        {
            this.PersistentList = new Dictionary<Guid, Patient>();
        }

        public override int Count
        {
            get
            {
                return this.PersistentList.Count;
            }
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override bool IsPersisted(Patient persistable)
        {
            return this.PersistentList.ContainsKey(persistable.ID);
        }

        protected override Patient Find(Guid domainObjectID, DateTime onDateTime)
        {
            lock (this.persistentListLock)
            {
                foreach (var p in this.PersistentList.Values)
                {
                    if (onDateTime <= p.RecordEndDateTime && onDateTime >= p.RecordStartDateTime && p.DomainObjectID == domainObjectID)
                    {
                        return p;
                    }
                }
            }

            return default(Patient);
        }

        protected override Patient Get(Guid id)
        {
            lock (this.persistentListLock)
            {
                if (this.PersistentList[id] == null)
                {
                    return default(Patient);
                }
                else
                {
                    return this.PersistentList[id];
                }
            }
        }

        protected override void Insert(Patient persistable)
        {
            throw new NotImplementedException();
        }

        protected override void Update(Patient persistable)
        {
            throw new NotImplementedException();
        }
    }
}
