using System;
using System.Collections.Generic;
using System.Linq;

namespace Beaker.Core
{
	internal class RepositoryDictionary : IRepositoryDictionary
	{
		internal RepositoryDictionary()
		{
			this.Repositories = new Dictionary<Type, Repository>();
		}

		private IDictionary<Type, Repository> Repositories { get; set; }

		void IRepositoryRegistry.AddRepository<TDomain>(GenericRepository<TDomain> repository)
		{
			this.AddRepository(typeof(TDomain), repository);
		}

		internal void AddRepository(Type t, Repository repository) 
		{
			this.Repositories[t] = (Beaker.Core.Repository)repository;
		}

		private void AddRepository<TDomain>(GenericRepository<TDomain> repository) where TDomain : DomainObject
		{
			this.Repositories[typeof(TDomain)] = repository;
		}

		void ITransactable.CommitTransaction()
		{
			if (this.Repositories.Count.Equals(0))
			{
				return;
			}

			Repository r = (Beaker.Core.Repository)this.Repositories.Values.First();
			r.CommitTransaction();
		}

		IRepositoryDictionary IForkable<IRepositoryDictionary>.Fork(Guid sessionId)
		{
			RepositoryDictionary newDictionary = new RepositoryDictionary();

			foreach (KeyValuePair<Type,Repository> kvp in this.Repositories)
			{
				Repository forkable = kvp.Value;
				var newRepository = forkable.Fork(sessionId);
				newDictionary.AddRepository(kvp.Key, newRepository);
			}

			return newDictionary;
		}

		GenericRepository<TDomain> IRepositoryIndex.GetRepository<TDomain>()
		{
			return (Beaker.Core.GenericRepository<TDomain>)this.Repositories[typeof(TDomain)];
		}

		void ITransactable.RollbackTransaction()
		{
			if (this.Repositories.Count.Equals(0))
			{
				return;
			}

			Repository r = (Beaker.Core.Repository)this.Repositories.Values.First();
			r.RollbackTransaction();
		}

		void ITransactable.StartTransaction()
		{
			if (this.Repositories.Count.Equals(0))
			{
				return;
			}

			Repository r = (Beaker.Core.Repository)this.Repositories.Values.First();
			r.StartTransaction();
		}

		public void Initialize()
		{
			foreach (KeyValuePair<Type, Repository> kvp in this.Repositories)
			{
				Repository repo = kvp.Value;
				repo.Initialize();
			}
		}
	}
}
