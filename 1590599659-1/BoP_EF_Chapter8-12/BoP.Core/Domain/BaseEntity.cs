using System.Text;
using System.Data.Objects.DataClasses;
using System.Data.Metadata.Edm;
using System;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]
[assembly: global::System.Data.Objects.DataClasses.EdmRelationshipAttribute("BoP.Core.Domain", "StakeHolderAccount", "StakeHolder", global::System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(BoP.Core.Domain.StakeHolder), "Account", global::System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(BoP.Core.Domain.Account))]

namespace BoP.Core.Domain
{
    /// <summary>
    /// This class was adapted from Billy Mccafferty's NHibernate Framework
    /// <seealso cref="http://devlicio.us/blogs/billy_mccafferty"/>
    /// 
    /// This is the base class for all entity objects in the model.  
    /// 
	/// This class has also been marked abstract so it cannot 
    /// be instantiated independently, but must be subclassed.
    /// </summary>
    /// 
    [DataContract]
    public abstract class BaseEntity<T> : IEntityWithRelationships, IEntityWithChangeTracker, IEntityWithKey
    {
        public BaseEntity()
        { }
        private T id = default(T);

        /// <summary>
        /// ID may be of type string, int, custom type, etc.
        /// Setter is protected to allow unit tests to set this 
        /// property via reflection and to allow domain objects 
        /// more flexibility in setting this for those objects with 
        /// assigned IDs.
        /// </summary>
        /// 
        
        [DataMember]
        public virtual T ID {
            get { return id; }
            set {
   
                id = value; }
        }

        
        public override sealed bool Equals(object obj) {
            BaseEntity<T> compareTo = obj as BaseEntity<T>;

            return (compareTo != null) &&
                   (HasSameNonDefaultIdAs(compareTo) ||
                    // Since the IDs aren't the same, either of them must be transient to 
                    // compare business value signatures
                    (((IsTransient()) || compareTo.IsTransient()) &&
                     HasSameBusinessSignatureAs(compareTo)));
        }

        /// <summary>
        /// Transient objects are not associated with an item already in storage.  
        /// For instance, a <see cref="Applicant" /> is transient if its ID is 0.
        /// </summary>
        public bool IsTransient() {
            return ID == null || ID.Equals(default(T));
        }

        /// <summary>
        /// Must be provided to properly compare two objects
        /// </summary>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        private bool HasSameBusinessSignatureAs(BaseEntity<T> compareTo) {

            return GetHashCode().Equals(compareTo.GetHashCode());
        }

        /// <summary>
        /// Returns true if self and the provided persistent 
        /// object have the same ID values and the IDs are 
        /// not of the default ID value
        /// </summary>
        private bool HasSameNonDefaultIdAs(BaseEntity<T> compareTo) {

            return (ID != null && ! ID.Equals(default(T))) &&
                   (compareTo.ID != null && ! compareTo.ID.Equals(default(T))) &&
                   ID.Equals(compareTo.ID);
        }

        /// <summary>
        /// Overriden to return the class type
        /// of this object.
        /// </summary>
        /// 
        /// <returns>
        /// the class name for this object
        /// </returns>
        /// 
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" Class: ").Append(GetType().FullName);
            return str.ToString();
        }


        #region IEntityWithRelationships Members
        RelationshipManager _relationships = null;

        RelationshipManager IEntityWithRelationships.RelationshipManager
        {
            get
            {
                if (null == _relationships)
                    _relationships = RelationshipManager.Create(this);
                return _relationships;
            }

        }

        #endregion
        
        #region IEntityWithChangeTracker Members

        
        IEntityChangeTracker _changeTracker = null;

        void IEntityWithChangeTracker.SetChangeTracker(IEntityChangeTracker changeTracker)
        {
            _changeTracker = changeTracker;

            // Every time the change tracker is set, we must also set all the 
            // complex type change trackers.
            //if (_extendedInfo != null)
            //{
            //    _extendedInfo.SetComplexChangeTracker("ExtendedInfo", _changeTracker);
            //}

        }
        
        #endregion

        #region IEntityWithKey Members

        public void PropertyChanging(string propName)
        {   
            if(propName.StartsWith("set_"))
                propName = propName.Replace("set_", string.Empty);
            if (_changeTracker != null)
            {
                _changeTracker.EntityMemberChanging(propName);

            }

        
        }

        public void PropertyChanged(string propName)
        {
            if (_changeTracker != null)
            {
                _changeTracker.EntityMemberChanged(propName);
            }


        }


        EntityKey _entityKey = null;
        [DataMember]
        System.Data.EntityKey IEntityWithKey.EntityKey
        {
            get
            {
                return _entityKey; 

            }
            set
            {
                // Set the EntityKey property, if it is not set.
                // Report the change if the change tracker exists.
                if (_changeTracker != null)
                {
                    _changeTracker.EntityMemberChanging("-EntityKey-");
                    _entityKey = value;
                    _changeTracker.EntityMemberChanged("-EntityKey-");
                }
                else
                {
                    _entityKey = value;
                }

            }
        }

        #endregion
    }
}
