using System;
using Beaker.Generators;
using Beaker.Generators.DomainObjects;

namespace Beaker.Code
{
	public class PatientDomainModel
	{
		public PatientDomainModel()
		{
			this.Model =
				new DomainObjectScaffold()
				    .ClassName("Patient")
						.AddProperty("string", "FirstName").End
						.AddProperty("string", "LastName").End
					.End();
			
			this.Model.Namespace = "Beaker.Module.Common";
			this.Model.Generate();
		}

		public DomainObjectScaffold Model
		{
			get; private set;
		}
	}
}
