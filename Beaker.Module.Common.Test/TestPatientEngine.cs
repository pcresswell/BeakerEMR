using NUnit.Framework;
using System;
using Beaker.Core;
using Beaker.Data.SQLite;
using SQLite;
using Beaker.Data;
using System.Linq;

namespace Beaker.Module.Common.Test
{
	[TestFixture()]
	public class TestPatientEngine : BaseTest
	{
		
		[Test()]
		public void AddAPatient()
		{
			var patient = this.CreatePeterCresswellPatient();
			Assert.IsTrue(this.SuperUser.CanCreate(patient));
			Assert.IsTrue(this.SuperUser.CanUpdate(patient));
			this.SuperUser.Save<Patient>(patient);

			// now ask for the patient back
			var retrievedPatient = this.SuperUser.Find<Patient>(patient.DomainObjectID);
			Assert.AreEqual(retrievedPatient, patient);

			Assert.AreEqual("Peter", this.SuperUser.Editor<ReadPatientEditor>(retrievedPatient).FirstName);
			Assert.AreEqual("Cresswell", this.SuperUser.Editor<ReadPatientEditor>(retrievedPatient).LastName);
		}

		[Test]
		public void RemoveAPatient()
		{
			var patient = this.CreatePeterCresswellPatient();

			this.SuperUser.Save<Patient>(patient);

			// now ask for the patient back
			var retrievedPatient =  this.SuperUser.Find<Patient>(patient.DomainObjectID);
			Assert.AreEqual(retrievedPatient, patient);

			// now remove the patient
			this.SuperUser.Delete<Patient>(patient);

			// now getting the object should give us nothing
			Assert.Throws<RecordNotFoundException>(() =>
			{
				this.SuperUser.Find<Patient>(patient.DomainObjectID);
			});
		}

		[Test]
		public void UserCannotInsert()
		{
			var patient = this.CreatePeterCresswellPatient();

			Assert.Throws<InsufficientPermissionException>(() =>
			{
				this.User.Save<Patient>(patient);
			});
		}

		[Test]
		public void UserCannotRemove()
		{
			var patient = this.CreatePeterCresswellPatient();

			this.SuperUser.Save<Patient>(patient);
			Assert.Throws<InsufficientPermissionException>(() =>
			{
				this.User.Delete<Patient>(patient);
			});

			Assert.AreEqual(patient, this.SuperUser.Find<Patient>(patient.DomainObjectID));
		}

		[Test]
		public void UserCannotUpdate()
		{
			var patient = this.CreatePeterCresswellPatient();

			this.SuperUser.Save<Patient>(patient);

			// now modify the user and try and save it
			var updater = this.SuperUser.Editor<UpdatePatientEditor>(patient);
			updater.FirstName = "Susana";
			updater.LastName = "Hsu";
			updater.Update();
			var updatedPatient = updater.Update();

			Assert.Throws<InsufficientPermissionException>(() =>
			{
				this.User.Save<Patient>(updatedPatient);
			});
		}

		[Test]
		public void AskForDomainObjectThatDoesNotExist()
		{
			Assert.Throws<RecordNotFoundException>(() =>
			{
				this.SuperUser.Find<Patient>(Guid.NewGuid());
			});
		}

		private Patient CreatePeterCresswellPatient()
		{
			var createPatient = this.SuperUser.Editor<CreatePatientEditor>();
			createPatient.FirstName = "Peter";
			createPatient.LastName = "Cresswell";
			return createPatient.Create();
		}

		[Test]
		public void ForkDatabase()
		{
			// Forking creates a new copy of the database with a separate connection
			// The connections operate against the same db but transactions 
			// should not affect each other
			// Remember that the superUser was created against a different database

			var patient = this.CreatePeterCresswellPatient();

			Database newDatabase = this.Database.Fork();
			newDatabase.StartTransaction();

			var editor = this.SuperUser.Editor<UpdatePatientEditor>(patient);
			editor.FirstName = "Susana";
			this.SuperUser.Save(editor.Update());

			newDatabase.RollbackTransaction();

			// the updated patient should still exist

			var updatedPatient = this.SuperUser.Find<Patient>(patient.DomainObjectID);

			Assert.AreEqual("Susana", updatedPatient.FirstName);

			// but if you do it against the correct database, it WILL work

			this.SuperUser.StartTransaction();

			editor = this.SuperUser.Editor<UpdatePatientEditor>(updatedPatient);
			editor.FirstName = "Jeremy";
			this.SuperUser.Save(editor.Update());

			this.SuperUser.RollbackTransaction();

			// the updated patient should still exist

			var secondUpdate = this.SuperUser.Find<Patient>(patient.DomainObjectID);

			// this should still be susana
			Assert.AreEqual("Susana", secondUpdate.FirstName);
		}
	}
}

