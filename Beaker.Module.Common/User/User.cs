using System;
using System.Collections;
using System.Collections.Generic;
using Beaker.Core;
using Authorize;

namespace Beaker.Module.Common
{
	public class User : DomainObject, IUser
	{
		internal User(Guid id, Guid domainObjectID, string userName, string salt, string encryptedPassword) : base(id, domainObjectID)
		{
			this.UserName = userName;
			this.Salt = salt;
			this.EncryptedPassword = encryptedPassword;
			this.Permissions = new UserPermission();
		}

		public string UserName { get; private set; }
		public string Salt { get; private set; }
		public string EncryptedPassword { get; private set; }
		protected UserPermission Permissions { get; private set; }
		internal IRepositoryIndex Repositories { get; set; }
		internal Database Database { get; set; }

		public TEditor Editor<TEditor>(object domainObject = null) where TEditor : Editor, new()
		{
			TEditor editor = new TEditor();
			if (domainObject != null)
			{
				editor.EditedObject = domainObject;
			}

			if (this.Can(editor.TypeOfAction, editor.Subject) != true)
			{
				throw new InsufficientPermissionException();
			}

			return editor;
		}

		public void Save<TDomain>(TDomain domainObject) where TDomain : DomainObject
		{
			GenericRepository<TDomain> repository = this.Repositories.GetRepository<TDomain>();
			repository.Save(domainObject, this);
		}

		public void Delete<TDomain>(TDomain domainObject) where TDomain : DomainObject
		{
			var repository = this.Repositories.GetRepository<TDomain>();
			repository.Delete(domainObject, this);
		}

		public bool CanRead(object subject)
		{
			return this.Can(Authorize.Actions.Read, subject) == true;
		}

		public bool CanUpdate(object subject)
		{
			return this.Can(Authorize.Actions.Update, subject) == true;
		}

		public bool CanCreate(object subject)
		{
			return this.Can(Authorize.Actions.Create, subject) == true;
		}

		public bool CanDelete(object subject)
		{
			return this.Can(Authorize.Actions.Delete, subject) == true;
		}

		public bool CanShare(object subject)
		{
			return this.Can(Authorize.Actions.Share, subject) == true;
		}

		public IUser CreateUser(string username, string password, string salt)
		{
			if (!this.CanCreate(typeof(User)))
			{
				throw new InsufficientPermissionException();
			}

			CreateUserCommand createUser = new CreateUserCommand(this.Repositories, this.Database)
			{
				Username = username,
				Password = password,
				Salt = salt
			};

			return createUser.Create();
		}

		public TDomain Find<TDomain>(Guid domainObjectID) where TDomain : DomainObject
		{
			return this.Repositories.GetRepository<TDomain>().Get(domainObjectID);
		}

		public void StartTransaction()
		{
			this.Database.StartTransaction();
		}

		public void CommitTransaction()
		{
			this.Database.CommitTransaction();
		}

		public void RollbackTransaction()
		{
			this.Database.RollbackTransaction();
		}

		internal void PermitAll()
		{
			this.Permissions.AddAuthorization(Authorize.Actions.Manage);
		}

		private bool? Can(Authorize.Action action, object subject)
		{
			return this.Permissions.Can(action, subject);
		}

	}
}
