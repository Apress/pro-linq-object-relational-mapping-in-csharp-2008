using System;
namespace BoP.Core
{
	
	/// <summary>
	/// Thrown when it is determined the user is in a disabled state
	/// Disabled users are typically when users have decided to disable themselves
	/// from accessing the system anymore.
	/// </summary>
	public class UserDisabledException:ApplicationException
	{
		
		/// <summary>
		/// Creates new UserDisabledException without detail message.
		/// </summary>
		public UserDisabledException():base()
		{
		}
		
		
		/// <summary>
		/// Constructs an UserDisabledException with the specified detail message.
		/// </summary>
		/// 
		/// <param name="msg">
		/// the detail message.
		/// </param>
		public UserDisabledException(String msg):base(msg)
		{
		}

		/// <summary>
		/// Constructs Exception with detail message and inner exception
		/// </summary>
		/// <param name="msg">
		/// Detail message
		/// </param>
		/// <param name="inner">
		/// Exception to wrap
		/// </param>
		public UserDisabledException(string msg, Exception inner) : base(msg, inner)
		{
		}

	}
}