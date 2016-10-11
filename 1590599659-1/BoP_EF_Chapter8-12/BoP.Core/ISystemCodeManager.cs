using System;
using IDictionary = System.Collections.IDictionary;
using Hashtable = System.Collections.Hashtable;
using IEnumerator = System.Collections.IEnumerator;

namespace BoP.Core
{
	
	/// <summary>
	/// An interface that defines how system codes can be 
	/// retrieved for an application and it's domain model.
	/// These system codes are usually used to verify values that
	/// are set on domain object within the model, and can also
	/// be used for the presentation layer when presenting a finite list
	/// of values to a user.
	/// Please refer to the "Code" and "CodeType" objects to see
	/// how this interface along with these objects can be used
	/// to retrieve various code values.
	/// 
	/// </summary>
	public interface ISystemCodeManager
	{
		
		/// <summary>
		/// Return the list of valid code types.
		/// The code types can then be used to retrieve a list
		/// of codes based on the code type value.
		///
		/// A code type is a way to categorize a group of codes.
		/// For example, the code type for the countries of the world
		/// could be "COUNTRIES".
		/// A code type for the list of states for the US could be
		/// "UNITED_STATES".
		///  
		/// Note that some of the code types defined may not be root
		/// code types, but instead subserviants.  Subservient code types
		/// are code types that exist for a code object to fulfill the 
		/// code's "next code type".
		/// 
		/// </summary>
		/// 
		/// <returns>
		/// the valid list of code types for this system code manager
		/// </returns>
		/// 
		CodeType[] GetCodeTypes();



		/// <summary>
		/// Returns the specified CodeType object.
		/// The code types can then be used to retrieve Code objects.
		/// </summary>
		/// 
		/// <param name="type">
		/// String constant of the code type
		/// </param>
		/// 
		/// <returns>
		/// the CodeType object for the specified type
		/// </returns>
		/// 
		/// <exception cref="System.ArgumentException">
		/// thrown if the type is not a valid code type value
		/// </exception>
		CodeType GetCodeType(String type);

		

		/// <summary>
		/// This will return all codes based upon the given code type.
		/// </summary>
		/// 
		/// <param name="codeType">
		/// code type value
		/// </param>
		/// 
		/// <returns>
		/// the list of codes based upon the code type
		/// </returns>
		/// 
		/// <exception cref="System.ArgumentException">
		/// the code type value specified is invalid
		/// </exception>
		Code[] GetCodes(String codeType);


	}
}