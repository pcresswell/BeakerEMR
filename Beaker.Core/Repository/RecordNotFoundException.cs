using System;
namespace Beaker.Core
{
	public class RecordNotFoundException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RecordNotFoundException"/> class
		/// </summary>
		public RecordNotFoundException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:RecordNotFoundException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		public RecordNotFoundException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:RecordNotFoundException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. </param>
		public RecordNotFoundException (string message, Exception inner) : base(message, inner)
		{
		}
	}
}
