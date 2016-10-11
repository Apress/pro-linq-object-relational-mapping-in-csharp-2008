using System;
using System.Runtime.Serialization;

namespace BoP.Util
{
	
	/// <summary> 
	/// The FinderException exception must be included in the throws clause of every findMETHOD(...) in a factory
	/// class. 
	/// 
	/// The exception is used as a standard application-level exception to report a failure
	/// to find the requested object(s). 
	/// 
	/// </summary>
	public class FinderException:ApplicationException
	{

		public FinderException():base()
		{
		}
		

		public FinderException(string message):base(message)
		{
		}


		public FinderException(string msg, Exception inner) : base(msg, inner)
		{
		}
		

		public FinderException(SerializationInfo info , StreamingContext context)
			: base(info,context)
		{}


	}
}