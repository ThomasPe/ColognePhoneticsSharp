using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColognePhoneticsSharp.Tests
{
    [TestClass]
    public class UnitTests
    {
        [DataTestMethod]
        [DataRow("Müller-Lüdenscheidt", "65752682")]
        [DataRow("Breschnew", "17863")]
        [DataRow("Hans", "068")]
        [DataRow("Franz", "3768")]
        [DataRow("Schokolade", "8452")]
        [DataRow("Raddampfer", "726137")]
        [DataRow("Marc", "678")]
        [DataRow("Frédéric", "37278")]
        [DataRow("Dominic", "2668")]
        public void GetPhonetics_EncodesReferenceWords(string input, string expected)
        {
            var output = ColognePhonetics.GetPhonetics(input);

            Assert.AreEqual(expected, output);
        }

        [DataTestMethod]
        [DataRow("AEIJOUYÄÖÜ", "0000000000")]
        [DataRow("FVW", "333")]
        [DataRow("GKQ", "444")]
        [DataRow("MN", "66")]
        [DataRow("SZß", "888")]
        [DataRow("BLR", "157")]
        public void GetEncoding_MapsStandardLetterGroups(string input, string expected)
        {
            Assert.AreEqual(expected, ColognePhonetics.GetEncoding(input));
        }

        [DataTestMethod]
        [DataRow("P", "1")]
        [DataRow("PH", "3")]
        [DataRow("X", "48")]
        [DataRow("KX", "48")]
        [DataRow("AX", "048")]
        [DataRow("D", "2")]
        [DataRow("TC", "88")]
        [DataRow("TS", "88")]
        [DataRow("TZ", "88")]
        public void GetEncoding_MapsContextSensitiveLetters(string input, string expected)
        {
            Assert.AreEqual(expected, ColognePhonetics.GetEncoding(input));
        }

        [DataTestMethod]
        [DataRow("C", "8")]
        [DataRow("CA", "40")]
        [DataRow("CE", "80")]
        [DataRow("AC", "08")]
        [DataRow("ACA", "040")]
        [DataRow("ACE", "080")]
        [DataRow("SCA", "880")]
        public void GetEncoding_MapsCAtEveryPosition(string input, string expected)
        {
            Assert.AreEqual(expected, ColognePhonetics.GetEncoding(input));
        }

        [DataTestMethod]
        [DataRow("Müller-Lüdenscheidt", "60550750206880022")]
        [DataRow("a b", "01")]
        [DataRow("A1B", "01")]
        [DataRow("", "")]
        public void GetEncoding_IsCaseInsensitiveAndIgnoresNonLetters(string input, string expected)
        {
            Assert.AreEqual(expected, ColognePhonetics.GetEncoding(input));
        }

        [DataTestMethod]
        [DataRow("60550750206880022", "6050750206802")]
        [DataRow("4488", "48")]
        [DataRow("", "")]
        public void CleanDoubles_RemovesOnlyAdjacentDuplicates(string input, string expected)
        {
            Assert.AreEqual(expected, ColognePhonetics.CleanDoubles(input));
        }

        [DataTestMethod]
        [DataRow("6050750206802", "65752682")]
        [DataRow("000", "0")]
        [DataRow("401020", "412")]
        [DataRow("", "")]
        public void CleanZeros_PreservesOnlyLeadingZero(string input, string expected)
        {
            Assert.AreEqual(expected, ColognePhonetics.CleanZeros(input));
        }
    }
}
