using System;
namespace Beaker.Generators
{
	public class MethodArgument : Argument
	{
		public MethodArgument(Method m) 
		{
			this.End = m;
		}

		public Method End { get; private set;}
	}
}
