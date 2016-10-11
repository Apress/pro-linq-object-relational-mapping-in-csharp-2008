using System;

namespace Slm.Actor
{
	
	/// <summary>
	/// Used as a base exception class for exception thrown from the user manager, especially when dealing
	/// with authentication
	/// </summary>
	/// 
	public class AuthenticateException:ApplicationException
	{
		
		/// <summary>
		/// Creates new AuthenticateException without detail message.
		/// </summary>
		public AuthenticateException()
		{
		}
		
		
		/// <summary>
		/// Constructs a new AuthenticateException with the specified detail message.
		/// </summary>
		/// 
		/// <param name="msg">
		/// the detail message.
		/// </param>
		/// 
		public AuthenticateException(String msg):base(msg)
		{
		}
	
		/// <summary>
		/// Constructs AuthenticateException with detail message and inner exception
		/// </summary>
		/// 
		/// <param name="msg">
		/// Detail message
		/// </param>
		/// <param name="inner">
		/// Exception to wrap
		/// </param>
		public AuthenticateException(string msg, Exception inner) : base(msg, inner)
		{
		}

	}
}