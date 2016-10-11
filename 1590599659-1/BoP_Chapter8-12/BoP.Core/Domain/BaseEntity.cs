using System.Text;
using System;

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
    [Serializable]
    public abstract class BaseEntity<T>
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
        public virtual T ID {
            get { return id; }
            set { id = value; }
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

    }
}
