﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using KissSpecifications;
using KissSpecifications.Commons;
using KissSpecifications.Globalization;
using PGP.Infrastructure.Framework.Globalization;
using HelperSharp;

namespace PGP.Infrastructure.Framework.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public static class SpecService
    {
        #region Constructors     
        /// <summary>
        /// Initializes the <see cref="SpecService"/> class.
        /// </summary>
        static SpecService()
        {
            KissSpecificationsConfig.GlobalizationResolver = new FuncGlobalizationResolver((text) =>
            {
                //switch (text)
                //{
                //    case MustNotHaveNullOrDefaultPropertySpecification<object>.NotSatisfiedReasonText:
                //        return Texts.NullOrDefaultPropertySpecification;

                //    case MustNotBeNullSpecification<object>.NotSatisfiedReasonText:
                //        return Texts.MustNotBeNullSpecification;

                //    case MustExistsSpecification<object, object>.NotSatisfiedReasonText:
                //        return Texts.MustExistsSpecification;

                //    case MustHaveUniqueTextSpecification<object>.NotSatisfiedReasonText:
                //        return Texts.ThereIsOtherEntityWithSameName;

                //    case MustComplyWithMetadataSpecification<object>.MinLengthNotSatisfiedReason:
                //        return Texts.MinLengthNotSatisfiedReason;

                //    case MustComplyWithMetadataSpecification<object>.MaxLengthNotSatisfiedReason:
                //        return Texts.MaxLengthNotSatisfiedReason;

                //    case MustComplyWithMetadataSpecification<object>.RequiredNotSatisfiedReason:
                //        return Texts.RequiredNotSatisfiedReason;

                //    case MustComplyWithMetadataSpecification<object>.UrlNotSatisfiedReason:
                //        return Texts.UrlNotSatisfiedReason;

                //    case MustHaveUniqueValueSpec<object, object>.NotSatisfiedReasonText:
                //        return Texts.ThereIsOtherEntityWithSameValue;

                //    default:
                //        return GlobalizationHelper.
                //}

                return text;
            });
        }
        #endregion

        /// <summary>
        /// Asserts the specified target.
        /// </summary>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="specifications">The specifications.</param>
        public static void Assert<TTarget>(TTarget target, params ISpecification<TTarget>[] specifications)
        {
            var notSatisfiedSpecifications = SpecificationService.FilterSpecificationsAreNotSatisfiedBy(target, specifications);
            //return error Object
        }

        /// <summary>
        /// Asserts the specified targets.
        /// </summary>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="targets">The targets.</param>
        /// <param name="specifications">The specifications.</param>
        public static void Assert<TTarget>(IEnumerable<TTarget> targets, params ISpecification<TTarget>[] specifications)
        {
            var notSatisfiedSpecifications = SpecificationService.FilterSpecificationsAreNotSatisfiedBy(targets, specifications);
            //return error Object
        }

        /// <summary>
        /// Asserts the groups.
        /// </summary>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="groupKeys">The group keys.</param>
        public static void AssertGroups<TTarget>(TTarget target, params object[] groupKeys)
        {
            var notSatisfiedSpecifications = SpecificationService.FilterSpecificationsAreNotSatisfiedBy(target, groupKeys);
            //return error Object
        }
    }
}
