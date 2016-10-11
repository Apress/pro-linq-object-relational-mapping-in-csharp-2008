using System;
using ConfigurationSettings = System.Configuration.ConfigurationSettings;
using Hashtable = System.Collections.Hashtable;

namespace BoP.Util
{
	
	
	/// <summary>
	/// This is an example implementation of a service factory interface.  It's built to
	/// instantate class references based on the assembly name and the class name.
	/// A "service name" is provided and then the corresponding service name entry
	/// is searched for in the web.config.  The assembly and class name is retrieved
	/// based on that service name, and an object reference is instantiated. One thing to note is that although
    /// this is called a service factory it has nothing to do with a web service.  The work service is being
    /// used at the conceptual level.
	/// </summary>
	/// 
	/// <remarks>
	/// Entries for the class service factory are created by creating a "SectionGroup" within
	/// the web.config and for each object to register, a line item is placed within the
	/// BoP.Util.ClassServiceFactory section group.  The following is an example of how to register
	/// the SystemCodeManager interface found in the core package:
	/// 
	/// <para>
    /// section name="BoP.Core.SystemCodeManager" type="System.Configuration.DictionarySectionHandler, System, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    /// </para>
    /// 
    /// <para>
    /// With the above entry, a corresponding Dictionary entry would exist under "BoP.Core.SystemCodeManager" and have the following key entries:
    /// </para>
	///
	/// <para>
	/// add key="assemblyName" value="BoP.Core"
    /// </para>
    /// <para>
	/// add key="className" value="BoP.Core.DefaultSystemCodeManager"
	/// </para>
	/// <para>
	///	add key="lifeSpan" value="0"
    /// </para>
	///
	/// <para>
	/// Where the assemblyName is the name of the assembly holding the class to instantiate, the
	/// className is the name of the class to instantiate, and a lifespan (time to keep in the cache)
	/// for that object reference. 
	/// With each service entry there is a life span that can be defined.  A life span
	/// is defined in minutes and indicates how long the object should be kept cached
	/// and referenced.  So for example, if there is a service name called "foo" with a 
	/// lifespan of 60, on the first lookup of foo, the object would be instantiated 
	/// and cached for a lifespan of 60 minutes.  This means if this object is referenced
	/// again through the same instance of the ClassServiceFactory, they would receive the exact
	/// same reference again.
	/// </para>
	/// <para>
	/// There are three basic lifespan values:
	/// -1	(Lifespan immediate)  This means the object will be instantiated and
	///		dereferenced immediately.  In other words, a new instance will always
	///		be created
	///	0	(Lifespan indefinite)  This means the object, once instantiated, will
	///		be refrenced indefinitely by this instance of the ClassServiceFactory.
	///	</para>
	///		
	///	Also note that the table/dictionary that holds these referenced is marked
	///	NONSERIALIZABLE.  This is done because the references that this service
	///	factory holds cannot be guaranteed to be serializable themselves.  And since
	///	most/all object in .NET are marked serializable, this is marked non-serializable
	///	here.  This will only effect the client in situations where the service factory
	///	is attempted to be serialized.  In which case if it is, the class is recoverable.
	///	In other words, this class will reinstantiate any classes again if required once
	///	they are called upon.
	///	</remarks>
	public class ClassServiceFactory:IServiceFactory
	{

		private const string ASSEMBLY_NAME = "assemblyName";
		private const string CLASS_NAME = "className";
		private const string LIFE_SPAN = "lifeSpan";


		//Since most things on .NET are serialized, we specifically make
		//this non serialized.  We do this because we cannot guarantee the
		//services we hold on to will be completely serializable.  But
		//it is built to be recoverable.  If the table is lost, the
		//services will be properly reinstantiated if needed.
		[NonSerialized()]
		private ServiceTable serviceTable;

		private const string SECONDARY_SERVICE_PROVIDER = "SecondaryServiceFactory";

		public ClassServiceFactory():base()
		{
		}



		/// <summary> 
		/// Finds and returns a "service" based on the 
		/// service name provided.  The config file is searched
		/// for a service name given.  If found, then an attempt
		/// is made to instantiate the object based on the defined assembly name
		/// and the class name.
		/// If the service name cannot be found in the configuration, if a secondary
		/// service factory is defined the secondary is called in an attempt to
		/// find the object.
		/// </summary>
		/// 
		/// <exception cref="System.ArgumentException">
		/// If the service name is null or empty
		/// </exception>
		/// 
		/// <exception cref="BoP.Util.ObjectNotFoundException">
		/// If the service object cannot be found based on the service name
		/// </exception>
		/// 
		/// <exception cref="BoP.Util.FinderException">
		/// If an unexpected exception
		/// occurs
		/// </exception>
		/// 
		/// <param name="serviceName">
		/// The service name to find the service by
		/// </param>
		/// 
		/// <returns>
		/// the object under that service name
		/// </returns>
		public Object FindByServiceName(string serviceName)
		{
			lock(this)
			{
				Object retObj = null;
				Service service = GetService(serviceName);

				//If we didn't find the service, check to see if a secondary service provider
				//exists and attempt to call it by the service name
				if (service == null)
				{
					IServiceFactory secondaryServiceProvider = GetSecondaryServiceProvider();
					if (secondaryServiceProvider != null)
						retObj = secondaryServiceProvider.FindByServiceName(serviceName);
				}

				//Otherwise the service is not null and should be ready 
				//If it is not ready, then throw an exception
				else if (service.IsReady())
				{
					retObj = service.GetObject();
				}

				//Throw an exception.  this should not happen
				else
				{
					throw new FinderException("Service:  " + serviceName + " is not in a ready state but should be!");
				}

				//if the retObj is null, then we throw an ObjectNotFoundException
				if (retObj == null)
					throw new ObjectNotFoundException();

				return(retObj);
			}
		}



