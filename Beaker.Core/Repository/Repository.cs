using System;
namespace Beaker.Core
{
	public abstract class Repository : IInitializeable	{
		public Repository()
		{
		}

		internal abstract void CommitTransaction();
		internal abstract Repository Fork(Guid sessionId);
		public abstract void Initialize();
		internal abstract void RollbackTransaction();
		internal abstract void StartTransaction();
	}
}
