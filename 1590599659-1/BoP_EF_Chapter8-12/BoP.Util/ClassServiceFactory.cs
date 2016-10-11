using System;
using ConfigurationSettings = System.Configuration.ConfigurationSettings;
using Hashtable = System.Collections.Hashtable;

namespace BoP.Util
{
	
	
/// <summary>
/// This code was adapted from the ClassServiceFactory found in 
/// org.eclipse.jdt.apt.core.  For more information please see:
/// http://www.eclipse.org/"
/// An implementation of the service factory interface. It's built to
/// instantiate class references based on the assembly name and the class name.
/// A "service name" is provided and then the corresponding service name entry
/// is searched for in web.config. The assembly and class name are retrieved
/// based on that service name, and an object reference is instantiated.
/// </summary>
public class ClassServiceFactory:IServiceFactory
{

       private const String ASSEMBLY_NAME = "assemblyName";
       private const String CLASS_NAME = "className";
       private const String LIFE_SPAN = "lifeSpan";

       [NonSerialized()]
       private ServiceTable serviceTable;

       private const String SECONDARY_SERVICE_PROVIDER = "SecondaryServiceFactory";

       public ClassServiceFactory():base(){}

       /// <summary>
       /// Finds and returns a "service" based on the
       /// service name provided.  The config file is searched
       /// for a service name given.  If found, then an attempt
       /// is made to instantiate the object based on the defined assembly name
       /// and the class name.
       /// If the service name cannot be found in the configuration and a secondary
       /// service factory is defined, the secondary is called in an attempt to
       /// find the object.
       /// </summary>
       public Object FindByServiceName(String serviceName)
       {
              lock(this)
              {
                      Object retObj = null;
                      Service service = GetService(serviceName);

                    // check to see if a secondary service provider
                    // exists and attempt to call it by the service name
                     if (service == null)
                     {
                           IServiceFactory secondaryServiceProvider =
                                GetSecondaryServiceProvider();
                           if (secondaryServiceProvider != null)
                             retObj = secondaryServiceProvider.
                                  FindByServiceName(serviceName);
                     }

                     // Otherwise, the service is not null and should be ready
                     // If it is not ready, then throw an exception
                     else if (service.IsReady())
                     {
                          retObj = service.GetObject();
                     }

                     // Throw an exception.  This should not happen
                     else
                     {
                          throw new FinderException("Service:  " + serviceName +
                             " is not in a ready state but should be");
                     }

                     // if the retObj is null, then we throw an
                     // ObjectNotFoundException
                     if (retObj == null)
                         throw new ObjectNotFoundException();

                     return(retObj);

                     }
              }

              /// <summary>
              /// Returns the service table. If for some reason the
              /// service table is empty/null, a new one is automatically
              /// created.
              /// </summary> 
              private ServiceTable GetServiceTable()
              {
                     if (serviceTable == null)
                           serviceTable = new ServiceTable();

                     return(serviceTable);
                }

              /// <summary>
              /// Attempts to return the service definition based
              /// on the service name. If the service is not
              /// found in the service table, the configuration file
              /// is searched. If it is not found through the
              /// configuration file, null will be returned.
              /// </summary>
              private Service GetService(String serviceName)
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
              /// for future reference.
              /// If the service cannot be created, null will be returned.
              /// If the configuration provided is invalid for some reason,
              /// a FinderException will be thrown.
              /// </summary>
              private Service LoadFromConfigFile(String serviceName)
              {
                     Service newService = null;

                     //If we find any '/' in the service name, replace them with "."
                     String convertedServiceName = serviceName;
                     int index = convertedServiceName.IndexOf("/");
                     while (index >= 0)
                     {
                             convertedServiceName =
                             convertedServiceName.Substring(0,index) + "." + 
                             convertedServiceName.Substring(index+1,
                             (convertedServiceName.Length - (index+1)));
                             index = convertedServiceName.IndexOf("/");
                     }

                      //Now attempt to find the configuration
                      //entry for this service name

                      String assemblyName = null;
                      String className =  null;
                      int lifeSpan = Service.LIFESPAN_INDEFINITE;

                      Hashtable serviceEntries =
                       (Hashtable)ConfigurationSettings.GetConfig
                       ("BoP.Util.ClassServiceFactory/" +
                                  convertedServiceName);

                      //If this service definition exists.

                      if (serviceEntries != null)
                      {
                             //Check for an assembly name.  If it
                             // exists, then a class name must also exist,
                             // otherwise it's an invalid state

                             if (serviceEntries.ContainsKey(ASSEMBLY_NAME))
                             {
                                    assemblyName =
                                    (String)serviceEntries[ASSEMBLY_NAME];

                                    if (!serviceEntries.ContainsKey(CLASS_NAME))
                                           throw new FinderException("Class name not defined for service: " + serviceName);

                                    className = (String)serviceEntries[CLASS_NAME];
                             }

                             //Check for a life span definition

                             if (serviceEntries.ContainsKey(LIFE_SPAN) &&
                                (!serviceName.Equals(SECONDARY_SERVICE_PROVIDER)))
                             {
                                    String span = (String)serviceEntries[LIFE_SPAN];
                                    try
                                    {
                                           lifeSpan = Int32.Parse(span);
                                           if (lifeSpan < 0)
                                              lifeSpan = Service.LIFESPAN_IMMEDIATE;
                                    }
                                    catch(Exception exc)
                                    {
                                            throw new FinderException("Life span value not recognized for service:  " +
                                                serviceName);
                                    }
                             }

                             //Now if an assembly name is defined, then create the
                             //service with the assembly name and class name.
                             //Otherwise, just create a shell for the service that
                             //will be used by the remote service.

                             if (assemblyName != null)
                             {
                                     newService = new Service(serviceName,
                                          assemblyName, className);
                             }
                             else
                             {
                                    IServiceFactory secondaryServiceFactory =
                                          GetSecondaryServiceProvider();
                                    if (secondaryServiceFactory != null)
                                          newService = new
                                          Service(serviceName,
                                           secondaryServiceFactory);
                                    else
                                          throw new FinderException("Service:  " +
                                              serviceName + " is defined as remote but a secondary service provider was not found");
                             }

                             //Set the life span to what was defined)
                             //and add it to the table

                             newService.SetLifeSpan(lifeSpan);
                             GetServiceTable().Add(serviceName,newService);
                     }

                     return(newService);
              }

              /// <summary>
              /// Attempts to find the secondary service factory that would
              /// be registered with this implementation.  This is used
              /// as a way to possibly link in a ServiceFactory that
              /// could reference remote objects.
              /// </summary>
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