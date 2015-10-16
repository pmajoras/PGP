using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using KissSpecifications;
using KissSpecifications.Commons;
using KissSpecifications.Globalization;
using PGP.Infrastructure.Framework.Globalization;
using HelperSharp;

namespace PGP.Infrastructure.Framework.Domain
{
    /// <summary>
    /// Utilitários para especificações.
    /// </summary>
    public static class SpecService
    {
        #region Constructors
        /// <summary>
        /// Inicia os membros estáticos da classe <see cref="SpecService"/>.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
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
        /// Verifica se todas as especificações são satisfeitas. Se uma única não for, será lançada um SpecificationNotSatisfiedException.
        /// </summary>
        /// <typeparam name="TTarget">O tipo do alvo da especificações.</typeparam>
        /// <param name="target">O alvo da especificações.</param>
        /// <param name="specifications">As especificações.</param>
        public static void Assert<TTarget>(TTarget target, params ISpecification<TTarget>[] specifications)
        {
            var notSatisfiedSpecifications = SpecificationService.FilterSpecificationsAreNotSatisfiedBy(target, specifications);
            //return error Object
        }

        /// <summary>
        /// Verifica se todas as especificações são satisfeitas. Se uma única não for, será lançada um SpecificationNotSatisfiedException.
        /// </summary>
        /// <typeparam name="TTarget">O tipo do alvo da especificações.</typeparam>
        /// <param name="targets">Os alvos das especificações.</param>
        /// <param name="specifications">As especificações.</param>
        public static void Assert<TTarget>(IEnumerable<TTarget> targets, params ISpecification<TTarget>[] specifications)
        {
            var notSatisfiedSpecifications = SpecificationService.FilterSpecificationsAreNotSatisfiedBy(targets, specifications);
            //return error Object
        }

        /// <summary>
        /// Verifica se todas as especificações dos grupos informados são satisfeitas. Se uma única não for, será lançada um SpecificationNotSatisfiedException.
        /// </summary>
        /// <typeparam name="TTarget">O tipo do alvo da especificações.</typeparam>
        /// <param name="target">O alvo da especificações.</param>
        /// <param name="groupKeys">As chaves dos grupos de especificações.</param>
        public static void AssertGroups<TTarget>(TTarget target, params object[] groupKeys)
        {
            var notSatisfiedSpecifications = SpecificationService.FilterSpecificationsAreNotSatisfiedBy(target, groupKeys);
            //return error Object
        }
    }
}
