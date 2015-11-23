using KissSpecifications;
using PGP.Infrastructure.Framework.Specifications;
using PGP.Infrastructure.Framework.Specifications.Errors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGP.Infrastructure.Framework.Commons.DomainSpecifications
{

    /// <summary>
    /// Must comply with metadata specification.
    /// </summary>
	public class MustComplyWithMetadataSpecificationBase<TTarget> : DomainSpecification<TTarget>
    {
        #region NotSatisfiedReasons

        /// <summary>
        /// The required not satisfied reason.
        /// </summary>
        public virtual DomainSpecificationError RequiredNotSatisfiedDefaultError { get; protected set; }

        /// <summary>
        /// The minimum length not satisfied reason.
        /// </summary>
        public virtual DomainSpecificationError MinLengthNotSatisfiedDefaultError { get; protected set; }

        /// <summary>
        /// The max length not satisfied reason.
        /// </summary>
        public virtual DomainSpecificationError MaxLengthNotSatisfiedDefaultError { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MustComplyWithMetadataSpecificationBase{TTarget}"/> class.
        /// </summary>
        public MustComplyWithMetadataSpecificationBase()
        {
            RequiredNotSatisfiedDefaultError = new DomainSpecificationError(0, "The {0} is required.", string.Empty);
            MinLengthNotSatisfiedDefaultError = new DomainSpecificationError(1, "The {0} minimum length is {1}.", string.Empty);
            MaxLengthNotSatisfiedDefaultError = new DomainSpecificationError(2, "The {0} max length is {1}.", string.Empty);
        }

        #endregion

        #region Interface Methods

        /// <summary>
        /// Determines whether the target object satisfies the specification.
        /// </summary>
        /// <param name="target">The target object to be validated.</param>
        /// <returns><c>true</c> if this instance is satisfied by the specified target; otherwise, <c>false</c>.</returns>
        public override bool IsSatisfiedBy(TTarget target)
        {
            var targetType = target.GetType();

            var propertiesToValidate = targetType.GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(ValidationAttribute), true).Any());

            foreach (var property in propertiesToValidate)
            {
                foreach (ValidationAttribute attributeToValidate in property.GetCustomAttributes(typeof(ValidationAttribute), true))
                {
                    var isValid = attributeToValidate.IsValid(property.GetValue(target));

                    if (!isValid)
                    {
                        SpecificationResult
                            .AddError(GetErrorFromInvalidAttribute(attributeToValidate, property.Name));
                    }
                }
            }


            return base.IsSatisfiedBy(target);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the error from invalid attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        protected virtual DomainSpecificationError GetErrorFromInvalidAttribute(ValidationAttribute attribute, string fieldName)
        {
            DomainSpecificationError returnError = null;
            string errorMessage = string.IsNullOrEmpty(attribute.ErrorMessage) ? null : attribute.ErrorMessage;

            if (attribute is RequiredAttribute)
            {
                returnError = new DomainSpecificationError(RequiredNotSatisfiedDefaultError.ErrorCode,
                    errorMessage ?? RequiredNotSatisfiedDefaultError.NotSatisfiedReason, fieldName);
            }
            else if (attribute is MinLengthAttribute)
            {
                returnError = new DomainSpecificationError(MinLengthNotSatisfiedDefaultError.ErrorCode,
                    errorMessage ?? MinLengthNotSatisfiedDefaultError.NotSatisfiedReason, fieldName);
            }
            else if (attribute is MaxLengthAttribute)
            {
                returnError = new DomainSpecificationError(MaxLengthNotSatisfiedDefaultError.ErrorCode,
                    errorMessage ?? MaxLengthNotSatisfiedDefaultError.NotSatisfiedReason, fieldName);
            }
            else
            {
                returnError = new DomainSpecificationError(-1, errorMessage, fieldName);
            }

            return returnError;
        }

        #endregion
    }
}
