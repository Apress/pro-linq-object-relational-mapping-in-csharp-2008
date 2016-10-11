using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.EntityClient;
using BoP.Core.Domain;

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]
namespace BoP.Data
{    
    public class BoPObjectContext : global::System.Data.Objects.ObjectContext
    {

        public BoPObjectContext() :
            base("name=BoPObjectContext", "BoPObjectContext")
        {
        }

        public BoPObjectContext(string connectionString) :
            base(connectionString, "BoPObjectContext")
        {
        }

        public BoPObjectContext(global::System.Data.EntityClient.EntityConnection connection) :
            base(connection, "BoPObjectContext")
        {
        }
        
        public global::System.Data.Objects.ObjectQuery<Account> Account
        {
            get
            {
                if ((this._Account == null))
                {
                    this._Account = base.CreateQuery<Account>("[Account]");
                }
                return this._Account;
            }
        }
        private global::System.Data.Objects.ObjectQuery<Account> _Account;
        
        public global::System.Data.Objects.ObjectQuery<User> User
        {
            get
            {
                if ((this._User == null))
                {
                    this._User = base.CreateQuery<User>("[User]");
                }
                return this._User;
            }
        }
        private global::System.Data.Objects.ObjectQuery<User> _User;
        
        public global::System.Data.Objects.ObjectQuery<StakeHolder> StakeHolder
        {
            get
            {
                if ((this._StakeHolder == null))
                {
                    this._StakeHolder = base.CreateQuery<StakeHolder>("[StakeHolder]");
                }
                return this._StakeHolder;
            }
        }
        private global::System.Data.Objects.ObjectQuery<StakeHolder> _StakeHolder;
        
    }
       
}
