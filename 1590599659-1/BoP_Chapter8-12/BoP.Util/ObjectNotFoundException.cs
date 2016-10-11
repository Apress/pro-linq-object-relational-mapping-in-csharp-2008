using System;
using System.Runtime.Serialization;

namespace BoP.Util
{

    /// <summary>
    /// The ObjectNotFoundException exception is thrown by a finder method in the factory classes
    /// to indicate that the specified object does not exist. 

    /// 
    /// </summary>
    public class ObjectNotFoundException : ApplicationException
    {


        public ObjectNotFoundException()
            : base()
        {
        }



        public ObjectNotFoundException(string message)
            : base(message)
        {
        }


        public ObjectNotFoundException(string msg, Exception inner)
            : base(msg, inner)
        {
        }


        public ObjectNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

    }
}