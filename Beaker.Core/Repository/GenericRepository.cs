namespace Beaker.Core
{
	using System;
	using Beaker.Core;
	using Authorize;
	using System.Collections.Generic;

	/// <summary>
	/// Repository.
	/// </summary>
	public class GenericRepository<TDomain> : Repository where  TDomain : DomainObject 
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Beaker.Data.Repository"/> class.
		/// </summary>
		/// <param name="engine">Engine.</param>
		public GenericRepository(IStorageEngine<TDomain> engine)
		{
			this.Engine = engine;
		}

		private IStorageEngine<TDomain> Engine { get; set; }

		public override void Initialize()
		{
			this.Engine.Initialize();
		}

		internal override Repository Fork(Guid sessionId)
		{
			GenericRepository<TDomain> newInstance = (Beaker.Core.GenericRepository<TDomain>)this.MemberwiseClone();
			newInstance.Engine = this.Engine.Fork(sessionId);
			return newInstance;
		}

		/// <summary>
		/// Adds the specified domainObject and records the given author.
		/// Does nothing if the given record already exists.
		/// Inserts the new record otherwise.
		/// </summary>
		/// <param name="domainObject">Domain object.</param>
		/// <param name="author">Author adding the object. Must have read permission on the object being added.</param>
		internal virtual void Save(TDomain domainObject, IUser author)
		{
			// If the record already exists, then do nothing
			// Otherwise...
			// 1. Get the current IRecord for the same domainObjectID
			// 2. End that record
			// 3. Insert the new record only if it does not exist

			if (Engine.IsRecordPersisted(domainObject.ID))
			{
				return;
			}

			DateTime endDate = DateTime.UtcNow;

			IRecord record = this.Engine.GetRecord(domainObject.DomainObjectID, endDate);

			// Discontinue old record if one exists.
			if (record != null)
			{
				// check that the user has Update permission
				if (!author.CanUpdate(domainObject))
				{
					throw new InsufficientPermissionException("The author " + author.DomainObjectID + " does not have sufficient permissions to update the domain object.");
				}

				record.RecordEnd = endDate;
				record.ValidEnd = endDate;
				this.Engine.Update(record);
			}
			else
			{
				// check that the user has Create permission
				if (!author.CanCreate(domainObject))
				{
					// this should not be possible but double check here.
					throw new InsufficientPermissionException("The author " + author.DomainObjectID + " does not have sufficient permissions to create a new domain object.");
				}
			}

			IRecord newRecord = this.Engine.MapToRecord(domainObject);
			newRecord.AuthorID = author.DomainObjectID;
			newRecord.ID = domainObject.ID;
			newRecord.DomainObjectID = domainObject.DomainObjectID;
			newRecord.RecordStart = endDate;
			newRecord.ValidStart = endDate;
			newRecord.RecordEnd = this.Infinity;
			newRecord.ValidEnd = this.Infinity;

			this.Engine.Insert(newRecord);
		}

		/// <summary>
		/// Removes the specified domainObject. 
		/// </summary>
		/// <param name="domainObject">Domain object.</param>
		internal void Delete(TDomain domainObject, IUser author)
		{
			// If the record does NOT already exists, then do nothing
			// Otherwise...
			// 1. Get the current IRecord for the same domainObjectID
			// 2. End that record
			if (!Engine.IsRecordPersisted(domainObject.ID))
			{
				return;
			}

			// check that the user has Delete permission
			if (!author.CanDelete(domainObject))
			{
				// this should not be possible but double check here.
				throw new InsufficientPermissionException("The author " + author.DomainObjectID + " does not have sufficient permissions to delete the domain object.");
			}

			DateTime endDate = DateTime.UtcNow;

			IRecord record = this.Engine.GetRecord(domainObject.DomainObjectID, endDate);
			record.RecordEnd = endDate;
			record.ValidEnd = endDate;
			this.Engine.Update(record);
		}

		/// <summary>
		/// Returns the domain object as it was on the given date. If no date is
		/// provided then it returns the current version.
		/// </summary>
		/// <param name="domainObjectID">Domain object identifier.</param>
		/// <param name="onDate">The effective date of the domain object.</param>
		internal TDomain Get(Guid domainObjectID, DateTime onDate = default(DateTime))
		{
			TDomain domainObject;
			if (DateTime.MinValue.Equals(default(DateTime)))
			{
				domainObject = this.Engine.GetDomainObject(domainObjectID, DateTime.UtcNow);
			}
			else
			{
				domainObject = this.Engine.GetDomainObject(domainObjectID, onDate);
			}

			if (domainObject == null)
			{
				throw new RecordNotFoundException();
			}

			return domainObject;
		}

		internal override void StartTransaction()
		{
			this.Engine.StartTransaction();
		}

		internal override void CommitTransaction()
		{
			this.Engine.CommitTransaction();
		}

		internal override void RollbackTransaction()
		{
			this.Engine.RollbackTransaction();
		}

		/// <summary>
		/// Represents the infinte end date for a non ending record.
		/// </summary>
		/// <value>Infinity</value>
		private DateTime Infinity
		{
			get
			{
				return new DateTime(3000, 1, 1);
			}
		}
	}
}
