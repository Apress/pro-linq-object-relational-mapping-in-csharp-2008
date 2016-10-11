using System;
using IDictionary = System.Collections.IDictionary;
using Hashtable = System.Collections.Hashtable;
using IEnumerator = System.Collections.IEnumerator;

namespace BoP.Core
{
	
	/// <summary>
	/// A default implementation of the SystemCodeManager interface.
	/// This implements methods for retrieving the various code types, as well
	/// defines default codes used within the base domain model.
	/// This implementation can be subclassed, and should override the
	/// "loadSystemCodes" method.  
	/// This implementation uses the IDictionary interface to provide a list
	/// of code types and corresponding codes for that code type.
	/// 
	/// </summary>
	public class DefaultSystemCodeManager : ISystemCodeManager
	{
		protected static readonly IDictionary codeTypes = new Hashtable();
		protected static readonly IDictionary codesMap = new Hashtable();

		protected const String PHONE_TYPE = "PHONE_TYPE";
		protected const String EMAIL_TYPE = "EMAIL_TYPE";
		protected const String ADDRESS_TYPE = "ADDRESS_TYPE";
		protected const String GENDER_TYPE = "GENDER";
		protected const String PERSONNAME_TITLE_TYPE = "PERSONNAME_TITLE";
		protected const String PERSONNAME_SUFFIX_TYPE = "PERSONNAME_SUFFIX";
		protected const String CALENDAR_MONTHS_TYPE = "CALENDAR_MONTHS";
		protected const String USER_STATUS_TYPE = "USER_STATUS";
		protected const String BORROWER_STATUS_TYPE = "BORROWER_STATUS";
		protected const String CHARGE_TYPE = "CHARGE_TYPE";
		protected const String COUNTRY_TYPE = "COUNTRY_TYPE";
		protected const String USASTATE_TYPE = "USASTATE_TYPE";



		public DefaultSystemCodeManager():base()
		{
			LoadCoreSystemCodes();
		}

		
		/// <summary>
		/// Return the list of valid code types.
		/// The code types can then be used to retrieve a list
		/// of codes based on the code type value.
		/// </summary>
		/// 
		/// <returns>
		/// the valid list of code types
		/// </returns>
		public virtual CodeType[] GetCodeTypes()
		{
			CodeType[] types = new CodeType[codeTypes.Count];
			IEnumerator e = codeTypes.GetEnumerator();

			for (int i=0;i < types.Length;i++)
			{
				e.MoveNext();
				types[i] = (CodeType)e.Current;
			}

			return types;
		}


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
		/// if there is no such code type object
		/// </exception>
		public virtual CodeType GetCodeType(String type)
		{
			CodeType tmp = (CodeType)codeTypes[type];
			
			if (tmp == null)
				throw new ArgumentException("No such code type " + type);
			
			return tmp;
		}
		

		/// <summary>
		/// Return a list of codes based upon the code type.
		/// This will return all codes based upon the code type.
		/// 
		/// If a code given has a "nextCodeType" value, this indicates
		/// the given code has subservant codes under it.  The subservant codes
		/// can be retrieved by asking for the set of codes based on the next code type value.
		/// 
		/// </summary>
		/// 
		/// <param name="codeType">
		/// the code type value
		/// </param>
		/// 
		/// <returns>
		/// the list of codes based upon the code type
		/// </returns>
		/// 
		/// <exception cref="System.ArgumentException">
		/// If the code type specified cannot be found
		/// </exception>
		public virtual Code[] GetCodes(String codeType)
		{
			Code[] codes = new Code[0];
			
			if (codeType != null)
			{
				Code[] tmp = (Code[]) codesMap[codeType];
				
				if (tmp != null)
					codes = tmp;
				else
					throw new ArgumentOutOfRangeException("Code type specified:  " + codeType + " does not exist");
			}
			else
			{
				throw new ArgumentOutOfRangeException("Code type specified cannot be null");
			}
			
			return (codes);
		}


