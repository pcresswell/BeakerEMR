using System;
using Beaker.Core;
namespace Beaker.Module.Common
{
	public interface ICommonModule : IModule
	{
		IUser CreateSuperUser(string password, string salt);
	}
}
