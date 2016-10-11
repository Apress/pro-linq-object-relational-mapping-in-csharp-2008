using System;

namespace BoP.Core.Domain
{
	
	
	/// <summary>
	/// Provides the operations for working with the domain model  
	/// by implementing business logic to work with the DAO layer.
	/// </summary>
	public interface IUserManager
	{
		
		/// <summary> 
		/// A method that will authenticate a user based on thier
		/// userName and password.  If the userName and password is correct,
		/// the user will be returned with thier person/profile information.
		/// </summary> 
		User Authenticate(String userName, String password);

        /// <summary>
        /// A Method for Creating a new user
        /// This is a helper method to encapsulate DAO functionality
        /// </summary>
        /// <param name="u"></param>
        User CreateUser(User u);

        /// <summary>
        /// Method for retrieving a forgoton password.
        /// An email is sent with the users password.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Password</returns>
        string ForgotPassword(string email);


        /// <summary>
        /// Method for retrieving a forgoton username.
        /// An email is sent with the users UserName.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Emails UserName</returns>
        string ForgotUserName(string email);

		/// <summary>
		/// a method that will logout a given user 
		/// </summary>
		/// <param name="user"></param>
		void Logout(User user);		

	}
}