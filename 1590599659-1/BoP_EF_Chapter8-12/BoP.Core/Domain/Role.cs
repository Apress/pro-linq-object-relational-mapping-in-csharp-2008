using System;
using System.Collections.Generic;
using System.Text;

namespace BoP.Core.Domain
{

    /// <summary>
    /// For every stakeholder, there should be only 
    /// one instance of that particular stakeholder.  
    /// However,a stakeholder can take on many different 
    /// roles to represent himself differently based on the
    /// domain that stakeholder is working in.
    /// 
    /// </summary>
    
    [Serializable]
    public class Role:BaseEntity<string>
    {
        /// <summary>
        /// Default Constructor needed for refleciton
        /// </summary>
        public Role()
        { }

        /// <summary>
        /// Constructor.  Also sets the name of this role.  Every role has a "nice" name or "display name"
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
        public virtual StakeHolder StakeHolder{ 
            get; 
            set; 
        }

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
