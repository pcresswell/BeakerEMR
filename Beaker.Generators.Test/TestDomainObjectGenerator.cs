using NUnit.Framework;
using System;
using Beaker.Generators;
using Beaker.Generators.DomainObjects;
using System.IO;

namespace Beaker.Generators.Test
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void TestPatientDomainObjectGenerator()
		{
			var domainObjectGenerator =
				new DomainObjectScaffold()
					.ClassName("TestPatient")
						.AddProperty("string", "FirstName").End
						.AddProperty("string", "LastName").End
					.End();
			domainObjectGenerator.Namespace = "Beaker.Module.Common";
			
			domainObjectGenerator.Generate();

			string domainObjectFileContents =
@"/*
/* NOTE: This is automatically generated code created by the Beaker.Generators package.
/* Do not manually change this.
*/

using System;
using Beaker.Core;

namespace Beaker.Module.Common
{
	public partial class TestPatient : DomainObject
	{
		private TestPatient(Guid iD, Guid domainObjectID, string firstName, string lastName) : base(iD, domainObjectID)
		{
			this.FirstName = firstName;
			this.LastName = lastName;
		}

		internal string FirstName
		{
			get;
			private set;
		}

		internal string LastName
		{
			get;
			private set;
		}

		internal static TestPatient Load(Guid iD, Guid domainObjectID, string firstName, string lastName)
		{
			return new TestPatient(iD, domainObjectID, firstName, lastName);
		}

		internal static TestPatient Create(string firstName, string lastName)
		{
			return TestPatient.Load(Guid.NewGuid(), Guid.NewGuid(),firstName, lastName);
		}

		internal TestPatient Update(string firstName, string lastName)
		{
			return TestPatient.Load(Guid.NewGuid(), this.DomainObjectID,firstName, lastName);
		}

	}
}
";
			Assert.AreEqual("TestPatient.designer.cs", domainObjectGenerator.DomainObjectClass.FileName);
			Assert.AreEqual(domainObjectFileContents, domainObjectGenerator.DomainObjectClass.FileContents);

		}


		[Test()]
		public void TestPatientSQLiteGenerator()
		{
			var domainObjectGenerator =
				new DomainObjectScaffold()
					.ClassName("TestPatient")
						.AddProperty("string", "FirstName").End
						.AddProperty("string", "LastName").End
					.End();

			domainObjectGenerator.Namespace = "Beaker.Module.Common";
			domainObjectGenerator.Generate();

			string sqliteFileContents =
@"/*
/* NOTE: This is automatically generated code created by the Beaker.Generators package.
/* Do not manually change this.
*/

using System;
using SQLite;
using Beaker.Core;
using Beaker.Data;
using Beaker.Data.SQLite;

namespace Beaker.Module.Common
{
	[Table(""test_patients"")]
	public partial class TestPatientTable : Table
	{
		[Column(""first_name"")]
		public string FirstName
		{
			get;
			set;
		}

		[Column(""last_name"")]
		public string LastName
		{
			get;
			set;
		}

	}
}
";
			Assert.AreEqual("TestPatientTable.designer.cs", domainObjectGenerator.SQLiteTable.FileName);
			Assert.AreEqual(sqliteFileContents, domainObjectGenerator.SQLiteTable.FileContents);

		}

		[Test()]
		public void TestPatientCreateEditorGenerator()
		{
			var domainObjectGenerator =
				new DomainObjectScaffold()
					.ClassName("TestPatient")
						.AddProperty("string", "FirstName").End
						.AddProperty("string", "LastName").End
					.End();

			domainObjectGenerator.Namespace = "Beaker.Module.Common";
			domainObjectGenerator.Generate();

			string createEditorContents =
@"/*
/* NOTE: This is automatically generated code created by the Beaker.Generators package.
/* Do not manually change this.
*/

using System;
using Authorize;
using Beaker.Core;

namespace Beaker.Module.Common
{
	public partial class CreateTestPatientEditor : Editor
	{
		public CreateTestPatientEditor() : base()
		{
		}

		public string FirstName
		{
			private get;
			set;
		}

		public string LastName
		{
			private get;
			set;
		}

		public TestPatient Create()
		{
			return TestPatient.Create(FirstName, LastName);
		}

	}
}
";
			Assert.AreEqual("CreateTestPatientEditor.designer.cs", domainObjectGenerator.CreateEditor.FileName);
			Assert.AreEqual(createEditorContents, domainObjectGenerator.CreateEditor.FileContents);

		}

		[Test()]
		public void TestPatientReadEditorGenerator()
		{
			var domainObjectGenerator =
				new DomainObjectScaffold()
					.ClassName("TestPatient")
						.AddProperty("string", "FirstName").End
						.AddProperty("string", "LastName").End
					.End();

			domainObjectGenerator.Namespace = "Beaker.Module.Common";
			domainObjectGenerator.Generate();

			string readEditorContents =
@"/*
/* NOTE: This is automatically generated code created by the Beaker.Generators package.
/* Do not manually change this.
*/

using System;
using Authorize;
using Beaker.Core;

namespace Beaker.Module.Common
{
	public partial class ReadTestPatientEditor : Editor
	{
		public ReadTestPatientEditor() : base()
		{
		}

		public string FirstName
		{
			get
			{
				return this.GetSubjectAs<TestPatient>().FirstName;
			}
		}

		public string LastName
		{
			get
			{
				return this.GetSubjectAs<TestPatient>().LastName;
			}
		}

	}
}
";
			Assert.AreEqual("ReadTestPatientEditor.designer.cs", domainObjectGenerator.ReadEditor.FileName);
			Assert.AreEqual(readEditorContents, domainObjectGenerator.ReadEditor.FileContents);

		}

		[Test()]
		public void TestPatientUpdateEditorGenerator()
		{
			var domainObjectGenerator =
				new DomainObjectScaffold()
					.ClassName("TestPatient")
						.AddProperty("string", "FirstName").End
						.AddProperty("string", "LastName").End
					.End();

			domainObjectGenerator.Namespace = "Beaker.Module.Common";
			domainObjectGenerator.Generate();

			string updateEditorContents =
@"/*
/* NOTE: This is automatically generated code created by the Beaker.Generators package.
/* Do not manually change this.
*/

using System;
using Authorize;
using Beaker.Core;

namespace Beaker.Module.Common
{
	public partial class UpdateTestPatientEditor : Editor
	{
		public UpdateTestPatientEditor() : base()
		{
		}

		public string FirstName
		{
			private get;
			set;
		}

		public string LastName
		{
			private get;
			set;
		}

		public TestPatient Update()
		{
			return this.GetSubjectAs<TestPatient>().Update(FirstName, LastName);
		}

	}
}
";
			Assert.AreEqual("UpdateTestPatientEditor.designer.cs", domainObjectGenerator.UpdateEditor.FileName);
			Assert.AreEqual(updateEditorContents, domainObjectGenerator.UpdateEditor.FileContents);

		}

		[Test()]
		public void TestPatientStorageEngineGenerator()
		{
			var domainObjectGenerator =
				new DomainObjectScaffold()
					.ClassName("TestPatient")
						.AddProperty("string", "FirstName").End
						.AddProperty("string", "LastName").End
					.End();

			domainObjectGenerator.Namespace = "Beaker.Module.Common";
			domainObjectGenerator.Generate();

			string storageEngineContents =
@"/*
/* NOTE: This is automatically generated code created by the Beaker.Generators package.
/* Do not manually change this.
*/

using System;
using Beaker.Core;
using Beaker.Data.SQLite;

namespace Beaker.Module.Common
{
	public partial class TestPatientStorageEngine : SQLiteStorageEngine<TestPatient, TestPatientTable>
	{
		public TestPatientStorageEngine(BeakerSQLiteConnection connection) : base(connection)
		{
		}

		protected override TestPatient MapToDomainObject(TestPatientTable table)
		{
			return TestPatient.Load(table.ID, table.DomainObjectID, table.FirstName, table.LastName);
		}

		protected override TestPatientTable MapToTable(TestPatient domainObject)
		{
			return new TestPatientTable()
			{
				ID = domainObject.ID,
				DomainObjectID = domainObject.DomainObjectID,
				FirstName = domainObject.FirstName,
				LastName = domainObject.LastName,
			};
		}

	}
}
";
			Assert.AreEqual("TestPatientStorageEngine.designer.cs", domainObjectGenerator.StorageEngine.FileName);
			Assert.AreEqual(storageEngineContents, domainObjectGenerator.StorageEngine.FileContents);

		}

		[Test()]
		public void TestWriteOutContents()
		{
			var patientScaffold =
				new Beaker.Code.PatientDomainModel();


			if (!File.Exists("/Users/peter/Downloads/Module/Common/" + patientScaffold.Model.Name + "/"))
			{
				Directory.CreateDirectory("/Users/peter/Downloads/Module/Common/" + patientScaffold.Model.Name);
			}

			foreach (var d in patientScaffold.Model.Classes)
			{
				File.WriteAllText("/Users/peter/Downloads/Module/Common/" + patientScaffold.Model.Name + "/" + d.FileName, d.FileContents);
			}
		}
	}
}

/*
 * 

*/
