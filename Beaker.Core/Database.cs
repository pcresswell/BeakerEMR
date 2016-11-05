using System;
namespace Beaker.Core
{
	public class Database : IRepositoryRegistry, IRepositoryIndex
	{
		public Database()
		{
			this.RepositoryDictionary = new RepositoryDictionary();
			this.CurrentSessionID = Guid.NewGuid();
		}

		protected Guid CurrentSessionID { get; private set; }

		private IRepositoryDictionary RepositoryDictionary { get; set; }

		public void CommitTransaction()
		{
			this.RepositoryDictionary.CommitTransaction();
		}

		public void RollbackTransaction()
		{
			this.RepositoryDictionary.RollbackTransaction();
		}

		public void StartTransaction()
		{
			this.RepositoryDictionary.StartTransaction();
		}

		public void AddRepository<TDomain>(GenericRepository<TDomain> repository) where TDomain : DomainObject
		{
			this.RepositoryDictionary.AddRepository(repository);
		}

		/// <summary>
		/// Fork this instance.
		/// </summary>
		public Database Fork()
		{
			Database newDatabase = new Database();
			newDatabase.RepositoryDictionary = this.RepositoryDictionary.Fork(newDatabase.CurrentSessionID);
			return newDatabase;
		}

		public GenericRepository<TDomain> GetRepository<TDomain>() where TDomain : DomainObject
		{
			return this.RepositoryDictionary.GetRepository<TDomain>();
		}

		public void Initialize()
		{
			this.RepositoryDictionary.Initialize();
		}

		internal void Save<TDomain>(TDomain domainObject, IUser author) where TDomain : DomainObject
		{
			this.RepositoryDictionary.GetRepository<TDomain>().Save(domainObject, author);
		}

		internal void Delete<TDomain>(TDomain domainObject, IUser author) where TDomain : DomainObject
		{
			this.RepositoryDictionary.GetRepository<TDomain>().Delete(domainObject, author);
		}

		internal TDomain Find<TDomain>(Guid domainObjectID) where TDomain : DomainObject
		{
			return this.RepositoryDictionary.GetRepository<TDomain>().Get(domainObjectID);
		}
	}
}
