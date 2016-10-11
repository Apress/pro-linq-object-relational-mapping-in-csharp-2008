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
		
		public UserDisabledException():base()
		{
		}
		
		
		public UserDisabledException(String msg):base(msg)
		{
		}


		public UserDisabledException(string msg, Exception inner) : base(msg, inner)
		{
		}

	}
}