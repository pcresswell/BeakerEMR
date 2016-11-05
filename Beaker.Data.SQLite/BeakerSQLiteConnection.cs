using System;
using SQLite;
using Beaker.Core;
namespace Beaker.Data.SQLite
{
	/// <summary>
	/// A SQLiteConnection wrapper for Beaker SQLite operations.
	/// </summary>
	public class BeakerSQLiteConnection : ITransactable
	{
		private int transactionRequestCount = 0;
		private object transactionLock = new object();
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Beaker.Data.SQLite.BeakerSQLiteConnection"/> class.
		/// </summary>
		/// <param name="databasePath">Database path.</param>
		/// <param name="storeDateTimeAsTicks">If set to <c>true</c> store date time as ticks.</param>
		internal BeakerSQLiteConnection(string databasePath, bool storeDateTimeAsTicks = true)
		{
			this.Connection = new SQLiteConnection(databasePath, storeDateTimeAsTicks);
		}

		/// <summary>
		/// Gets or sets the connection.
		/// </summary>
		/// <value>The connection.</value>
		private SQLiteConnection Connection { get; set; }

		/// <summary>
		/// Is the record persisted.
		/// </summary>
		/// <returns><c>true</c>, if record is persisted, <c>false</c> otherwise.</returns>
		/// <param name="id">Identifier.</param>
		/// <typeparam name="TTable"></typeparam>
		internal bool IsRecordPersisted<TTable>(Guid id) where TTable : Table, new()
		{
			return this.Connection.Find<TTable>(id) != null;
		}

		/// <summary>
		/// Finds the by record date. Returns null if no record found.
		/// </summary>
		/// <returns>The by record date.</returns>
		/// <param name="domainObjectID">Domain object identifier.</param>
		/// <param name="recordDate">Record date.</param>
		/// <typeparam name="TTable">The Table Type</typeparam>
		internal TTable FindByRecordDate<TTable>(Guid domainObjectID, DateTime recordDate) where TTable : Table, new()
		{
			return this.FindAllByRecordDate<TTable>(recordDate).Where(x => x.DomainObjectID.Equals(domainObjectID)).FirstOrDefault();
		}

		/// <summary>
		/// Begins the transaction. 
		/// </summary>
		public void StartTransaction()
		{
			lock (transactionLock)
			{

				if (!this.Connection.IsInTransaction)
				{
					this.Connection.BeginTransaction();
				}
			}

		}

		/// <summary>
		/// Commits the transaction.
		/// </summary>
		public void CommitTransaction()
		{
			lock (transactionLock)
			{
				this.DecrementTransactionCount();
				if (this.Connection.IsInTransaction && this.transactionRequestCount.Equals(0))
				{
					this.Connection.Commit();
				}
			}
		}

		/// <summary>
		/// Rollbacks the transaction.
		/// </summary>
		public void RollbackTransaction()
		{
			lock(transactionLock)
			{
				this.transactionRequestCount = 0;
				this.Connection.Rollback();
			}
		}


		/// <summary>
		/// Releases all resource used by the <see cref="T:Beaker.Data.SQLite.BeakerSQLiteConnection"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the
		/// <see cref="T:Beaker.Data.SQLite.BeakerSQLiteConnection"/>. The <see cref="Dispose"/> method leaves the
		/// <see cref="T:Beaker.Data.SQLite.BeakerSQLiteConnection"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the
		/// <see cref="T:Beaker.Data.SQLite.BeakerSQLiteConnection"/> so the garbage collector can reclaim the memory that the
		/// <see cref="T:Beaker.Data.SQLite.BeakerSQLiteConnection"/> was occupying.</remarks>
		public void Dispose()
		{
			this.Connection.Dispose();
		}

		/// <summary>
		/// Query the table
		/// </summary>
		/// <typeparam name="TTable">The 1st type parameter.</typeparam>
		internal TableQuery<TTable> Table<TTable>() where TTable : Table, new()
		{
			return this.Connection.Table<TTable>();
		}

		/// <summary>
		/// Update the record.
		/// </summary>
		/// <param name="record">Record.</param>
		internal int Update(IRecord record)
		{
			return this.Connection.Update(record);
		}

		/// <summary>
		/// Creates the table.
		/// </summary>
		/// <returns>The table.</returns>
		/// <param name="flags">Flags.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		internal int CreateTable<TTable>(CreateFlags flags) where TTable : Table
		{
			return this.Connection.CreateTable<TTable>(flags);
		}

		internal int Insert<TTable>(TTable newRecord) where TTable : Table
		{
			return this.Connection.Insert(newRecord);
		}
		/// <summary>
		/// Finds all by record date.
		/// </summary>
		/// <returns>The all by record date.</returns>
		/// <param name="recordDate">Record date.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		private TableQuery<T> FindAllByRecordDate<T>(DateTime recordDate) where T : Table, new()
		{
			return this.Connection.Table<T>().Where(x => (x.ValidStart <= recordDate)
					   && (recordDate <= x.ValidEnd)
					   && (x.RecordStart <= recordDate)
					   && (recordDate <= x.RecordEnd));
		}

		private void IncrementTransactionCount()
		{
			this.transactionRequestCount++;
		}

		private void DecrementTransactionCount()
		{
			this.transactionRequestCount--;
			this.transactionRequestCount = Math.Max(0, transactionRequestCount);
		}
	}
}

