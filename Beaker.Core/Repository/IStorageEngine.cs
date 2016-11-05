using System;
using Beaker.Core;
using System.Collections.Generic;

namespace Beaker.Core
{
	/// <summary>
	/// Storage engine that stores the records and maps domain objects to the record type.
	/// </summary>
	public interface IStorageEngine<TDomain> : ITransactable where TDomain : DomainObject 
	{
		/// <summary>
		/// Indicates if the record already exists in the storage engine
		/// </summary>
		/// <returns><c>true</c>, if already persisted was recorded, <c>false</c> otherwise.</returns>
		/// <param name="id">Identifier.</param>
		bool IsRecordPersisted(Guid id);

		/// <summary>
		/// Initialize the engine.
		/// </summary>
		void Initialize();
		/// <summary>
		/// Gets the record for the given domainObjectID as recorded on the provided date.
		/// </summary>
		/// <returns>The record.</returns>
		/// <param name="domainObjectID">Domain object identifier.</param>
		/// <param name="recordDate">Record date.</param>
		IRecord GetRecord(Guid domainObjectID, DateTime recordDate);

		/// <summary>
		/// Update the specified record in storage.
		/// </summary>
		/// <param name="record">Record.</param>
		void Update(IRecord record);
		/// <summary>
		/// Converts the given domain object into the equivalent record.
		/// Does not persist the record.
		/// </summary>
		/// <returns>The equivalent record.</returns>
		/// <param name="domainObject">Domain object to be mapped.</param>
		IRecord MapToRecord(TDomain domainObject);

		/// <summary>
		/// Insert the specified newRecord in storage.
		/// </summary>
		/// <param name="newRecord">New record.</param>
		void Insert(IRecord newRecord);

		/// <summary>
		/// Gets the domain object as it was on the effective date.
		/// </summary>
		/// <returns>The domain object.</returns>
		/// <param name="domainObjectID">Domain object identifier.</param>
		/// <param name="onDate">The record date.</param>
		TDomain GetDomainObject(Guid domainObjectID, DateTime recordDate);
	
		/// <summary>
		/// Creates a new instance of the engine with the given session id
		/// </summary>
		/// <param name="sessionId">Session identifier.</param>
		IStorageEngine<TDomain> Fork(Guid sessionId);
	}
}

