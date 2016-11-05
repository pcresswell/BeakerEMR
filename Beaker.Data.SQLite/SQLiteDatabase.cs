using System;
using Beaker.Core;

namespace Beaker.Data.SQLite
{
	public class SQLiteDatabase : Database, IConnection
	{
		public SQLiteDatabase(string databasePath) : base()
		{
			this.ConnectionPool = new SQLiteConnectionPool(databasePath);
		}

		public BeakerSQLiteConnection BeakerSQLiteConnection
		{
			get
			{
				return this.ConnectionPool.GetSession(this.CurrentSessionID);
			}
		}

		private SQLiteConnectionPool ConnectionPool { get; set; }

		public BeakerSQLiteConnection GetConnection()
		{
			return this.BeakerSQLiteConnection;
		}

		public void Dispose()
		{
			this.BeakerSQLiteConnection.Dispose();
		}

		public BeakerSQLiteConnection GetConnection(Guid sessionId)
		{
			return this.ConnectionPool.GetSession(sessionId);
		}
	}
}
