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
using Beaker.Core;
using Beaker.Core.Medication;
using AutoMapper;
using Beaker.Core.Authorize;

namespace Beaker.Repository.SQLite
{
    public class SQLiteRepositoryFactory
    {
        public IEnumerable<IRepository> RegisterRepositoriesWithDatabase(SQLiteDatabase database)
        {
            IList<IRepository> repositories = new List<IRepository>();

            var personRepo = new PersonRepository();
            this.RegisterWithDatabase<IPersonRepository, Person>(database, personRepo);
            repositories.Add(personRepo);

            var medicationRepo = new MedicationRepository();
            this.RegisterWithDatabase<IMedicationRepository, Medication>(database, medicationRepo);
            repositories.Add(medicationRepo);

            var patientRepo = new PatientRepository(){ PersonRepository = personRepo };
            this.RegisterWithDatabase<IPatientRepository, Patient>(database, patientRepo);
            repositories.Add(patientRepo);

            var userRepo = new UserRepository();
            this.RegisterWithDatabase<IUserRepository, User>(database, userRepo);
            repositories.Add(userRepo);

            var userPermissionRepo = new PermissionRepository();
            userRepo.PermissionRepository = userPermissionRepo;
            this.RegisterWithDatabase<IPermissionRepository, Permission>(database, userPermissionRepo);
            repositories.Add(userPermissionRepo);

            Mapper.Initialize(cfg =>
                {
                    personRepo.Initialize(cfg);
                    medicationRepo.Initialize(cfg);
                    patientRepo.Initialize(cfg);
                    userRepo.Initialize(cfg);
                    userPermissionRepo.Initialize(cfg);
                });
            
            return repositories;
        }

        public SQLiteRepositoryFactory()
        {
        }

        private void RegisterWithDatabase<TRepository, TPersistable>(IRepositoryRegistrar registrar, TRepository repo) where TRepository: IRepository where TPersistable : IPersistable
        {
            registrar.RegisterRepository<TRepository, TPersistable>(repo);
        }
    }

}

