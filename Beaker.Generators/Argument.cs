using System;
using Humanizer;
namespace Beaker.Generators
{
	public abstract class Argument : Generator
	{
		public Argument()
		{
		}

		public Argument Type(string t)
		{
			this._Type = t;
			return this;
		}

		public Argument Name(string n)
		{
			this._Name = n;
			return this;
		}

		public override void Generate()
		{
			this.FileContents = this._Type.ToString() + " " + this._Name.Camelize();
		}
	}
}
