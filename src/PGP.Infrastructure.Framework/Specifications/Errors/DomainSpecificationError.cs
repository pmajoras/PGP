﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGP.Infrastructure.Framework.Specifications.Errors
{
    /// <summary>
    /// A class that represents a Domain Specification Error
    /// </summary>
    public class DomainSpecificationError
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainSpecificationError"/> class.
        /// </summary>
        /// <param name="errorCode">The errorCode.</param>
        /// <param name="notSatisfiedReason">The not satisfied reason.</param>
        /// <param name="fieldName">Name of the field in case of error in a field.</param>
        public DomainSpecificationError(int errorCode, string notSatisfiedReason, string fieldName = null)
        {
            ErrorCode = errorCode;
            NotSatisfiedReason = notSatisfiedReason;
            FieldName = fieldName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public int ErrorCode { get; protected set; }

        /// <summary>
        /// Gets or sets the not satisfied reason.
        /// </summary>
        /// <value>
        /// The not satisfied reason.
        /// </value>
        public string NotSatisfiedReason { get; protected set; }

        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        /// <value>
        /// The name of the field.
        /// </value>
        public string FieldName { get; protected set; }

        #endregion
    }
}
