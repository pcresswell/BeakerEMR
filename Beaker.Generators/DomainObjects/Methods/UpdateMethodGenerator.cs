using System;
using System.Linq;
namespace Beaker.Generators.DomainObjects.Methods
{
	internal class UpdateMethodGenerator : Method
	{
		public UpdateMethodGenerator()
		{
			this.SetVisibility(Generators.Visibility.Internal);
			this.SetName("Update");
		}

		protected override string GetReturnType()
		{
			return this.ClassGenerator.GetNameAsPascal();
		}

		protected override System.Collections.Generic.IList<Argument> GetMethodArguments()
		{
			return this.ClassGenerator.Constructors.First().Arguments.Where(x => x.DeclaredInBase == false).Cast<Argument>().ToList();
		}

		public override void GetMethodBody(System.Text.StringBuilder builder)
		{
			var arguments = this.ClassGenerator.Constructors.First().Arguments.Where(x => x.DeclaredInBase == false);

			builder
				.Append(this.PrependTabs + "\t")
				.Append("return ")
				.Append(this.ClassGenerator.GetNameAsPascal() + ".Load(Guid.NewGuid(), this.DomainObjectID,")
				.Append(string.Join(", ", arguments.Select(x => x.GetNameAsCamelize())))
				.Append(");")
				.AppendLine();
		}
	}
}
