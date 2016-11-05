using System;
using System.Collections.Generic;
using Beaker.Core;
using Beaker.Module;
using Beaker.Data.SQLite;

namespace Beaker.Module.Common
{
	public class UserStorageEngine : 
	SQLiteStorageEngine<User, UserTable>, IStorageEngine<User>
	{
		public UserStorageEngine(IConnection connector) : base(connector)
		{
		}

		protected override UserTable MapToTable(User domainObject)
		{
			return new UserTable()
			{
				ID = domainObject.ID,
				DomainObjectID = domainObject.DomainObjectID,
				EncryptedPassword = domainObject.EncryptedPassword,
				Salt = domainObject.Salt,
				UserName = domainObject.UserName
			};
		}

		protected override User MapToDomainObject(UserTable table)
		{
			return new User(
				table.ID,
				table.DomainObjectID,
				table.UserName,
				table.Salt,
				table.EncryptedPassword);
		}

		protected override SQLiteStorageEngine<User, UserTable> NewInstance(IConnection connector)
		{
			return new UserStorageEngine(connector);
		}
	}
}
