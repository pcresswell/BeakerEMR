using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beaker.Generators
{
	public abstract class Method : Generator
	{
		public Method()
		{
			this.Arguments = new List<MethodArgument>();
			this.PrependTabs = "\t\t";
			this.SkipMethodArguments = false;
			this.Body = new List<string>();
		}

		public bool IsStaticMethod { get; private set; }
		internal Class ClassGenerator { get; set; }
		protected IList<MethodArgument> Arguments { get; private set; }
		protected bool SkipMethodArguments { get; set; }
		protected IList<string> Body { get; private set; }


		/// <summary>
		/// Creates a method signature.
		/// </summary>
		/// <returns>The method signature.</returns>
		/// <param name="v">V.</param>
		/// <param name="isOverride">If set to <c>true</c> is override.</param>
		/// <param name="isStatic">If set to <c>true</c> is static.</param>
		/// <param name="returnType">Return type.</param>
		/// <param name="methodName">Method name.</param>
		/// <param name="arguments">Arguments.</param>
		/// <param name="baseArguments">Base arguments.</param>
		public static string CreateMethodSignature(Visibility v = Generators.Visibility.None, bool isOverride = false, bool isStatic = false, string returnType = default(string), string methodName = default(string), IList<Argument> arguments = null, IList<Argument> baseArguments = null)
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			if (!Beaker.Generators.Visibility.None.Equals(v))
			{
				builder
					.Append(v.ToString().ToLower() + " ");
			}

			if (isOverride)
			{
				builder
					.Append("override ");
			}

			if (isStatic)
			{
				builder
					.Append("static ");
			}

			if (!string.IsNullOrEmpty(returnType))
			{
				builder
					.Append(returnType + " ");
			}

			builder
				.Append(methodName + "(");

			if (arguments != null)
			{
				builder
					.Append(string.Join(", ", arguments));
			}

			builder
				.Append(")");

			if (baseArguments != null)
			{
				builder
					.Append(" : base(")
					.Append(string.Join(", ", baseArguments.Select(x => x.GetNameAsCamelize())))
					.Append(")");
			}

			return builder.ToString();
		}

		protected virtual void GetMethodSignature(System.Text.StringBuilder builder)
		{
			string signature = Method.CreateMethodSignature(this._Visibility, this.IsOverride, this.IsStaticMethod, this.GetReturnType(), this.MethodName, this.GetMethodArguments());
			this.GetAnnotations(builder);
			builder
				.Append(this.PrependTabs)
				.Append(signature)
				.AppendLine();
		}

		protected virtual IList<Argument> GetMethodArguments()
		{
			IList<Argument> arguments = new List<Argument>();
			if (!this.SkipMethodArguments)
			{
				foreach (var a in this.Arguments)
				{
					arguments.Add(a);
				}
			}

			return arguments;
		}

		public Method NoMethodArguments()
		{
			this.SkipMethodArguments = true;
			return this;
		}

		protected virtual string GetReturnType()
		{
			return this._Type;
		}

		protected string MethodName
		{
			get
			{
				return this._Name;
			}
		}

		protected string MethodType
		{
			get
			{
				return this._Type;
			}
		}

		public Class End
		{
			get
			{
				return this.ClassGenerator;
			}
		}

		public Method SetStaticMethod(bool staticMethod = true)
		{
			this.IsStaticMethod = staticMethod;
			return this;
		}

		public Method SetVisibility(Visibility v)
		{
			this._Visibility = v;
			return this;
		}

		public Method SetReturnType(string t)
		{
			this._Type = t;
			return this;
		}

		public Method SetName(string name)
		{
			this._Name = name;
			return this;
		}

		public MethodArgument AddArgument()
		{
			MethodArgument argumentGenerator = new MethodArgument(this);
			this.Arguments.Add(argumentGenerator);
			return argumentGenerator;
		}

		public abstract void GetMethodBody(System.Text.StringBuilder builder);

		public override void Generate()
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			foreach (var arg in this.Arguments)
			{
				arg.Generate();
			}

			var arguments = this.Arguments;

			this.GetMethodSignature(builder);
			this.OpeningBrackets(builder);
			this.GetMethodBody(builder);
			this.ClosingBrackets(builder);

			this.FileContents = builder.ToString();
		}

		public Method AddBodyLine(string body)
		{
			this.Body.Add(body);
			return this;
		}

		public new Method SetOverride(bool isOverride)
		{
			base.SetOverride(isOverride);
			return this;
		}
	}
}