		/// <summary> 
		/// Returns the service table.  If for some reason the
		/// service table is empty/null, a new one is automatically 
		/// created.
		/// 
		/// </summary>
		/// 
		/// <returns>
		/// the service table for this factory
		/// 
		/// </returns>
		private ServiceTable GetServiceTable()
		{
			if (serviceTable == null)
				serviceTable = new ServiceTable();

			return(serviceTable);
		}


		/// <summary> 
		/// Attempts to return the service definition based
		/// on the service name.  If the service is not
		/// found in the service table, the configuration file
		/// is searched.  If it is not found through the
		/// configuration file, null will be returned.
		/// 
		/// </summary>
		/// 
		/// <param name="serviceName">
		/// The service name to find the service by
		/// </param>
		///  
		/// <returns>
		/// the service based on the service name
		/// </returns>
		private Service GetService(string serviceName)
		{
			Service theService = GetServiceTable().GetService(serviceName);

			if (theService == null)
				theService = LoadFromConfigFile(serviceName);

			return(theService);
		}



		/// <summary> 
		/// Attempts to create the service from the configuration
		/// file provided it exists based on the service name.
		/// The service will also be loaded into the service table
		/// for future reference
		/// If the service cannot be created, null will be returned.
		/// If the configuration provided is invalid for some reason,
		/// a FinderException will be thrown
		/// </summary>
		/// 
		/// <param name="serviceName">
		/// The service name to find the service by
		/// </param>
		///  
		/// <returns>
		/// the service based on the service name
		/// </returns>
		private Service LoadFromConfigFile(string serviceName)
		{
			Service newService = null;

			//If we find any '/' in the service name, replace them with "."
			string convertedServiceName = serviceName.Replace("/",".");

			//Now attempt to find the configuration entry for this service name

			string assemblyName = null;
			string className =  null;
			int lifeSpan = Service.LIFESPAN_INDEFINITE;

			Hashtable serviceEntries = (Hashtable)ConfigurationSettings.GetConfig("BoP.Util.ClassServiceFactory/" + convertedServiceName);

			//If this service definition exists..

			if (serviceEntries != null)
			{
				//Check for an assembly name.  If it exists then a class name must also exist,
				//otherwise it's an invalid state

				if (serviceEntries.ContainsKey(ASSEMBLY_NAME))
				{

					assemblyName = serviceEntries[ASSEMBLY_NAME].ToString();

					if (!serviceEntries.ContainsKey(CLASS_NAME))
						throw new FinderException("Class name not defined for service:  " + serviceName);

					className = serviceEntries[CLASS_NAME].ToString();
				}

				//Check for a life span definition
				if (serviceEntries.ContainsKey(LIFE_SPAN) && (!serviceName.Equals(SECONDARY_SERVICE_PROVIDER)))
				{
					string span = serviceEntries[LIFE_SPAN].ToString();
					try
					{
						lifeSpan = Int32.Parse(span);
						if (lifeSpan < 0)
							lifeSpan = Service.LIFESPAN_IMMEDIATE;
					}
					catch(Exception exc)
					{
						throw new FinderException("Life span value not recognized for service:  " + serviceName);
					}
				}

				//Now if an assembly name is defined, then create the service with the assembly name
				//and class name.  Otherwise just create a shell for the service that will 
				//be used by the remote service

				if (assemblyName != null)
				{
					newService = new Service(serviceName, assemblyName, className);
				}
				else
				{
					IServiceFactory secondaryServiceFactory = GetSecondaryServiceProvider();
					if (secondaryServiceFactory != null)
						newService = new Service(serviceName,secondaryServiceFactory);
					else
						throw new FinderException("Service:  " + serviceName + " is defined as remote but a secondary service provider was not found");
				}

				//Set the lifespan to what was defined or the default (-1)
				//and add it to the table

				newService.SetLifeSpan(lifeSpan);
				GetServiceTable().AddService(newService);
			}

			return(newService);
		}



		/// <summary> 
		/// Attempts to find the secondary service factory that would
		/// be registered with this implementation.  This is used
		/// as a way to possibly link in a ServiceFactory that
		/// could reference remote objects.
		/// </summary>
		///  
		/// <returns>
		/// a secondary service factory, if one exists
		/// </returns>
		private IServiceFactory GetSecondaryServiceProvider()
		{
			IServiceFactory factory2 = null;
			Service service = GetService(SECONDARY_SERVICE_PROVIDER);
			if (service != null)
				factory2 = (IServiceFactory)service.GetObject();

			return(factory2);
		}
	}
}