using NUnit.Framework;
using System;
using Authorize;
using Beaker.Core;
using Beaker.Data;
using Beaker.Module.Common;
using Beaker.Data.SQLite;

namespace Beaker.Module.Common.Test
{
	[TestFixture()]
	public class TestPatient : BaseTest
	{
		[Test()]
		public void CreateAPatient()
		{
			IUser superUser = this.CreateSuperUser();
			var patient = CreatePeterCresswell(superUser);

			var reader = superUser.Editor<ReadPatientEditor>(patient);
			Assert.AreEqual("Peter", reader.FirstName);
			Assert.AreEqual("Cresswell", reader.LastName);
		}

		[Test()]
		public void UpdateAPatient()
		{
			IUser superUser = this.CreateSuperUser();

			var patient = CreatePeterCresswell(superUser);

			// now update the patient and verify that the patient was updated

			var updater = superUser.Editor<UpdatePatientEditor>(patient);
			updater.FirstName = "Susana";
			updater.LastName = "Hsu";
			var updatedPatient = updater.Update();
			 
			var reader = superUser.Editor<ReadPatientEditor>(updatedPatient);
			Assert.AreEqual("Susana", reader.FirstName);
			Assert.AreEqual("Hsu", reader.LastName);
		}

		public static Patient CreatePeterCresswell(IUser user)
		{
			return CreatePatient("Peter", "Cresswell", user);
		}

		public static Patient CreateSusanaHsu(IUser user)
		{
			return CreatePatient("Susana", "Hsu", user);
		}

		public static Patient CreatePatient(string firstName, string lastName, IUser user)
		{
			var creator = user.Editor<CreatePatientEditor>();
			creator.FirstName = firstName;
			creator.LastName = lastName;
			return creator.Create();
		}


	}
}

