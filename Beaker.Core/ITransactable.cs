using System;
namespace Beaker.Core
{
	public interface ITransactable
	{
		void StartTransaction();
		void CommitTransaction();
		void RollbackTransaction();
	}
}
