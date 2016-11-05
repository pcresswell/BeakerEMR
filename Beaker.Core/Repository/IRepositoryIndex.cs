using System;
namespace Beaker.Core
{
	public interface IRepositoryIndex
	{
		GenericRepository<TDomain> GetRepository<TDomain>() where TDomain : DomainObject;
	}
}
