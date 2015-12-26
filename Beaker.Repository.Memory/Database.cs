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
using Beaker.Core.Medication;
using Beaker.Repository;

namespace Beaker.Repository.Memory
{
    public class Database : IMigratable
    {
        private IDictionary<Type, object> repositories = new Dictionary<Type, object>();
        private IDictionary<string, Migration> migrations = new Dictionary<string, Migration>();

        public Database()
        {
            IMedicationRepository medicationRepository = new MedicationRepository(new MedicationMemoryPersistentStore());
            this.repositories[typeof(IMedicationRepository)] = medicationRepository;
        }

        public void Apply(Migration migration)
        {
            if (this.HasMigration(migration.ID))
            {
                return;
            }

            this.migrations[migration.ID] = migration;

            migration.Apply(this);
        }

        public void CommitTransaction()
        {
            // do nothing
        }

        public bool HasMigration(string id)
        {
            return migrations.ContainsKey(id);
        }

        public T Repository<T>() where T : IRepository
        {
            return (T) this.repositories[typeof(T)];
        }

        public void RollbackTransaction()
        {
            // do nothing
        }

        public void StartTransaction()
        {
            // do nothing
        }
    }
}
