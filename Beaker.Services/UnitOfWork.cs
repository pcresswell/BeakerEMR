using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core;

namespace Beaker.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private User ActiveUser { get; set; }

        public UnitOfWork(User activeUser)
        {
            this.ActiveUser = activeUser;
        }

        public TPersistable Create<TPersistable>() where TPersistable : IPersistable, new()
        {
            throw new NotImplementedException();
        }

        public void Delete<TPersistable>(TPersistable persistable) where TPersistable : IPersistable
        {
            throw new NotImplementedException();
        }

        public void Save<TPersistable>(TPersistable persistable) where TPersistable : IPersistable
        {
            throw new NotImplementedException();
        }
    }
}
