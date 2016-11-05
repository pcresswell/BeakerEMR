using System;
namespace Beaker.Core
{
	public interface IForkable<T>
	{
		T Fork(Guid sessionId);
	}

	public interface IForkable : IForkable<object>
	{
		
	}
}
