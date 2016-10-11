using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace BoP.Core.Domain
{
    /// <summary>
    /// The Person class represents an individual and contains logical attributes about
    /// that individual that make him or her unique.  These attributes consist of 
    /// (but are not limited to) Gender, DOB, First Name, Last Name, taxid,
    /// etc.
    /// 
    /// Within the domain, there should only be one existence of a particular person,
    /// but a single person can have many roles.
    /// See the <see cref="Role"/> class for details.
    /// </summary>
    /// 
    [Serializable]
    [KnownType("GetKnownTypes")]
    public class Person: StakeHolder
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char? Gender { get; set; }
        public DateTime? DOB{ get; set; }
        public string TaxId { get; set; }
        public string Email { get; set; }

        static Type[] GetKnownTypes()
        {

            return new Type[] { typeof(User)};

        }

        public override string GetName()
        {
            return FirstName + " " + LastName; 
        }

    }
}
