using System;
using System.Collections.Generic;
using System.Linq;

namespace Beaker.Generators
{
	public class Constructor : Generator
	{
		public Constructor(Class c)
		{
			this.Arguments = new List<ConstructorArgument>();
			this.PrependTabs = "\t\t";

		}

		private Class ClassGenerator { get; set; }
		internal IList<ConstructorArgument> Arguments { get; private set; }

		public Class End
		{
			get
			{
				return this.ClassGenerator;
			}
		}

		public Constructor Visibility(Visibility v)
		{
			this._Visibility = v;
			return this;
		}

		public Constructor AddArgument(string name = default(string), string type = default(string), bool definedInBaseClass = false)
		{
			var argument = new ConstructorArgument(this);
			argument.Name(name);
			argument.Type(type);
			if (definedInBaseClass)
			{
				argument.InBase();
			}

			Arguments.Add(argument);
			return this;
		}

		public override void Generate()
		{
			foreach (var a in this.Arguments)
			{
				a.Generate();
			}

			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			string signature = Method.CreateMethodSignature(
				this._Visibility,
				false,
				false,
				null,
				this.GetNameAsPascal(),
				this.Arguments.Cast<Argument>().ToList() ,
				this.Arguments.Where(x=> x.DeclaredInBase).Cast<Argument>().ToList());

			builder
				.Append(this.PrependTabs)
				.Append(signature)
				.AppendLine();

			this.OpeningBrackets(builder);
			
			foreach (var a in this.Arguments.Where(x => x.DeclaredInBase == false))
			{
				builder
					.Append(this.PrependTabs+"\t")
					.Append("this." + a.GetNameAsPascal() + " = " + a.GetNameAsCamelize() + ";")
					.AppendLine();
			}

			this.ClosingBrackets(builder);
			this.FileContents = builder.ToString();
		}

		internal Constructor Name(string name)
		{
			this._Name = name;
			return this;
		}
	}
}
