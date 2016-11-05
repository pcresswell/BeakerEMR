using System;
using System.Text;

namespace Beaker.Generators.Methods
{
	public class GenericMethod : Method
	{
		public GenericMethod()
		{
		}

		public override void GetMethodBody(StringBuilder builder)
		{
			foreach (var bodyLine in this.Body)
			{
				builder.Append(this.PrependTabs + "\t").Append(bodyLine).AppendLine();
			}
		}
	}
}
