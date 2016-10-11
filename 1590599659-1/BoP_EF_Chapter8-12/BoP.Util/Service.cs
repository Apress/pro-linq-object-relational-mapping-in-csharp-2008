using System;
using Activator = System.Activator;
using ObjectHandle = System.Runtime.Remoting.ObjectHandle;
using FileNotFoundException = System.IO.FileNotFoundException;
using MissingMethodException = System.MissingMethodException;
using TypeLoadException = System.TypeLoadException;

namespace BoP.Util
{
	
	
	/// <summary> The Service class tracks service information about
	/// a particular service.  It holds the lifespan of the service, as well as
	/// the class name and assembly which is used to instantiate the service class (if local).
	/// 
	/// Note that is implementation is thread safe. 
	/// 
	/// 
	/// </summary>
	internal class Service
	{
		public const int LIFESPAN_INDEFINITE = 0;
		public const int LIFESPAN_IMMEDIATE = -1;

		protected string className;
		protected string assemblyName;
		protected int lifeSpan;
		protected Object obj;
		protected DateTime timestamp;
		protected string name;
		protected IServiceFactory factory;
		

		/// <summary>
		/// Creates a service reference based on the service name, the assembly name and class name.
		/// In this instance, this service will be marked as LOCAL since an assembly and class name
		/// is provided.  An attempt to get a service reference will force an instantiation of an
		/// object by this class
		/// If the service name is null or empty string, an ArgumentException will be thrown.
		/// If the assembly name is null or empty string, an ArgumentException will be thrown.
		/// If the class name is null or empty string, an ArgumentException will be thrown. 
		/// </summary>
		public Service(string serviceName, string assemblyName, string className):base()
		{
			if ((serviceName == null) || (serviceName.Length <= 0))
				throw new ArgumentException("Service name cannot be empty");

			if ((assemblyName == null) || (assemblyName.Length <= 0))
				throw new ArgumentException("Assembly name cannot be empty");

			if ((className == null) || (className.Length <= 0))
				throw new ArgumentException("Class name cannot be empty");

			name = serviceName;
			this.assemblyName = assemblyName;
			this.className = className;

			lifeSpan = 0;
			obj = null;
			factory = null;
			timestamp = System.DateTime.Now;
		}




		/// <summary>
		/// Creates a service reference based on the service name, and the secondary service
		/// factory provided.  In this case, whenever this service is referenced the secondary
		/// service factory will be queried to find the service reference.
		/// If the service name is null or empty string, an ArgumentException will be thrown.
		/// the service factory can be null and then set at a later time.  However, this
		/// service will not be in a "ready state" until the factory is set.
		/// </summary>

		public Service(string serviceName, IServiceFactory factory):base()
		{
			if ((serviceName == null) || (serviceName.Length <= 0))
				throw new ArgumentException("Service name cannot be empty");

			name = serviceName;
			this.factory = factory;

			lifeSpan = 0;
			obj = null;
			timestamp = System.DateTime.Now;
			assemblyName = null;
			className = null;
		}
		


		/// <summary>
		/// Is this service local.  A local service has an assembly name defined with the service
		/// information.
		/// </summary>
		/// 
		/// <returns>
		/// is the service a local service (as opposed to remote)
		/// </returns>
		public virtual bool IsLocal()
		{
			return (assemblyName != null);
		}
		
	

		/// <summary>
		/// Indicates if this service is ready to be used.  A service that is defined as local
		/// will always return ready.  If the service is remote and a secondary factory
		/// has not been set, this will return false.
		/// </summary>
		/// 
		/// <returns>
		/// is the service ready to be used
		/// </returns>
		public virtual bool IsReady()
		{
			if (IsLocal())
				return(true);
			else if (factory == null)
				return(false);
			else
				return(true);
		}


		/// <summary>
		/// Returns the object reference for this service.  Depending
		/// on if this service is local, an object may be instantiated or
		/// if remote, a remote reference will be returned.
		/// If a reference to the service cannot be found/instantiated,
		/// a ObjectNotFoundException is thrown.
		/// If an unexpected exception occurs such as the service not being ready,
		/// a FinderException is thrown.
		/// 
		/// </summary>
		/// 
		/// <returns>
		/// the object reference (ie the service)
		/// </returns>
		/// 
		public virtual Object GetObject()
		{
			if (!IsReady())
				throw new FinderException("Service is not in a ready state");

			lock (this)
			{
				if (lifeSpan == - 1)
					obj = null;
				else if ((lifeSpan > 0) && ((DateTime.Now.Ticks >= (timestamp.Ticks + (lifeSpan * 60 * 1000)))))
					obj = null;
				
				if (obj == null)
				{
					if (IsLocal())
						obj = CreateByInstantiation();
					else
						obj = CreateByServiceFactory();
				}

				return obj;
			}
		}


		
		/// <summary>
		/// Returns the service name for this service information.
		/// </summary>
		/// 
		/// <returns>
		/// the service class name
		/// </returns>
		public virtual string GetName()
		{
			return (name);
		}
		
		
		
		
		
