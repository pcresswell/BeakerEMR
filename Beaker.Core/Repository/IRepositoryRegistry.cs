using System;
namespace Beaker.Core
{
	public interface IRepositoryRegistry
	{
		void AddRepository<TDomain>(GenericRepository<TDomain> repository) where TDomain : DomainObject;	
	}
}
