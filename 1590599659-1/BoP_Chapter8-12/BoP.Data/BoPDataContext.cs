using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using BoP.Core;
using BoP.Core.DataInterfaces;
using BoP.Core.Domain;
using System.Configuration;

namespace BoP.Data.LTS
{
	
	public sealed class BoPDataContext : System.Data.Linq.DataContext
	{
        static XmlMappingSource map = XmlMappingSource.FromXml(System.IO.File.ReadAllText("BoP.map"));
        static string connectionString = ConfigurationManager.ConnectionStrings["BoP"].ToString();
        
         public BoPDataContext() :
             base(connectionString, map)
		{
			
		}

        public BoPDataContext(string connection)
            :
            base(connection, map) {
        }

        
        public System.Data.Linq.Table<Account> Accounts
        {
            get
            {
                return this.GetTable<Account>();
            }
        }

        public System.Data.Linq.Table<StakeHolder> StakeHolders{
            get {
                return this.GetTable<StakeHolder>();
            }
        }

        public System.Data.Linq.Table<StakeHolder> Persons
        {
            get
            {
                return this.GetTable<StakeHolder>();
            }
        }       
        
 }

        
}
         


            
  
