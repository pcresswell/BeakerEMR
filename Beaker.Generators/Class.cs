using System;
using System.Collections.Generic;
using System.Linq;
namespace Beaker.Generators
{
	public class Class : Generator
	{
		public Class() : base()
		{
			this.Constructors = new List<Constructor>();
			this.Methods = new List<Method>();
			this.Properties = new List<Property>();
			this.Usings = new List<string>();
			this.Usings.Add("System");
		}

		public string NSpace { get; private set; }
		internal IList<Constructor> Constructors { get; private set; }
		internal IList<Property> Properties { get; private set; }
		private IList<Method> Methods { get; set; }
		private IList<string> Usings { get; set; }


		public string BaseName { get; private set; }

		public Class Namespace(string nspace)
		{
			this.NSpace = nspace;
			return this;
		}

		public void End() { }

		public Class ClassName(string n)
		{
			this._Name = n;
			return this;
		}

		public string FileName
		{
			get
			{
				return this._Name + ".designer.cs";
			}
		}

		public Class BaseClassName(string n)
		{
			this.BaseName = n;
			return this;
		}

		public Class AddUsing(string usings)
		{
			this.Usings.Add(usings);
			return this;
		}

		public Constructor AddConstructor(Visibility v = Visibility.None, string name = default(string))
		{
			Constructor c = new Constructor(this);
			c.Visibility(v);
			c.Name(name);
			this.Constructors.Add(c);
			return c;
		}

		public T AddMethod<T>() where T : Method, new()
		{
			T m = new T();
			m.ClassGenerator = this;
			this.Methods.Add(m);
			return m;
		}

		public T AddProperty<T>() where T: Property, new()
		{
			T p = new T()
			{
				ClassGenerator = this
			};

			this.Properties.Add(p);
			return p;
		}

		internal new Class AddAnnotation(string v)
		{
			base.AddAnnotation(v);
			return this;
		}

		public override void Generate()
		{
			foreach (var constructor in this.Constructors)
			{
				constructor.Generate();
			}

			foreach (var method in this.Methods)
			{
				method.Generate();
			}

			foreach (var prop in this.Properties)
			{
				prop.Generate();
			}

			var builder = new System.Text.StringBuilder();

			builder
				.Append("/*")
				.AppendLine();

			builder
				.Append("/* NOTE: This is automatically generated code created by the Beaker.Generators package.")
				.AppendLine();

			builder
				.Append("/* Do not manually change this.")
				.AppendLine();

			builder
				.Append("*/")
				.AppendLine();

			builder.AppendLine();

			foreach (var u in this.Usings)
			{
				builder
					.Append("using ")
					.Append(u)
					.Append(";")
					.AppendLine();
			}

			builder.AppendLine();

			builder
				.Append("namespace " + this.NSpace)
				.AppendLine();

			builder
				.Append("{")
				.AppendLine();
			
			if (this.Annotations.Any())
			{
				builder.Append("\t");
				this.GetAnnotations(builder);
			}

			builder
				.Append("\tpublic partial class " + this.GetNameAsPascal() + " : " + this.BaseName)
				.AppendLine();

			builder
				.Append("\t{")
				.AppendLine();

			if (this.Constructors.Any())
			{
				builder
				   .Append(this.Constructors.First().FileContents)
				   .AppendLine();
			}

			foreach (var prop in this.Properties)
			{
				builder
					.Append(prop.FileContents)
					.AppendLine();
			}

			foreach (var m in this.Methods)
			{
				builder
					.Append(m.FileContents)
					.AppendLine();
			}

			builder
				.Append("\t}")
				.AppendLine();

			builder
				.Append("}")
				.AppendLine();

			this.FileContents = builder.ToString();
		}
	}
}


