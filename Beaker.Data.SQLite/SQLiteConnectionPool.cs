using System;
using System.Collections.Generic;

namespace Beaker.Data.SQLite
{
	/// <summary>
	/// Creates BeakerSQLiteConnection objects per session
	/// </summary>
	public class SQLiteConnectionPool
	{
		private object sessionLock = new object();

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Beaker.Data.SQLite.SQLiteConnectionPool"/> class.
		/// </summary>
		/// <param name="databasePath">SQLite Database path.</param>
		public SQLiteConnectionPool(string databasePath)
		{
			this.DatabasePath = databasePath;
			this.Connections = new Dictionary<Guid, BeakerSQLiteConnection>();
		}

		/// <summary>
		/// Connection Dictionary by sessionId.
		/// </summary>
		/// <value>The connections.</value>
		private IDictionary<Guid, BeakerSQLiteConnection> Connections { get; set; }

		/// <summary>
		/// Gets or sets the database path.
		/// </summary>
		/// <value>The database path.</value>
		private string DatabasePath { get; set; }

		/// <summary>
		/// Gets the session by the given sessionId. 
		/// If there is no session, a new one is created.
		/// </summary>
		/// <returns>The session.</returns>
		/// <param name="sessionId">Session identifier.</param>
		public BeakerSQLiteConnection GetSession(Guid sessionId)
		{
			lock (sessionLock)
			{
				if (this.Connections.ContainsKey(sessionId))
				{
					return this.Connections[sessionId];
				}

				BeakerSQLiteConnection newConnection = new BeakerSQLiteConnection(this.DatabasePath);
				this.Connections[sessionId] = newConnection;
				return newConnection;
			}
		}

		/// <summary>
		/// Dispose the specified session.
		/// </summary>
		/// <param name="sessionId">Session identifier.</param>
		public void Dispose(Guid sessionId)
		{
			lock (sessionLock)
			{
				if (this.Connections.ContainsKey(sessionId))
				{
					var connection = this.Connections[sessionId];
					this.Connections.Remove(sessionId);
					connection.Dispose();
				}
			}
		}
	}
}
