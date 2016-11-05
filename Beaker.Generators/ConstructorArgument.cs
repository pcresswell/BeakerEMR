using System;
namespace Beaker.Generators
{
	public class ConstructorArgument : Argument
	{
		public ConstructorArgument(Constructor c)
		{
			this.End = c;
		}

		public bool DeclaredInBase { get; private set; }

		public Constructor End { get; private set;}

		public ConstructorArgument InBase()
		{
			this.DeclaredInBase = true;
			return this;
		}
	}
}
