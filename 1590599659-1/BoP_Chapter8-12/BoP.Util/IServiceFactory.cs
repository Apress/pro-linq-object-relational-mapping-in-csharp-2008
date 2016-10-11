using System;

namespace BoP.Util
{


    /// <summary>
    /// A simple interface that can find a "service" based on a service name.  
    /// </summary>
    public interface IServiceFactory
    {


        /// <summary> 
        /// Finds and returns a "service" based on the 
        /// service name provided.  
        /// </summary>
        Object FindByServiceName(string serviceName);



    }
}