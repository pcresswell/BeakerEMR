using System;
namespace Beaker.Core
{
	public class InsufficientPermissionException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MyException"/> class
		/// </summary>
		public InsufficientPermissionException() : this("You do not have the required permission to perform this action")
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:MyException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		public InsufficientPermissionException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:MyException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. </param>
		public InsufficientPermissionException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}

