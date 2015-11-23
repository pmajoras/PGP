using System;
using System.Globalization;
using HelperSharp;
using PGP.Infrastructure.Framework.Globalization;

namespace GoImage.PortalFotografico.Infrastructure.Framework.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class Money
    {
        #region Fields
        private CultureInfo m_cultureInfo;
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="Money"/> class.
        /// </summary>
        public Money()
            : this(0, "R$")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Money"/> class.
        /// </summary>
        /// <param name="amount">The amount.</param>
        public Money(decimal amount)
            : this(amount, "R$")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Money"/> class.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="currency">The currency.</param>
        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
        #endregion

        #region Properties 

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets the culture information.
        /// </summary>
        /// <value>
        /// The culture information.
        /// </value>
        private CultureInfo CultureInfo
        {
            get
            {
                if (m_cultureInfo == null)
                {
                    m_cultureInfo = CultureInfoHelper.GetCultureInfoByCurrency(Currency);
                }

                return m_cultureInfo;
            }
        }
        #endregion

        #region Operators

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Money operator +(Money left, Money right)
        {
            ExceptionHelper.ThrowIfNull("left", left);
            ExceptionHelper.ThrowIfNull("right", right);

            EnsureSameCurrency(left, right);

            return Money.SameCurrency(left, Math.Round(left.Amount + right.Amount, 2));
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Money operator -(Money left, Money right)
        {
            ExceptionHelper.ThrowIfNull("left", left);
            ExceptionHelper.ThrowIfNull("right", right);

            EnsureSameCurrency(left, right);

            return Money.SameCurrency(left, Math.Round(left.Amount - right.Amount, 2));
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Money operator *(Money left, int right)
        {
            ExceptionHelper.ThrowIfNull("left", left);

            return Money.SameCurrency(left, Math.Round(left.Amount * right, 2));
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Money operator *(Money left, decimal right)
        {
            ExceptionHelper.ThrowIfNull("left", left);

            return Money.SameCurrency(left, Math.Round(left.Amount * right, 2));
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Money operator /(Money left, decimal right)
        {
            ExceptionHelper.ThrowIfNull("left", left);

            return Money.SameCurrency(left, Math.Round(left.Amount / right, 2));
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Money left, Money right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (null == (object)left || null == (object)right)
            {
                return false;
            }

            return left.Amount == right.Amount && left.Currency == right.Currency;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Money left, Money right)
        {
            if (ReferenceEquals(left, right))
            {
                return false;
            }

            if (null == (object)left || null == (object)right)
            {
                return true;
            }

            return left.Amount != right.Amount || left.Currency != right.Currency;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reaises the specified amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public static Money Reais(decimal amount)
        {
            return new Money(amount, "BRL");
        }

        /// <summary>
        /// Uses the dollars.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public static Money USDollars(decimal amount)
        {
            return new Money(amount, "USD");
        }

        /// <summary>
        /// Sames the currency.
        /// </summary>
        /// <param name="money">The money.</param>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public static Money SameCurrency(Money money, decimal amount)
        {
            return new Money(amount, money.Currency);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return String.Format(CultureInfo, "{0:c2}", Amount);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            Money money = obj as Money;
            if (null == (object)money)
            {
                return false;
            }

            return money.Amount == this.Amount && money.Currency == this.Currency;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Adds the specified right.
        /// </summary>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public Money Add(Money right)
        {
            return this + right;
        }

        /// <summary>
        /// Subtracts the specified right.
        /// </summary>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public Money Subtract(Money right)
        {
            return this - right;
        }

        /// <summary>
        /// Multiplies the specified multiplier.
        /// </summary>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        public Money Multiply(int multiplier)
        {
            return this * multiplier;
        }

        /// <summary>
        /// Multiplies the specified multiplier.
        /// </summary>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        public Money Multiply(decimal multiplier)
        {
            return this * multiplier;
        }

        /// <summary>
        /// Divides the specified divider.
        /// </summary>
        /// <param name="divider">The divider.</param>
        /// <returns></returns>
        public Money Divide(decimal divider)
        {
            return this / divider;
        }

        /// <summary>
        /// Ensures the same currency.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        private static void EnsureSameCurrency(Money left, Money right)
        {
            if (left.Currency != right.Currency)
            {
                throw new InvalidOperationException("InvalidMoneyOperationDifferentCurrencies");
            }
        }

        #endregion
    }
}
