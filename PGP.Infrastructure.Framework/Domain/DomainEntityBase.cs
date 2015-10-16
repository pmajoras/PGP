using System;
using HelperSharp;
using Skahal.Infrastructure.Framework.Domain;

namespace PGP.Infrastructure.Framework.Domain
{
    /// <summary>
    /// Base class for the Domain Entity
    /// </summary>
    public abstract class DomainEntityBase : EntityWithIdBase<int>, IEntity, ICloneable
    {
        #region Constructors

        /// <summary>
        /// Initializes and instance of <see cref="DomainEntityBase"/>
        /// </summary>
        protected DomainEntityBase()
        {
            CreatedDate = DateTime.Now;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Property to check if the entity is new.
        /// </summary>        
        public bool IsNew
        {
            get
            {
                return Id == 0;
            }
        }

        #endregion

        /// <summary>
        /// The entity creation Date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        #region Methods
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>        
        public virtual object Clone()
        {
            var cloned = ObjectHelper.CreateShallowCopy(this) as DomainEntityBase;
            cloned.Id = 0;

            return cloned;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var result = base.Equals(obj);

            return result
                && (obj.GetType().IsAssignableFrom(GetType()) || GetType().IsAssignableFrom(obj.GetType())); // Needed because of EF proxies.
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return "{0}_{1}".With(GetType(), Id).GetHashCode();
        }

        #endregion
    }
}
