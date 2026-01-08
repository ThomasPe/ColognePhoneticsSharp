using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColognePhoneticsSharp.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void GetPhoneticsTest()
        {
            var input = "Müller-Lüdenscheidt";
            var expected = "65752682";
            var output = ColognePhonetics.GetPhonetics(input);
            Assert.AreEqual(expected, output);

            var input2 = "Breschnew";
            var expected2 = "17863";
            var output2 = ColognePhonetics.GetPhonetics(input2);
            Assert.AreEqual(expected2, output2);

            Assert.AreEqual("068", ColognePhonetics.GetPhonetics("Hans"));
            Assert.AreEqual("3768", ColognePhonetics.GetPhonetics("Franz"));
            Assert.AreEqual("8452", ColognePhonetics.GetPhonetics("Schokolade"));
            Assert.AreEqual("726137", ColognePhonetics.GetPhonetics("Raddampfer"));
        }

        [TestMethod]
        public void GetEncodingTest()
        {
            var input = "Müller-Lüdenscheidt";
            var expected = "60550750206880022";

            var output = ColognePhonetics.GetEncoding(input);
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void CleanDoublesTest()
        {
            var input = "60550750206880022";
            var expected = "6050750206802";

            var output = ColognePhonetics.CleanDoubles(input);
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void CleanZerosTest()
        {
            var input = "6050750206802";
            var expected = "65752682";

            var output = ColognePhonetics.CleanZeros(input);
            Assert.AreEqual(expected, output);
        }

        [TestMethod]
        public void GetEncodingOneChar()
        {
            // Test single character 'C' - should not throw IndexOutOfRangeException
            var input2 = "C";
            var expected2 = "8";
            var output2 = ColognePhonetics.GetPhonetics(input2);
            Assert.AreEqual(expected2, output2);
            
            // Test a few other single characters to ensure no regressions
            Assert.AreEqual("4", ColognePhonetics.GetPhonetics("K"));
            Assert.AreEqual("8", ColognePhonetics.GetPhonetics("S"));
            Assert.AreEqual("0", ColognePhonetics.GetPhonetics("A"));
        }
    }
}
