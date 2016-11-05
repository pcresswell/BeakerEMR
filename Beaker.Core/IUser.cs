using System;
namespace Beaker.Core
{
	public interface IUser : ITransactable
	{
		Guid DomainObjectID { get; }

		TEditor Editor<TEditor>(object domainObject = null) where TEditor : Editor, new();
		IUser CreateUser(string username, string password, string salt);

		bool CanUpdate(object domainObject);
		bool CanCreate(object domainObject);
		bool CanDelete(object domainObject);

		void Save<TDomain>(TDomain domainObject) where TDomain : DomainObject;
		void Delete<TDomain>(TDomain domainObject) where TDomain : DomainObject;
		TDomain Find<TDomain>(Guid domainObjectID) where TDomain : DomainObject;
	}
}
