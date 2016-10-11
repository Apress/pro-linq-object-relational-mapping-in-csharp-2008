using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Collections;
using IServiceFactory = BoP.Util.IServiceFactory;
using ClassServiceFactory = BoP.Util.ClassServiceFactory;
using ServiceUnavailableException = BoP.Util.ServiceUnavailableException;

namespace BoP.Core
{
	
	/// <summary>
	/// Identifies the various code types within the system.
	/// For every set of codes, there is a parent code type.
	/// 
	/// This class categorizes a group of codes.  A code type can
	/// also be marked as "final".  This means the code type is required
	/// in order for the system to operate correctly and cannot be removed
	/// from the system.  The description, however, can always be changed.
	/// 
	/// So, for example, a code object could represent each country in the world.
	/// And the collection of countries would be grouped together under the code type
	/// "COUNTRIES".
	/// 
	/// </summary>

	public class CodeType
	{
		private const String SYSTEMCODE_MANAGER = "BoP/Core/ISystemCodeManager";

		protected String type;
		protected String description;
		protected bool final;
		protected bool root;
		
		[NonSerialized()]
		private System.Collections.IDictionary codes;
		[NonSerialized()]
		private Code[] codesArray;

		private IServiceFactory serviceFactory;
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public CodeType():this(null, null, false, false)
		{
			codesArray = null;
			codes = null;
		}
		
		
		/// <summary> 
		/// Constructor.  Creates a new code type given the code type value, the description,
		/// whether it's final, and if it's a root code type.
		/// </summary>
		/// 
		/// <param name="type">
		/// the type (or value) of the code type
		/// </param>
		/// 
		/// <param name="description">
		/// the "nice name" or description of the code type
		/// </param>
		/// <param name="isFinal">
		/// is this code type final and should not be changed
		/// </param>
		/// <param name="isRoot">
		/// code types can be chained to create a tree.  This indicates if this code type is the root.
		/// </param>
		public CodeType(String type, String description, bool isFinal, bool isRoot):base()
		{
			this.type = type;
			this.description = description;
			this.final = isFinal;
			this.root = isRoot;
			codesArray = null;
			codes = null;
		}
		


		/// <summary>
		/// Find a Code object from this CodeType's collection.
		/// </summary>
		/// 
		/// <param name="codeValue">
		/// The value or key of the Code
		/// </param>
		/// 
		/// <returns>
		/// the Code object with the corresponding value
		/// </returns>
		/// 
		public Code GetCode(String codeValue)
		{
			//Make sure the codes list is initialized
			GetCodes();
			
			Code tmp = (Code) codes[codeValue];
			
			if (tmp == null)
				throw new ArgumentOutOfRangeException("No such code " + codeValue);
			
			return tmp;
		}
		


		/// <summary>
		/// Determine if there is a corresponding Code object.
		/// </summary>
		/// 
		/// <param name="codeValue">
		/// The value or key of the Code
		/// </param>
		/// 
		/// <returns>
		/// boolean indicating if this is a valid codeValue
		/// </returns>
		/// 
		public bool IsValidCode(String codeValue)
		{
			//Make sure the codes array is initialized
			GetCodes();
			
			return codes.Contains(codeValue);
		}
		

		/// <summary>
		/// Get an array of this CodeType's Code objects.
		/// This is a convenience method that accesses the SystemCodeManager
		/// to attempt to retrieve the codes by the given code type.
		/// </summary>
		/// 
		/// <returns>
		/// array of this CodeType's Code objects
		/// </returns>
		///
		/// <exception cref="BoP.Util.ServiceUnavailableException">
		/// If for some reason the list of codes could not be returned
		/// </exception>
		public Code[] GetCodes()
		{
			lock (this)
			{
				//We keep a codes array along with the hash code because we are
				//preserving order here.  The hashtable cannot guarantee the
				//order like the array can.
				if (codesArray == null)
				{
					if (serviceFactory == null)
						serviceFactory = new ClassServiceFactory();

					try
					{
						ISystemCodeManager systemCodeManager = (ISystemCodeManager)serviceFactory.FindByServiceName(SYSTEMCODE_MANAGER);
						codesArray = systemCodeManager.GetCodes(GetValue());
					}
					catch(Exception exc)
					{
						throw new ServiceUnavailableException(exc.Message);
					}
				}

				//This will initialize our internal codes list
				//that is used by other components in this class
				//It also guarantees thread safety
				if ((codes == null) && (codesArray != null))
				{
					codes = new Hashtable();
					for (int i=0; i < codesArray.Length; i++)
					{
						if (codesArray[i].GetCodeType() == null)
							codesArray[i].SetCodeType(this);

						codes.Add(codesArray[i].GetValue(), codesArray[i]);
					}
				}
					
				return codesArray;
			}
		}


		/// <summary>
		/// Since code types can be linked together through a code object, we need to indicate which
		/// code types are considered roots (ie at the top of the tree).  This attribute is
		/// used to indicate whether the code type is a root code type.
		/// </summary>
		/// 
		/// <returns>
		/// indicates this code type is the root
		/// </returns>
		/// 
		public bool IsRoot()
		{
			return root;
		}

		
		
		/// <summary>
		/// Indicates if this code type is final which means this code type cannot be
		/// removed.
		/// </summary>
		/// 
		/// <returns>
		/// this CodeType is final and should not be changed
		/// </returns>
		/// 
		public bool IsFinal()
		{
			return final;
		}

		
		/// <summary>
		/// The type of this CodeType's collection of Codes
		/// </summary>
		/// 
		/// <returns>
		/// the value for this code type (ie the type)
		/// </returns>
		///
		public String GetValue()
		{
			return type;
		}
	
		

		/// <summary>
		/// The type of this CodeType's collection of Codes
		/// </summary>
		/// 
		/// <param name="type">
		/// type of this CodeType's collection of Codes
		/// </param>
		/// 
		/// <exception cref="System.SystemException">
		/// if the Code is set as final value cannot be modified
		/// </exception>
		/// 
		public void SetValue(String val)
		{
			if (IsFinal())
				throw new SystemException("Code type " + val + " is final, value cannot be modified.");
			this.type = val;
		}

		
		
		/// <summary>
		/// Overriden to provide equality by value, instead of equality 
		/// by reference
		/// </summary>
		/// 
		/// <param name="obj">
		/// the object to match by
		/// </param>
		/// 
		public override bool Equals(Object obj)
		{
			if (obj is CodeType)
			{
				CodeType code = (CodeType) obj;
				if (type.Equals(code.GetValue()))
					return true;
			}
			return false;
		}
		



		/// <summary> 
		/// Overriden here to return the description, or nice name, for the codetype object
		/// </summary>
		/// 
		/// <returns>
		/// the nice name for the code type
		/// </returns>
		/// 
		public override String ToString()
		{
			return description;
		}
	}
}