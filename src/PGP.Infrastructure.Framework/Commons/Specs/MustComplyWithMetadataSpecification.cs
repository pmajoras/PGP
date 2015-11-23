using KissSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGP.Infrastructure.Framework.Commons.Specs
{

    /// <summary>
    /// Must comply with metadata specification.
    /// </summary>
	public class MustComplyWithMetadataSpecification<TTarget> : SpecificationBase<TTarget>
    {
        #region Constants

        /// <summary>
        /// The required not satisfied reason.
        /// </summary>
        public const string RequiredNotSatisfiedReason = "The {0} is required.";

        /// <summary>
        /// The minimum length not satisfied reason.
        /// </summary>
        public const string MinLengthNotSatisfiedReason = "The minimum length to {0} is {1}.";

        /// <summary>
        /// The max length not satisfied reason.
        /// </summary>
        public const string MaxLengthNotSatisfiedReason = "The maximum length to {0} is {1}.";

        /// <summary>
        /// The URL not satisfied reason.
        /// </summary>
        public const string UrlNotSatisfiedReason = "The {0} is an invalid URL.";

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the target object satisfies the specification.
        /// </summary>
        /// <param name="target">The target object to be validated.</param>
        /// <returns><c>true</c> if this instance is satisfied by the specified target; otherwise, <c>false</c>.</returns>
        public override bool IsSatisfiedBy(TTarget target)
        {

            return true;
        }

        #endregion
    }
}