		/// <summary>
		/// Returns the class name for this service information class.  Null is
		/// allowed here.
		/// 
		/// </summary>
		/// <returns>
		/// the service class name
		/// </returns>
		public virtual string GetClassName()
		{
			return (className);
		}
		
		
	
	
		/// <summary>
		/// Returns the assembly name for this service.  Null is
		/// allowed here.
		/// 
		/// </summary>
		/// <returns>
		/// the service class name
		/// </returns>
		public virtual string GetAssemblyName()
		{
			return (assemblyName);
		}
		
		
		
		/// <summary>
		/// Returns the life span for the object contained in this service information.
		/// A life span of -1 indicates this service should always be reinstantiated/looked up.
		/// A life span of 0 indicates the object should ALWAYS be stored.
		/// By default the value for the lifespan is 0.
		/// 
		/// </summary>
		/// <returns> 
		/// the time in minutes
		/// </returns>
		public virtual int GetLifeSpan()
		{
			return (lifeSpan);
		}
		
		
		
		
		/// <summary>
		///  Sets the life span in minumtes for this service.
		/// 
		/// A life span of -1 indicates this service should always be reinstantiated/looked up.
		/// A life span of 0 indicates the object should ALWAYS be stored.
		/// By default the value for the lifespan is 0.
		/// </summary>
		/// 
		/// <param name="span">
		/// the time in minutes
		/// </param>
		public virtual void SetLifeSpan(int span)
		{
			lock (this)
			{
				lifeSpan = span;
				if (span > 0)
					this.timestamp = System.DateTime.Now;
			}
		}




		/// <summary>
		/// Returns the service factory associated with this service,  if one
		/// exists.
		/// </summary>
		/// 
		/// <returns>
		/// the service factory
		/// </returns>
		public virtual IServiceFactory GetServiceFactory()
		{
			return (factory);
		}



		/// <summary>
		/// Sets the service factory to be associated with this
		/// service.  If the service is a local service, attempting
		/// to set this value will throw an ApplicationException
		/// </summary>
		/// 
		/// <param name="factory">
		/// the service factory to be used by this service
		/// </param>
		public virtual void SetServiceFactory(IServiceFactory factory)
		{
			if (IsLocal())
				throw new ApplicationException("Service factory cannot be set on a local service");

			lock(this)
			{
				this.factory = factory;
			}
		}



		/// <summary>
		/// Attempts to create the "service" by instantiation.  The assembly is found and
		/// then the class is instantiated by the class name.  If either of these
		/// fail a "ObjectNotFoundException is thrown.  This actually uses the System.Activator 
		/// package which uses methods to create an instance of an object.  A handle is 
		/// returned for the object and then the handle is "unwrapped" to find
		/// and return the real object.
		/// </summary>
		/// 
		/// <returns>
		/// the instantiated object
		/// </returns>
		protected virtual Object CreateByInstantiation()
		{
			Object retObj = null;
			ObjectHandle handle = null;

			//Take the assembly name and the class name for this service and attempt 
			//to create an instance

			try
			{
				handle = Activator.CreateInstance(assemblyName, className);
			}
			catch(FileNotFoundException exc)
			{
				throw new ObjectNotFoundException("Assembly could not be found:  " + assemblyName);
			}
			catch(MissingMethodException exc2)
			{
				throw new ObjectNotFoundException("Default Constructor could not be found:  " + className);
			}
			catch(TypeLoadException exc3)
			{
				throw new ObjectNotFoundException("Class name could not be found:  " + className);
			}
			catch(Exception exc4)
			{
				throw new ObjectNotFoundException(exc4.Message);
			}

			//If we don't have a handle, it means we couldn't find it
			if (handle == null)
				throw new ObjectNotFoundException("Assembly/Classname could not be found:  " + assemblyName + "; " + className);


			//Now attempt to unwrap the handle for the object
			try
			{
				retObj = handle.Unwrap();
			}
			catch(Exception exc)
			{
				throw new ObjectNotFoundException("Unexpected Exception accessing reference object through handle: " + exc.Message);
			}

			//If we don't have an object, it means the class could not be found

			if (retObj == null)
				throw new ObjectNotFoundException("Class could not be found when unwrapping handle reference: " + className);

			return(retObj);
		}




		/// <summary>
		/// Attempts to create the "service" by the secondary service factory.
		/// If the object cannot be found, a ObjectNotFoundException is thrown.
		/// </summary>
		/// 
		/// <returns>
		/// the object found (if any)
		/// </returns>
		protected virtual Object CreateByServiceFactory()
		{
			Object retObj = null;
			try
			{
				retObj = factory.FindByServiceName(name);
				return(retObj);
			}
			catch(Exception exc)
			{
				throw new ObjectNotFoundException(exc.Message);
			}
		}
			
	}
}