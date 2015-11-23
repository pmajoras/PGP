using PGP.Infrastructure.Framework.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PGP.Infrastructure.Framework.Tests.Globalization
{
    [TestClass]
    public class CultureInfoHelperTest
    {
        [TestMethod]
        public void GetCultureInfoByCurrency_ValidIsoCurrencySimbol_CultureInfo()
        {
            var actual = CultureInfoHelper.GetCultureInfoByCurrency("BRL");
            Assert.AreEqual("pt-BR", actual.Name);

            actual = CultureInfoHelper.GetCultureInfoByCurrency("USD");
            Assert.AreEqual("en-US", actual.Name);
        }
    }
}
