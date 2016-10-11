using System;

namespace BoP.Core.Domain
{
	
	
	/// <summary>
	/// Defines basic methods for creating a valid user, or finding
	/// a user based on various criteria.
	/// This interface can be extended to provide extra methods if
	/// needed.
	/// 
	/// </summary>
	public interface IUserFactory
	{

		/// <summary> 
		/// A method that creates a valid user object.  If a valid
		/// user object cannot be created, a CreateException should be thrown.
		/// </summary>

		User Create();



		/// <summary> 
		/// Creates a valid user object setting the user name and password provided.  If a valid
		/// user object cannot be created, a CreateException should be thrown.
		/// </summary>
		
		User Create(String userName, String password);
		



		/// <summary> 
		/// Finds and returns a user based on a username.  If the user is not found,
		/// ObjectNotFoundException is thrown.  Any other exceptions should throw a 
		/// FinderException.
		/// </summary>
		
		User FindByUserName(String username);


	}
}