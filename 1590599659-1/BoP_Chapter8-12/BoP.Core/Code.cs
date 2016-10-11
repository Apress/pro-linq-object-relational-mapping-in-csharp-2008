using System;


namespace BoP.Core
{
	
	
	/// <summary>
	/// Provides a representation of an entry in a "lookup table".
	/// This is used in conjuction with the System Code Manager.
	/// A code object provides a value and the corresponding description of that value.
	/// Since some codes are dependent for the system to operate correctly, some codes can be
	/// marked "final".
	/// This means the code description can be changed, but the code itself cannot be removed.
	/// 
	/// Codes can also be strung together to form a hiararchy or tree.  Each
	/// code object can have a next "code type".  This means if the code is chosen
	/// the next code type can be found and a list of codes for that code type.
	/// 
	/// For example, there can be a list of codes for the countries.  For each code
	/// for a given country, it can have a code type of "STATES".  For the code type
	/// "STATES", there could be a collection of the states for the country, and so on.
	/// 
	/// 
	/// </summary>

	public class Code
	{
		protected CodeType type = null;
		protected string theValue = "";
		protected string description = "";
		protected bool final = false;
		protected CodeType nextCodeType = null;
		
		/// <summary> Default Constructor.</summary>
		public Code():base()
		{
		}
		
		
		
		
		/// <summary> 
		/// Constructor.  Creates a new code given the parent code type, the value, the
		/// description of the code, if it's final (cannot be changed) and the next code type
		/// 
		/// </summary>
		/// <param name="type	the">parent code type
		/// </param>
		/// <param name="value">the value of the code
		/// </param>
		/// <param name="description">the "nice name" or description of the code
		/// </param>
		/// <param name="isFinal">is this code final and should not be changed
		/// </param>
		/// <param name="nextCodeType	the">next code type pointer.  used to create a code tree.
		/// 
		/// </param>
		public Code(CodeType type, string val, string description, bool isFinal, CodeType nextCodeType)
		{
			this.type = type;
			this.theValue = val;
			this.description = description;
			this.final = isFinal;
			this.nextCodeType = nextCodeType;
		}
		
		
		/// <summary> 
		/// A convenience constructor.  The type, value, and description are defined.  By default,
		/// the code is NOT final and it does not have a next code type.
		/// 
		/// </summary>
		/// <param name="type	the">parent code type
		/// </param>
		/// <param name="value">the value of the code
		/// </param>
		/// <param name="description">the "nice name" or description of the code
		/// 
		/// </param>
		public Code(CodeType type, string val, string description):this(type, val, description, false, null)
		{
		}
		
		
		/// <summary> 
		/// Returns the description for the code.
		/// This is usually the "nice name" for the code
		/// </summary>
		/// 
		/// <returns>
		/// the description of the code
		/// </returns>
		public string GetDescription()
		{
			return description;
		}
		

	
		/// <summary> 
		/// Sets the description for the code.
		/// This is usually the "nice name" for the code.
		/// </summary>
		/// 
		/// <param name="desc">
		/// description of the code
		/// </param>	
		public void SetDescription(string desc)
		{
			description = desc;
		}


		
		/// <summary> 
		/// Returns the value for the code.  
		/// </summary>
		/// 
		/// <returns>
		/// the value for the code
		/// </returns>
		public string GetValue()
		{
			return theValue;
		}
		
	
		/// <summary> 
		/// Sets the code value.  
		/// 
		/// </summary>
		/// 
		/// <param name="value">
		/// value for the code
		/// </param>
		/// 
		/// <exception cref="System.SystemException">
		/// if the code is set as final and the 
		/// value cannot be modified
		/// </exception>
		public void SetValue(string val)
		{
			if (IsFinal())
				throw new SystemException("Code " + theValue + " is final, value cannot be modified.");
				
				theValue = val;

		}


		/// <summary>
		/// Is this code object final.  A final code cannot be removed
		/// from the code type.
		/// 
		/// </summary>
		/// <returns>
		/// Is this code final
		/// </returns>
		public bool IsFinal()
		{
			return final;
		}
			

		/// <summary>
		/// Returns the parent code type for this code.  Every code should
		/// belong to a code type.
		/// </summary>
		/// 
		/// <returns>
		/// the parent code type of this code object.
		/// </returns>
		public CodeType GetCodeType()
		{
			return type;
		}
		

	
		/// <summary>
		/// Sets the parent code type for this code.  Every code should
		/// have a code type associated.
		/// </summary>
		///
		/// <param name="type">
		/// the parent code type
		/// </param>
		public void SetCodeType(CodeType type)
		{
			this.type = type;
		}


		/// <summary>
		/// Returns the next code type.  This allows a code
		/// to be linked to another code type to create a tree
		/// </summary>
		/// 
		/// <returns>
		/// the next code type, if one exists
		/// </returns>
		///
		public CodeType GetNextCodeType()
		{
			return nextCodeType;
		}

	
		/// <summary>
		/// Sets the next code type.  This allows a code
		/// to be linked to another code type to create a tree.
		/// 
		/// </summary>
		/// <param name="nextCodeType">
		/// sets the next code type, if applicable
		/// </param>
		public void SetNextCodeType(CodeType nType)
		{	
			this.nextCodeType = nType;
		}


		/// <summary> 
		/// Indicates if the code object has a next code type.
		/// </summary>
		/// <returns>
		/// does this code object link to another code type
		/// </returns>
		public bool HasNextCodeType()
		{
			return (nextCodeType != null);
		}
		
		
	
		/// <summary> 
		/// Overriden here to return the description, or nice name, for the code object
		/// </summary>
		/// 
		/// <returns>
		/// the nice name for the code 
		/// </returns>
		/// 
		public override string ToString()
		{
			return description;
		}
		
		
		/// <summary> 
		/// Overriden here to return the description, or nice name, for the code object
		/// </summary>
		/// 
		/// <returns>
		/// the nice name for the code 
		/// </returns>
		/// 
		public override bool Equals(Object obj)
		{
			bool equal = false;
			if (obj is Code)
			{
				Code code = (Code) obj;
				equal = (type != null && type.Equals(code.GetCodeType()));

				if (equal)
					equal = (((theValue != null) && (code.GetValue() != null) && (theValue.Equals(code.GetValue()))) || ((theValue == null) && (code.GetValue() == null)));
				
				if (equal)
					equal = (((description != null) && (code.GetDescription() != null) && (description.Equals(code.GetDescription()))) || ((description == null) && (code.GetDescription() == null)));
			}
			return equal;
		}
	}
}