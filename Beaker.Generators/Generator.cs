using System;
using Humanizer;
using System.Collections.Generic;
using System.Text;

namespace Beaker.Generators
{
	public abstract class Generator
	{
		public Generator()
		{
			this.Annotations = new List<string>();
		}

		protected IList<string> Annotations { get; private set; }
		public abstract void Generate();

		public string FileContents { get; protected set; }
		public string PrependTabs { get; set; }

		protected Visibility _Visibility { get; set; }
		protected string _Type { get; set; }
		protected string _Name { get; set; }

		public Generator SetOverride(bool isOverride)
		{
			this.IsOverride = isOverride;
			return this;
		}

		protected void GetAnnotations(StringBuilder builder)
		{
			foreach (var annotation in this.Annotations)
			{
				builder
					.Append(this.PrependTabs)
					.Append("[")
					.Append(annotation)
					.Append("]")
					.AppendLine();
			}
		}

		internal virtual Generator AddAnnotation(string v)
		{
			this.Annotations.Add(v);
			return this;
		}

		protected bool IsOverride { get; set; }

		internal string GetNameAsCamelize()
		{
			return this._Name.Camelize();
		}

		internal string GetNameAsPascal()
		{
			return this._Name.Pascalize();
		}

		internal string GetTypeAsLowerCase()
		{
			return this._Type.ToString().ToLower();
		}

		internal string GetVisibilityAsLowerCase()
		{
			if (Visibility.None.Equals(_Visibility))
			{
				return string.Empty;
			}

			return this._Visibility.ToString().ToLower();
		}

		public override string ToString()
		{
			return this.FileContents;
		}

		protected void OpeningBrackets(System.Text.StringBuilder builder)
		{
			builder.Append(this.PrependTabs).Append("{").AppendLine();
		}
		protected void ClosingBrackets(System.Text.StringBuilder builder)
		{
			builder.Append(this.PrependTabs).Append("}").AppendLine();
		}
	}
}
