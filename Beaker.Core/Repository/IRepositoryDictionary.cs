using System;
namespace Beaker.Core
{
	public interface IRepositoryDictionary : IRepositoryRegistry, IRepositoryIndex, IForkable<IRepositoryDictionary>, ITransactable, IInitializeable
	{
		
	}
}
