using System;
namespace Beaker.Generators.DomainObjects
{
	public class DomainObjectProperty : Generator
	{
		public DomainObjectProperty(DomainObjectScaffold d)
		{
			this.End = d;
		}

		public DomainObjectScaffold End { get; private set; }
		public string PropertyType { get; private set; }
		public string PropertyName { get; private set; }

		public DomainObjectProperty Type(string type)
		{
			this.PropertyType = type;
			return this;
		}

		public DomainObjectProperty Name(string name)
		{
			this.PropertyName = name;
			return this;

		}

		public override void Generate()
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			return this.PropertyName;
		}
	}
}
