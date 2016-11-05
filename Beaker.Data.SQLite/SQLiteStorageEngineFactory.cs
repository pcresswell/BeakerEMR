using System;
using Beaker.Core;
using Beaker.Data;

namespace Beaker.Data.SQLite
{
	public class SQLiteStorageEngineFactory : StorageEngineFactory
	{
		public SQLiteStorageEngineFactory(BeakerSQLiteConnection connection)
		{
			this.Connection = connection;
		}

		private BeakerSQLiteConnection Connection { get; set; }

		public override IStorageEngine<Patient> CreatePatientStorageEngine()
		{
			return new PatientStorageEngine(this.Connection);
		}

		public override IStorageEngine<User> CreateUserStorageEngine()
		{
			return new UserStorageEngine(this.Connection);
		}
	}
}
