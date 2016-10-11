using System;
using System.Collections.Generic;
using System.Text;

namespace BoP.Core.Domain
{
    /// <summary>
    /// The User class, as the name implies is the "User"
    /// role in the system. This role is placed on any stakeholder 
    /// in the FBP to provide the required information
    /// for logging on and off of the system.  This Role is 
    /// created after a user of the web site registers.
    /// </summary>
    /// 
    [Serializable]
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName = "BoP.Core.Domain", Name = "User")]
    public class User:Role
    {
        private const string ROLE_NAME = "User";
        private string _userId;
        private string _password;
        private string _active;
        


        public User() { } 

        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        public string UserId {
            get { return base.ID; }
            set { base.ID = value; }
        }

        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        public string Password {
            get { return _password ;} 
            set
            {

                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);
                _password = value;
                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);


            }
        }

        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        public string Active
        {
            get { return _active; }             
            set
            {

                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);
                _active = value;
                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);


            }
        }


        public global::System.Data.Objects.DataClasses.EntityReference<Person> PersonReference
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Person>("BoP.Core.Domain.PersonUser", "Person");
            }
            set
            {
                if ((value != null))
                {
                    ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.InitializeRelatedReference<Person>("BoP.Core.Domain.PersonUser", "Person", value);
                }
            }
        }
        
        
    }
}
