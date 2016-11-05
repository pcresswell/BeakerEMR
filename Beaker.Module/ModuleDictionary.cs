using System;
using Beaker.Core;
using System.Collections.Generic;
using System.Collections;
namespace Beaker.Module
{
	internal class ModuleDictionary
	{
		internal ModuleDictionary()
		{
			this.Modules = new Dictionary<Type, IModule>();
		}

		private IDictionary<Type, IModule> Modules { get; set; }

		internal void AddModule<TDomain>(IModule module) where TDomain : DomainObject
		{
			this.Modules[typeof(TDomain)] = module;
		}

		internal IModule GetModule<TDomain>() where TDomain : DomainObject
		{
			return this.Modules[typeof(TDomain)];
		}


	}
}
