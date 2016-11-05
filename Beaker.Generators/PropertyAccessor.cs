using System;
namespace Beaker.Generators
{
	public class PropertyAccessor : Generator
	{
		public PropertyAccessor(Property p)
		{
			this.End = p;
			this.BodyContents = string.Empty;
			this.PrependTabs = "\t\t\t";
		}

		private AccessorType AccessorType { get; set; }
		public Property End { get; private set; }
		private string BodyContents { get; set; }

		public PropertyAccessor SetVisibility(Visibility v)
		{
			this._Visibility = v;
			return this;
		}

		public PropertyAccessor SetBodyContents(string bodyContents)
		{
			this.BodyContents = bodyContents;
			return this;
		}

		public PropertyAccessor SetAccessorType(AccessorType t)
		{
			this.AccessorType = t;
			return this;
		}

		public override void Generate()
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			builder
				.Append(this.PrependTabs);
			
			if (!Beaker.Generators.Visibility.None.Equals(this._Visibility))
			{
				builder
					.Append(this.GetVisibilityAsLowerCase() + " ");
			}

			builder
					.Append(this.AccessorType.ToString().ToLower());

			if (string.IsNullOrEmpty(this.BodyContents))
			{

				builder
					.Append(";")
					.AppendLine();
			}
			else
			{
				builder
					.AppendLine();

				this.OpeningBrackets(builder);

				builder
					.Append(this.PrependTabs + "\t")
					.Append(this.BodyContents)
					.AppendLine();

				this.ClosingBrackets(builder);
			}

			this.FileContents = builder.ToString();
		}
	}
}
