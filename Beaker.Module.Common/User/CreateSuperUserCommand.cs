using System;
using Beaker.Core;

namespace Beaker.Module.Common
{
	public class CreateSuperUserCommand : CreateUserCommand
	{
		public CreateSuperUserCommand(IRepositoryIndex index, Database database) : base(index,database)
		{
			this.Username = "sa";
		}

		public override IUser Create()
		{
			User user = (Beaker.Module.Common.User)base.Create();
			user.PermitAll();
			return user;
		}
	}
}
