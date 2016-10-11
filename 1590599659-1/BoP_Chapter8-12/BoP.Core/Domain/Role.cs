using System;
using System.Collections.Generic;
using System.Text;

namespace BoP.Core.Domain
{

    /// <summary>
    /// For every stakeholder, there should be only 
    /// one instance of that particular stakeholder in the domain.  
    /// A stakeholder can take on many different 
    /// roles to represent himself based on the
    /// domain that stakeholder is involved in.
    /// 
    /// For example, Bill(who is a person and hence a stakeholder) 
    /// could be seen as a Police Man for his profession, but is also
    /// seen as a Coach for his high school football team, 
    /// and is seen as a borrower if he has an auto loan.  
    /// All of the above would be represented as Roles on Bill.
    /// Every role defined for a stakeholder should extend off of 
    /// this class. 
    /// 
    /// </summary>
    /// 
    [Serializable]
    public class Role
    {
        /// <summary>
        /// Default Constructor needed for refleciton
        /// </summary>
        public Role()
        { }

        /// <summary>
        /// Constructor to set the name of this role.  Every role has a "nice" name or "display name"
        /// indicating the type of role it is.  Every class which extends off of role is responsible for
        /// providing a nice name for it's role.
        /// </summary>
        /// 
        /// <param name="name">
        /// display name for this role.
        /// </param>
        /// 
        public Role(string name)
        {
            if (Name != null)
                this.Name = name;
        }

        public int RoleType { get; set; }


        /// <summary>
        /// Returns the "nice" name or "display" name for this role.  Every class which subclasses this
        /// role class is responsible for supplying a display name.
        /// </summary>
        /// 
        /// <returns>
        /// the name for this role.
        /// </returns>
        public string Name { get; set; }


        public int StakeHolderId{ get; set; }

        /// <summary>
        /// Gets/Sets the stakeholder for this role.
        /// EVERY role should be associated with a stakeholder
        /// </summary>
        /// 
        /// <returns>
        /// the stakeholder for this implementing this role.
        /// </returns>
        public virtual StakeHolder StakeHolder{ get; set; }

        /// <summary>
        /// Convenience method.  Get the stakeholder's name, which may be a person's
        /// name or company's name.
        /// </summary>
        /// 
        /// <returns>
        /// the name
        /// </returns>
        public virtual String GetName()
        {
            if (StakeHolder == null)
                return "Undefined";
            else
                return StakeHolder.GetName();
        }


        /// <summary>
        /// Convenience method.  Get the stakeholder as a person.
        /// Returns null if the stakeholder can't be narrowed to a Person.
        /// </summary>
        /// 
        /// <returns>
        /// the stakeholder as a person
        /// </returns>
        public virtual Person Person
        {
            get
            {
                if (StakeHolder is Person)
                    return ((Person)StakeHolder);
                else
                    return (null);
            }
            
            set
            {
                StakeHolder = value;
            }
        }



        /// <summary>
        /// Returns a string representation of the object.
        /// 
        /// </summary>
        /// <returns>
        /// a string representation of the object.
        /// </returns>
        public override String ToString()
        {
            StringBuilder buf = new StringBuilder();
            lock (this)
            {
                buf.Append(base.ToString());
                buf.Append("Role Model");
            }
            buf.Append("\r\n");
            return buf.ToString();
        }

    }
}
