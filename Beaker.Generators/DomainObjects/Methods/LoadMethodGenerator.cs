using System;
using System.Linq;
namespace Beaker.Generators.DomainObjects.Methods
{
	internal class LoadMethodGenerator : Method
	{
		public LoadMethodGenerator()
		{
			this.SetVisibility(Generators.Visibility.Internal);
			this.SetStaticMethod();
			this.SetName("Load");
		}

		protected override string GetReturnType()
		{
			return this.ClassGenerator.GetNameAsPascal();
		}

		protected override System.Collections.Generic.IList<Argument> GetMethodArguments()
		{
			return this.ClassGenerator.Constructors.First().Arguments.Cast<Argument>().ToList();
		}

		public override void GetMethodBody(System.Text.StringBuilder builder)
		{
			var arguments = this.ClassGenerator.Constructors.First().Arguments;
			builder
				.Append(this.PrependTabs + "\t")
				.Append("return new ")
				.Append(this.ClassGenerator.GetNameAsPascal() + "(")
				.Append(string.Join(", ", arguments.Select(x => x.GetNameAsCamelize())))
				.Append(");")
				.AppendLine();
		}
	}
}
