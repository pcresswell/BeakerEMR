using System;
using System.Collections.Generic;

namespace Beaker.Generators
{
	public class Property : Generator
	{
		public Property()
		{
			this.Accessors = new List<PropertyAccessor>();
			this.PrependTabs = "\t\t";
		}

		internal Class ClassGenerator { get; set; }
		protected IList<PropertyAccessor> Accessors { get; private set; }

		internal new Property AddAnnotation(string v)
		{
			base.AddAnnotation(v);
			return this;
		}

		internal string GetPropertyType()
		{
			return this._Type;
		}

		public void End() { }

		public Property SetName(string name)
		{
			this._Name = name;
			return this;
		}

		public Property SetType(string t)
		{
			this._Type = t;
			return this;
		}

		public Property SetVisibility(Visibility v)
		{
			this._Visibility = v;
			return this;
		}

		public PropertyAccessor Accessor()
		{
			PropertyAccessor a = new PropertyAccessor(this);
			this.Accessors.Add(a);
			return a;
		}

		public override void Generate()
		{
			foreach (var a in this.Accessors)
			{
				a.Generate();
			}

			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			this.GetAnnotations(builder);

			builder
				.Append(this.PrependTabs)
				.Append(this.GetVisibilityAsLowerCase()+ " ");

			if (this.IsOverride)
			{
				builder.Append("override ");
			}

			builder
				.Append(this.GetTypeName() + " " + this.GetNameAsPascal())
				.AppendLine();

			this.OpeningBrackets(builder);

			foreach (var a in this.Accessors)
			{
				builder
					.Append(a.FileContents);
			}

			this.ClosingBrackets(builder);

			this.FileContents = builder.ToString();
		}



		public string GetTypeName()
		{
			return this._Type;
		}

}
}
