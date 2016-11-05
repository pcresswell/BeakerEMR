using System;
using System.Collections.Generic;
using Beaker.Core;
using Beaker.Data;

namespace Beaker.Module.Common.Test
{
	public class RepositoryRegistry : IRepositoryDictionary
	{
		internal RepositoryRegistry()
		{
			this.Repositories = new Dictionary<Type, object>();
		}

		private IDictionary<Type, object> Repositories { get; set; }

		internal void Register<TDomain>(GenericRepository<TDomain> repository) where TDomain : DomainObject
		{
			this.Repositories[typeof(TDomain)] = repository;
		}

		internal GenericRepository<TDomain> GetRepository<TDomain>() where TDomain : DomainObject
		{
			return (Beaker.Core.GenericRepository<TDomain>)this.Repositories[typeof(TDomain)];
		}

		GenericRepository<TDomain> IRepositoryIndex.GetRepository<TDomain>()
		{
			return this.GetRepository<TDomain>();
		}

		public void Initialize()
		{
			foreach (var repo in this.Repositories.Values)
			{
				IInitializeable initializeableRepo = (IInitializeable)repo;
				initializeableRepo.Initialize();
			}
		}

		void IRepositoryRegistry.AddRepository<TDomain>(GenericRepository<TDomain> repository)
		{
			this.Register<TDomain>(repository);
		}

		IRepositoryDictionary IForkable<IRepositoryDictionary>.Fork(Guid sessionId)
		{
			throw new NotImplementedException();
		}

		void ITransactable.StartTransaction()
		{
			throw new NotImplementedException();
		}

		void ITransactable.CommitTransaction()
		{
			throw new NotImplementedException();
		}

		void ITransactable.RollbackTransaction()
		{
			throw new NotImplementedException();
		}
	}
}
