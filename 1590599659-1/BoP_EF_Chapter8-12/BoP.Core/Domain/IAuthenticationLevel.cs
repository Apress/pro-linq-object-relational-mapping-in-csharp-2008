using System;
using IDictionaryEnumerator = System.Collections.IDictionaryEnumerator;

namespace BoP.Core.Domain
{
	/// <summary>
	/// An interface that defines methods for granting an authentication level to an existing user.
	/// The UserManager uses this interface for determining if a user qualifies for the authentication
	/// level this instance manages.  Each project will need to implement this interface if the
	/// application will allow granting of authentication levels higher than level 1.
	/// </summary>
	public interface IAuthenticationLevel
	{


		/// <summary> 
		/// Indicates if the user given is granted the 
		/// authentication level this instance manages.
		/// </summary>
		///
		/// <param name="user">
		/// the user to be granted this authentication level
		/// </param>
		/// 
		/// <returns>
		/// Is the user provided has been granted this authentication level 
		/// </returns>
		bool IsGranted(User user);



		/// <summary> 
		/// Returns the level this authentication level manages.  This
		/// authentication level must be one of the authentication levels defined
		/// </summary>
		///
		/// <returns>
		/// The authentication level this instance manages 
		/// </returns>
		int GetLevel();




	}
}
