using Hashtable = System.Collections.Hashtable;
using IEnumerator = System.Collections.IEnumerator;
using System;
using System.Web.Caching;
using System.Web;

namespace BoP.Util
{

    /// <summary>
    /// The ServiceTable is used to house the various services registered.
    /// Note that all new mutator methods are synchronized and thread safe within this class.
    /// </summary>
    internal class ServiceTable : Hashtable
    {

        public ServiceTable()
        { }

        /// <summary>
        /// Indicates if the serviceName given exists as
        /// a service in the service table.
        /// 
        /// </summary>
        public virtual bool IsService(string serviceName)
        {
            if (HttpRuntime.Cache[serviceName] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// Returns the service based upon the service name.
        /// 
        /// </summary>
        public virtual Service GetService(string serviceName)
        {

            return ((Service)HttpRuntime.Cache[serviceName]);
        }



        /// <summary>
        /// Add a service object by the service name.
        /// 
        /// </summary>
        public virtual bool AddService(Service service)
        {
            lock (this)
            {
                bool added = false;

                if (!(IsService(service.GetName())))
                {
                    HttpRuntime.Cache.Insert(service.GetName(), service);
                    added = true;
                }

                return (added);
            }
        }

        /// <summary>
        /// Removes a service object by the service name.
        /// 
        /// </summary>
        public virtual bool RemoveService(string name)
        {
            lock (this)
            {
                bool removed = false;

                if (IsService(name))
                {
                    removed = true;
                    HttpRuntime.Cache.Remove(name);
                }

                return (removed);
            }
        }




    }
}