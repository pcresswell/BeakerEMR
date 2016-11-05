namespace Beaker.Module.Common.Test
{
	using System;
	using NUnit.Framework;
	using Beaker.Core;
	using Beaker.Data.SQLite;
	using Beaker.Module;


	public class BaseTest 
	{

		public BaseTest()
		{
		}

		protected BeakerSQLiteConnection Connection { get; private set; }

		protected IUser SuperUser { get; set; }
		protected IUser User { get; private set; }

		private ICommonModule CommonModule { get; set; }
		private Guid SessionID { get; set; }


		protected SQLiteDatabase Database { get; private set; }

		[SetUp]
		public void Setup()
		{
			this.Database = new SQLiteDatabase(":memory:");
			this.CommonModule = new Beaker.Module.Common.Module(this.Database);
			this.CommonModule.Initialize();
			this.Database.Initialize();

			this.SuperUser = CommonModule.CreateSuperUser("123", "abc");
			this.User = this.SuperUser.CreateUser("pcresswell", "123", "abc");
		}

		[TearDown]
		public void Teardown()
		{
			this.Database.Dispose();
		}

		protected IUser CreateSuperUser()
		{
			return this.CommonModule.CreateSuperUser("123", "abc");
		}
		protected IUser CreateUser()
		{
			return this.SuperUser.CreateUser("pcresswell", "abc", "123");
		}
	}
}