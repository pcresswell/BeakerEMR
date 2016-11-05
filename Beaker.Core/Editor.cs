using System;
using Beaker.Core;
namespace Beaker.Core
{
	public abstract class Editor
	{
		protected Editor()
		{
		}

		private object editedObject;

		public object EditedObject
		{
			get
			{
				return this.editedObject;
			}
		    set
			{
				this.editedObject = value;
				this.OnEditedObjectChanged();
			}
		}

		protected virtual void OnEditedObjectChanged()
		{
		}

		/// <summary>
		/// Simple helper method to cast subject to type
		/// </summary>
		/// <returns>The subject as.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		protected T GetSubjectAs<T>() where T : DomainObject
		{
			return (T)this.EditedObject;
		}

		public virtual object Subject
		{
			get
			{
				return this.EditedObject;
			}
		}

		public Authorize.Action TypeOfAction
		{
			get
			{
				if (this.GetType().Name.StartsWith("Read"))
				{
					return new Authorize.Read(this.Subject);
				}
				else if (this.GetType().Name.StartsWith("Create"))
				{
					return Authorize.Actions.Create;
				}
				else if (this.GetType().Name.StartsWith("Update"))
				{
					return new Authorize.Update(this.Subject);
				}

				return null;
			}
		}
	}
}
