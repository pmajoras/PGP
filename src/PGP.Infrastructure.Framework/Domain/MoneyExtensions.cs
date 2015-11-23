using System;
using System.Collections.Generic;
using System.Linq;

namespace GoImage.PortalFotografico.Infrastructure.Framework.Domain
{
    /// <summary>
    /// Extensions methods para Money.
    /// </summary>
    public static class MoneyExtensions
    {
        /// <summary>
        /// Sums the specified selector.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        public static Money Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Money> selector)
        {
            if (source.Count() == 0)
            {
                return Money.Reais(0);
            }

            var first = selector(source.First());

            return Money.SameCurrency(first ?? Money.Reais(0), source.Sum(s => selector(s) == null ? 0 : selector(s).Amount));
        }

        /// <summary>
        /// Averages the specified selector.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        public static Money Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Money> selector)
        {
            if (source.Count() == 0)
            {
                return Money.Reais(0);
            }

            var first = selector(source.First());

            return Money.SameCurrency(first ?? Money.Reais(0), source.Average(s => selector(s) == null ? 0 : selector(s).Amount));
        }
    }
}
