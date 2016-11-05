using System;
using Beaker.Data;
using Beaker.Data.SQLite;
using Beaker.Module;
using Beaker.Core;
using Prism.Modularity;
using Prism.Common;

namespace Beaker.Module.Common
{
	
	public class Module : ICommonModule
	{
		public Module(SQLiteDatabase database)
		{
			this.Database = database;
		}

		private SQLiteDatabase Database { get; set; }

		IUser ICommonModule.CreateSuperUser(string password, string salt)
		{
			CreateSuperUserCommand createCommand = new CreateSuperUserCommand(this.Database, this.Database)
			{
				Password = password,
				Salt = salt
			};

			return createCommand.Create();
		}

		void Prism.Modularity.IModule.Initialize()
		{
			this.Database.AddRepository(new GenericRepository<Patient>(new Beaker.Module.Common.PatientStorageEngine(this.Database)));
			this.Database.AddRepository(new GenericRepository<User>(new UserStorageEngine(this.Database)));
		}
	}
}