		/// <summary>
		/// Loads the Base System Codes needed by the core
		/// domain model.  This will then call the 
		/// "loadSystemCodes" method which can the be overriden
		/// by other subclasses to provide thier own types.
		/// </summary>
		///
		private void LoadCoreSystemCodes()
		{
			if (codeTypes.Count <= 0)
			{


				//Codes for phone types
				CodeType phoneType = new CodeType(PHONE_TYPE,"Phone Types",false,true);
				codeTypes.Add(phoneType.GetValue(), phoneType);
				codesMap.Add(PHONE_TYPE, new Code[] {new Code(phoneType,"HOME", "Home"),
																new Code(phoneType,"WORK", "Work"),
																new Code(phoneType,"CELL", "Cell"),
																new Code(phoneType,"MOBILE", "Mobile"),
																new Code(phoneType,"FAX", "Fax")});


				//Codes for email types
				CodeType emailType = new CodeType(EMAIL_TYPE,"Email Types",false,true);
				codeTypes.Add(emailType.GetValue(), emailType);
				codesMap.Add(EMAIL_TYPE, new Code[] {new Code(emailType,"HOME", "Home"),
														  new Code(emailType,"WORK", "Work"),
														  new Code(emailType,"OTHER", "Other")});


				//Codes for address types
				CodeType addrType = new CodeType(ADDRESS_TYPE,"Address Types",false,true);
				codeTypes.Add(addrType.GetValue(), addrType);
				codesMap.Add(ADDRESS_TYPE, new Code[] {new Code(addrType,"HOME", "Home"),
														  new Code(addrType,"WORK", "Work"),
														  new Code(addrType,"OTHER", "Other")});



				//Codes for assigning gender
				CodeType gender = new CodeType(GENDER_TYPE,"Gender",false,true);
				codeTypes.Add(gender.GetValue(), gender);
				codesMap.Add(GENDER_TYPE, new Code[] {new Code(gender,"M", "Male"),
													  new Code(gender,"F", "Female"),
													  new Code(gender,"U", "Unknown")});


				//Codes for suffixes for a name
				CodeType title = new CodeType(PERSONNAME_TITLE_TYPE,"Name Titles",false,true);
				codeTypes.Add(title.GetValue(), title);
				codesMap.Add(PERSONNAME_TITLE_TYPE, new Code[] {new Code(title,"MR", "Mr"),
													new Code(title,"MRS", "Mrs"),
													new Code(title,"MS", "Ms"),
													new Code(title,"DR", "Dr")});


				//Codes for titles for a name
				CodeType suffix = new CodeType(PERSONNAME_SUFFIX_TYPE,"Name Suffix",false,true);
				codeTypes.Add(suffix.GetValue(), suffix);
				codesMap.Add(PERSONNAME_SUFFIX_TYPE, new Code[] {new Code(suffix,"I", "I"),
																 new Code(suffix,"II", "II"),
																 new Code(suffix,"III", "III"),
																 new Code(suffix,"IV", "IV"),
																 new Code(suffix,"V", "V"),
																 new Code(suffix,"JR", "Jr"),
																 new Code(suffix,"SR", "Sr")});

				//Codes for the calendar months in the year
				CodeType months = new CodeType(CALENDAR_MONTHS_TYPE,"Calendar Months",false,true);
				codeTypes.Add(months.GetValue(), months);
				codesMap.Add(CALENDAR_MONTHS_TYPE, new Code[] {new Code(months,"01", "Janurary"),
													new Code(months,"02", "Feburary"),
													new Code(months,"03", "March"),
													new Code(months,"04", "April"),
													new Code(months,"05", "May"),
													new Code(months,"06", "June"),
													new Code(months,"07", "July"),
													new Code(months,"08", "August"),
													new Code(months,"09", "September"),
													new Code(months,"10", "October"),
													new Code(months,"11", "November"),
													new Code(months,"12", "December")});

				//Provides the basic statuses for the user object
				CodeType userStatus = new CodeType(USER_STATUS_TYPE,"User Status",false,true);
				codeTypes.Add(userStatus.GetValue(), userStatus);
				codesMap.Add(USER_STATUS_TYPE, new Code[] {new Code(userStatus,"ACTIVE","Active"),
														   new Code(userStatus,"LOCKED","Locked"),
														   new Code(userStatus,"RESET","Reset"),
														   new Code(userStatus,"EXPIRED","Expired"),
														   new Code(userStatus,"SUSPENDED","Suspended"),
														   new Code(userStatus,"REVOKED","Revoked"),
														   new Code(userStatus,"DISABLED","Disabled")});

				//Provides the basic statuses for the borrower object
				CodeType borrowerStatus = new CodeType(BORROWER_STATUS_TYPE,"Borrower Status",false,true);
				codeTypes.Add(borrowerStatus.GetValue(), borrowerStatus);
				codesMap.Add(BORROWER_STATUS_TYPE, new Code[] {new Code(borrowerStatus,"ACTIVE","Active"),
															   new Code(borrowerStatus,"DISABLED","Disabled"),
															   new Code(borrowerStatus,"DEATH","Deceased"),
															   new Code(borrowerStatus,"BANKRUPT","Bankrupt")});

				//Codes for charge types
				CodeType chargeType = new CodeType(CHARGE_TYPE,"Charge Types",false,true);
				codeTypes.Add(chargeType.GetValue(), chargeType);
				codesMap.Add(CHARGE_TYPE, new Code[] {new Code(chargeType,"LATE_CHARGE", "Late Charge")});

				//Provides the country and state code types/codes through Code "linking"

				CodeType countryType = new CodeType(COUNTRY_TYPE, "Country Type",false,true);
				codeTypes.Add(countryType.GetValue(),countryType);
				CodeType USAStateType = new CodeType(USASTATE_TYPE,"USA States Type",false,true);
				codesMap.Add(COUNTRY_TYPE, new Code[]{new Code(countryType,"US","United States",true,USAStateType)});
				codeTypes.Add(USAStateType.GetValue(), USAStateType);
				codesMap.Add(USASTATE_TYPE,new Code[] {new Code(USAStateType,"AL","Alabama"),
															new Code(USAStateType,"AK","Alaska"),
															new Code(USAStateType,"AZ","Arizona"),
															new Code(USAStateType,"AR","Arkansas"),
															new Code(USAStateType,"CA","California"),
															new Code(USAStateType,"CO","Colorado"),
															new Code(USAStateType,"CT","Connecticut"),
															new Code(USAStateType,"DE","Delaware"),
															new Code(USAStateType,"FL","Florida"),
															new Code(USAStateType,"GA","Georgia"),
															new Code(USAStateType,"HI","Hawaii"),
															new Code(USAStateType,"ID","Idaho"),
															new Code(USAStateType,"IL","Illinois"),
															new Code(USAStateType,"IN","Indiana"),
															new Code(USAStateType,"IA","Iowa"),
															new Code(USAStateType,"KS","Kansas"),
															new Code(USAStateType,"KY","Kentucky"),
															new Code(USAStateType,"LA","Louisiana"),
															new Code(USAStateType,"ME","Maine"),
															new Code(USAStateType,"MH","Marshall Islands"),
															new Code(USAStateType,"MD","Maryland"),
															new Code(USAStateType,"MA","Massachusetts"),
															new Code(USAStateType,"MI","Michigan"),
															new Code(USAStateType,"FM","Micronesia"),
															new Code(USAStateType,"AA","Military(AA)"),
															new Code(USAStateType,"AE","Military(AE)"),
															new Code(USAStateType,"AP","Military(AP)"),
															new Code(USAStateType,"MN","Minnesota"),
															new Code(USAStateType,"MS","Mississippi"),
															new Code(USAStateType,"MO","Missouri"),
															new Code(USAStateType,"MT","Montana"),
															new Code(USAStateType,"NE","Nebraska"),
															new Code(USAStateType,"NV","Nevada"),
															new Code(USAStateType,"NH","New Hampshire"),
															new Code(USAStateType,"NJ","New Jersey"),
															new Code(USAStateType,"NM","New Mexico"),
															new Code(USAStateType,"NY","New York"),
															new Code(USAStateType,"NC","North Carolina"),
															new Code(USAStateType,"ND","North Dakota"),
															new Code(USAStateType,"OH","Ohio"),
															new Code(USAStateType,"OK","Oklahoma"),
															new Code(USAStateType,"OR","Oregon"),
															new Code(USAStateType,"PW","Palau"),
															new Code(USAStateType,"VI","Virgin Islands"),
															new Code(USAStateType,"PA","Pennsylvania"),
															new Code(USAStateType,"RI","Rhode Island"),
															new Code(USAStateType,"SC","South Carolina"),
															new Code(USAStateType,"SD","South Dakota"),
															new Code(USAStateType,"TN","Tennessee"),
															new Code(USAStateType,"TX","Texas"),
															new Code(USAStateType,"UT","Utah"),
															new Code(USAStateType,"VT","Vermont"),
															new Code(USAStateType,"VA","Virginia"),
															new Code(USAStateType,"WA","Washington"),
															new Code(USAStateType,"WV","West Virginia"),
															new Code(USAStateType,"WI","Wisconsin"),
															new Code(USAStateType,"WY","Wyoming"),
															new Code(USAStateType,"DC","District Of Columbia"),
															new Code(USAStateType,"AS","American Samoa"),
															new Code(USAStateType,"GU","Guam"),
															new Code(USAStateType,"CM","Northern Mariana Islands"),
															new Code(USAStateType,"PR","Puerto Rico")});
				LoadSystemCodes();
			}
		}


		/// <summary>
		/// Default method created that is purposely empty.
		/// This can then be overriden to provide specific
		/// codes needed by specific implementations.
		/// 
		/// Implementation providing new code types and codes should do the following:
		/// 1.  Create a new code type giving the code type value and it's description.
		/// 2.  Add the new code type to the "codeTypes" list by calling codeTypes.Add(codetype.getValue(),codeType)
		/// 3.  Add the list of codes under the given codeType by calling the "codesMap.Add(codeType.getValue(),array list of new codes)"
		///     where "array list of new codes" should be a finite array of Code[] objects
		/// </summary>
		protected virtual void LoadSystemCodes()
		{
		}

	}
}