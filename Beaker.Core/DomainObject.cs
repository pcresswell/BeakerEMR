using System;
using System.Reflection;
namespace Beaker.Core
{
	public abstract class DomainObject
	{
		protected DomainObject(Guid id, Guid domainObjectID)
		{
			if (Guid.Empty.Equals(id))
			{
				throw new ArgumentException("The ID cannot be an empty GUID", "id");
			}

			if (Guid.Empty.Equals(domainObjectID))
			{
				throw new ArgumentException("The domainObjectID cannot be an empty GUID", "domainObjectID");
			}

			this.ID = id;
			this.DomainObjectID = domainObjectID;
			this.IsDeleted = false;
		}

		public Guid ID { get; private set; }

		public Guid DomainObjectID { get; private set; }

		internal bool IsDeleted { get; private set; }

		internal DomainObject Delete()
		{
			this.IsDeleted = true;
			return this;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (obj is DomainObject)
			{
				DomainObject other = (DomainObject)obj;
				return other.ID.Equals(this.ID);
			}
				
			return false;
		}

		public override int GetHashCode()
		{
			return this.ID.GetHashCode();
		}
	}
}

