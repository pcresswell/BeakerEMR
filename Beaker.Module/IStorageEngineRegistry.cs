using System;
using Beaker.Core;
using Beaker.Data;

namespace Beaker.Module
{
	public interface IStorageEngineRegistry
	{
		void Register<TDomain>(IStorageEngine<TDomain> engine) where TDomain : DomainObject;
	}
}
