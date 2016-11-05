using System;
using Beaker.Core;
using Beaker.Data.SQLite;

namespace Beaker.Module.Common
{
	public class PatientStorageEngine : SQLiteStorageEngine<Patient, PatientTable>
	{
		public PatientStorageEngine(IConnection connection) : base(connection)
		{
		}

		protected override Patient MapToDomainObject(PatientTable table)
		{
			return Patient.Load(table.ID, table.DomainObjectID, table.FirstName, table.LastName);
		}

		protected override PatientTable MapToTable(Patient domainObject)
		{
			return new PatientTable()
			{
				ID = domainObject.ID,
				DomainObjectID = domainObject.DomainObjectID,
				FirstName = domainObject.FirstName,
				LastName = domainObject.LastName,
			};
		}

		protected override SQLiteStorageEngine<Patient, PatientTable> NewInstance(IConnection connector)
		{
			return new PatientStorageEngine(connector);
		}
	}
}
