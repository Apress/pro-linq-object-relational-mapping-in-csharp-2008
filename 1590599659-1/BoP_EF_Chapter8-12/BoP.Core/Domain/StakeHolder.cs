using System;
using System.Collections.Generic;
using System.Text;

namespace BoP.Core.Domain
{

    /// <summary>
    /// The base stakeholder class.  A stakeholder is a person, 
    /// company, or organization that has an interest 
    /// in the domain.
    /// </summary>
    /// 
    [Serializable]
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName = "BoP.Core.Domain", Name = "StakeHolder")]
    public abstract class StakeHolder:BaseEntity<int>
    {
        List<Role> roles;
 
        /// <summary>
        /// Default constructor initializes the Role collection
        /// </summary>
		public StakeHolder()
		{
            roles = new List<Role>();
		}


        public int StakeHolderType { get; set; }

        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        public int StakeHolderId
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }

        public List<Account> Accounts
        {get;set;}

        [global::System.Data.Objects.DataClasses.EdmRelationshipNavigationPropertyAttribute("BoP.Core.Domain", "StakeHolderAccount", "Account")]
        public global::System.Data.Objects.DataClasses.EntityCollection<Account> Account
        {
            get
            {
                return ((global::System.Data.Objects.DataClasses.IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Account>("BoP.Core.Domain.StakeHolderAccount", "Account");
            }
        }

        public List<Role> Roles
        {
            get{
                return roles;
            }
            set
            {
                foreach (Role r in value)
                    roles.Add(r);
            }
        }

		/// <summary>
		/// Check if there are any roles associated with this
		/// stakeholder.
		/// </summary>
		/// 
		/// <returns>
		/// is the roles list empty.
		/// </returns>
		public virtual bool IsRolesEmpty()
		{
			return (Roles.Count == 0);
		}


		public virtual String GetName()
        {
            return "";
        }
				
		/// <summary>
		/// Adds a role to this stakeholder.  Also makes sure 
        /// to assign the stakeholder to the role as well.
		/// 
		/// </summary>
		/// <param name="r">
		/// the role to add
		/// </param>
		public virtual void AddRole(Role r)
		{
            r.StakeHolder = this;
            r.StakeHolderId = this.StakeHolderId;
			Roles.Add(r);
		}
		
		/// <summary>
		/// Removes the Role.
		/// The stakeholder reference is also set to null.
		/// 
		/// </summary>
		/// <param name="r">
		/// the role to remove
		/// </param>
		/// <returns>
		/// was the role removed
		/// </returns>
		public virtual bool RemoveRole(Role r)
		{
			Roles.Remove(r);
			
			bool success = (Roles.IndexOf(r) < 0);
			if (success)
				r.StakeHolder = null;
			
			return success;
		}
		
		
		/// <summary>
		/// Get the number of roles on this stakeholder
		/// </summary>
		/// 
		/// <returns>
		/// the number of roles for the stakeholder
		/// </returns>
		/// 
		public virtual int NumberOfRoles()
		{
			return Roles.Count;
		}
		
		
		/// <summary>
		/// Check if the role is contained in this stakeholder.
		/// 
		/// </summary>
		/// <param name="a">
		/// the role to check for
		/// </param>
		/// <returns>
		/// was the role found
		/// </returns>
		public virtual bool ContainsRole(Role a)
		{
			return Roles.Contains(a);
		}
		
		
		/// <summary>
		/// Every role has a name, see if a role exists on this stakeholder by the designated name.
		/// </summary>
		/// 
		/// <param name="roleName">
		/// the role name to search for
		/// </param>
		/// 
		/// <returns>
		/// does the role exist by the given role name
		/// </returns>
		public virtual bool ContainsRoleOfName(String roleName)
		{
			Role role = GetRoleOfName(roleName);
			return (role != null);
		}
		
		
		/// <summary>
		/// Every role has a name, return the role based on the role name
		/// passed in.  If the role does not exist by the given name,
		/// null is returned.
		/// </summary>
		/// 
		/// <param name="roleName">
		/// the role name to search for
		/// </param>
		/// 
		/// <returns>
		/// the role by the given name
		/// </returns>
		public virtual Role GetRoleOfName(string roleName)
		{
			if ((roleName != null) && (roleName.Length > 0))
			{
				
				for (int i = 0; i < Roles.Count; i++)
				{
					if (roleName.Equals(Roles[i].Name))
						return Roles[i];
				}
			}
			
			return null;
		}

    }
}
