using System;
using Beaker.Core;

namespace Beaker.Module.Common
{
	public class CreateUserCommand
	{
		public CreateUserCommand(IRepositoryIndex index, Database database)
		{
			this.Index = index;
			this.Database = database;
		}

		public virtual IUser Create()
		{
			User user = new User(Guid.NewGuid(), Guid.NewGuid(), this.Username, this.Salt, this.Password);
			user.Repositories = this.Index;
			user.Database = this.Database;
			return user;
		}

		protected IRepositoryIndex Index { get; private set; }

		protected Database Database { get; private set; }

		public string Password { private get; set; }

		public string Salt { private get; set; }

		public string Username { private get; set; }
	}
}
