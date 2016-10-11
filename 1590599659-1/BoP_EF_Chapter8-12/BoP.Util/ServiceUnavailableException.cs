using System;
using System.Runtime.Serialization;
namespace BoP.Util
{
	
	/// <summary>
	/// This exception is used whenever access to a "service" is interrupted, or an
	/// unexpected exception occurs using a service.  Typically if the FinderException
	/// or ObjectNotFoundException will be wrapped by this exception whenever looking
	/// for a service is part of the business logic or part of a business process.
	/// </summary>
	/// 
	public class ServiceUnavailableException:ApplicationException
	{
		
		public ServiceUnavailableException():base()
		{
		}
		
		
		public ServiceUnavailableException(String msg):base(msg)
		{
		}

		public ServiceUnavailableException(string msg, Exception inner) : base(msg, inner)
		{
		}

		public ServiceUnavailableException(SerializationInfo info , StreamingContext context)
			: base(info,context)
		{
		}

	}
}