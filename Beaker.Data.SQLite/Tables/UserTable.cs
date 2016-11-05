using System;
using SQLite;
namespace Beaker.Data.SQLite
{
	[Table("users")]
	public class UserTable : Table
	{
		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		/// <value>The username.</value>
		[Column("username")]
		[NotNull]
		[Unique]
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		[Column("encrypted_password")]
		[NotNull]
		public string EncryptedPassword { get; set; }

		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		/// <value>The email address.</value>
		[Column("email_address")]
		[NotNull]
		[Unique]
		public string EmailAddress { get; set; }

		[Column("salt")]
		[NotNull]
		public string Salt { get; set; }
	}
}