using System;
using System.Linq;
namespace Beaker.Generators.DomainObjects.Methods
{
	/// <summary>
	/// A method used to create a domain object
	/// </summary>
	internal class CreateMethodGenerator : Method
	{
		public CreateMethodGenerator()
		{
			this.SetName("Create");
			this.SetStaticMethod();
		}

		public override void GetMethodBody(System.Text.StringBuilder builder)
		{
			builder
				.Append(this.PrependTabs + "\t")
				.Append("return ");

				
			// In this case, skip method arguments is really telling us are we doing this
			// method for the CreateXXXEditor or in the XXX domain object itself.

			// this is for the domain object
			if (!this.SkipMethodArguments)
			{
				builder.Append(this.MethodType + ".Load(Guid.NewGuid(), Guid.NewGuid(),").Append(string.Join(", ", this.Arguments.Select(x => x.GetNameAsCamelize())));
			}
			else // this is for the editor.
			{
				builder.Append(this.MethodType + ".Create(").Append(string.Join(", ", this.Arguments.Select(x => x.GetNameAsPascal())));
			}

			builder
				.Append(");")
				.AppendLine();
		}
	}
}
