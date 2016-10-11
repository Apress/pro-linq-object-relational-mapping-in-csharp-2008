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
    /// (but are not limited to) gender, birth date, First Name, Last Name, taxid,
    /// etc.
    /// 
    /// Within the domain, there should only be one existence of a particular person,
    /// but a single person can have many roles.
    /// See the <see cref="Role"/> class for details.
    /// </summary>
    /// 
    [Serializable]
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName = "BoP.Core.Domain", Name = "Person")]
    public class Person: StakeHolder
    {
        private string _firstName;
        private string _lastName;
        private string _gender;
        private DateTime? _dob;
        private string _taxId;
        private string _email;

        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {

                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);
                _firstName = value;
                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);


            }
        }

        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {

                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);
                _lastName = value;
                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);


            }
        }


        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        public string TaxId
        {
            get
            {
                return _taxId;
            }
            set
            {

                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);
                _taxId = value;
                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);


            }
        }

        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);
                _email = value;
                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);


            }
        }

        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        public DateTime? DOB
        {
            get
            {
                return _dob;
            }
            set
            {

                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);
                _dob = value;
                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);


            }
        }
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {

                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);
                _gender = value;
                this.PropertyChanging(System.Reflection.MethodInfo.GetCurrentMethod().Name);


            }
        }


        public override string GetName()
        {
            return FirstName + " " + LastName; 
        }
    }
}
