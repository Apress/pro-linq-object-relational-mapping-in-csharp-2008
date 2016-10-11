using System;

namespace BoP.Core.Domain
{
	
	
	/// <summary>
	/// Provides additional operations that are not defined 
    /// as creational or query methods in the DAO layer.  
	/// 
	/// </summary>
	public interface IUserManager
	{
		
		/// <summary> 
		/// A method that will authenticate a user based on thier
		/// userName and password.  If the userName and password is correct,
		/// the user will be returned with thier person/profile information.
		/// </summary>

		User Authenticate(String userName, String password);

        void CreateUser(User u);

        /// <summary>
        /// Method for retrieving a forgoton password.
        /// An email is sent with the users password.
        /// </summary>
  
        string ForgotPassword(string email);


        /// <summary>
        /// Method for retrieving a forgoton username.
        /// An email is sent with the users UserName.
        /// </summary>

        string ForgotUserName(string email);

		/// <summary>
		/// a method that will logout a given user 
		/// </summary>
		/// <param name="user"></param>
		void Logout(User user);		

	}
}