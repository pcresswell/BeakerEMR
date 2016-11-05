using System;
namespace Beaker.Data.SQLite
{
	public interface IConnection
	{
		BeakerSQLiteConnection GetConnection();
		BeakerSQLiteConnection GetConnection(Guid sessionId);
	}
}
