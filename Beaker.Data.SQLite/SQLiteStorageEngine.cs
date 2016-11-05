using System;
using Beaker.Core;
using Beaker.Data;
using SQLite;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Beaker.Data.SQLite
{
	public abstract class SQLiteStorageEngine<TDomain, TTable> : IStorageEngine<TDomain> where TDomain : DomainObject where TTable : Table, new()
	{
		protected SQLiteStorageEngine(IConnection connector)
		{
			this.Connection = connector.GetConnection();
			this.Connector = connector;
		}

		private IConnection Connector { get; set; }

		internal BeakerSQLiteConnection Connection { get; private set; }

		protected abstract TDomain MapToDomainObject(TTable table);

		protected abstract SQLiteStorageEngine<TDomain, TTable> NewInstance(IConnection connector);

		protected abstract TTable MapToTable(TDomain domainObject);

		private TDomain GetDomainObject(Guid domainObjectID, DateTime recordDate)
		{
			var domainObject = this.Connection.FindByRecordDate<TTable>(domainObjectID, recordDate);
			if (domainObject == null)
			{
				return null;
			}

			return this.MapToDomainObject(domainObject);
		}

		bool IStorageEngine<TDomain>.IsRecordPersisted(Guid id)
		{
			return this.Connection.IsRecordPersisted<TTable>(id);
		}

		void IStorageEngine<TDomain>.Initialize()
		{
			this.Connection.CreateTable<TTable>(CreateFlags.None);
		}

		IRecord IStorageEngine<TDomain>.GetRecord(Guid domainObjectID, DateTime recordDate)
		{
			return this.Connection.FindByRecordDate<TTable>(domainObjectID, recordDate);
		}

		void IStorageEngine<TDomain>.Update(IRecord record)
		{
			this.Connection.Update(record);
		}

		IRecord IStorageEngine<TDomain>.MapToRecord(TDomain domainObject)
		{
			return this.MapToTable(domainObject);
		}

		void IStorageEngine<TDomain>.Insert(IRecord newRecord)
		{
			this.Connection.Insert((TTable)newRecord);
		}

		TDomain IStorageEngine<TDomain>.GetDomainObject(Guid domainObjectID, DateTime recordDate)
		{
			return this.GetDomainObject(domainObjectID, recordDate);
		}

		IStorageEngine<TDomain> IStorageEngine<TDomain>.Fork(Guid sessionId)
		{
			PrivateConnector connector = new PrivateConnector(sessionId, this.Connector);
			return this.NewInstance(connector);
		}

		void ITransactable.StartTransaction()
		{
			this.Connection.StartTransaction();
		}

		void ITransactable.CommitTransaction()
		{
			this.Connection.CommitTransaction();
		}

		void ITransactable.RollbackTransaction()
		{
			this.Connection.RollbackTransaction();
		}

		private class PrivateConnector : IConnection
		{
			public PrivateConnector(Guid sessionId, IConnection connection)
			{
				this.SessionID = sessionId;
				this.Connection = connection;
			}
			private Guid SessionID { get; set; }
			private IConnection Connection { get; set; }

			public BeakerSQLiteConnection GetConnection()
			{
				return this.GetConnection(this.SessionID);
			}

			public BeakerSQLiteConnection GetConnection(Guid sessionId)
			{
				return this.Connection.GetConnection(sessionId);
			}
		}
	}
}
