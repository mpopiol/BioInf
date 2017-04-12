using BioInf.Model;
using NUnit.Framework;

namespace BioInf.Test
{
    [TestFixture]
    public class NucleotidTest
    {
        private Nucleotid nucleotid;

        [TestFixtureSetUp]
        public void SetUp()
        {
            nucleotid = new Nucleotid();
        }

        [Test]
        public void Nucleotid_StartsWith_ReturnsFalse_OnEmptySubSeq()
        {
            nucleotid.Sequence = "ACCCG";
            var testSubstring = "";
            var result = nucleotid.StartsWith(testSubstring);

            Assert.False(result);
        }

        [Test]
        public void Nucleotid_StartsWith_ReturnsFalse_OnEmptySeq()
        {
            nucleotid.Sequence = "";
            var testSubstring = "AG";
            var result = nucleotid.StartsWith(testSubstring);

            Assert.False(result);
        }

        [Test]
        public void Nucleotid_StartsWith_ReturnsTrue_OnMatchingWholeSeq()
        {
            nucleotid.Sequence = "ACCTGGG";
            var testSubstring = "ACC";
            var result = nucleotid.StartsWith(testSubstring);

            Assert.True(result);
        }

        [Test]
        public void Nucleotid_StartsWith_ReturnsTrue_MatchingTwoDeltaOne()
        {
            nucleotid.Sequence = "ACCTGGG";
            var testSubstring = "ACG";
            var result = nucleotid.StartsWith(testSubstring, 1);

            Assert.True(result);
        }

        [Test]
        public void Nucleotid_EndsWith_ReturnsFalse_OnEmptySeq()
        {
            nucleotid.Sequence = "";
            var testSubstring = "ACG";
            var result = nucleotid.EndsWith(testSubstring);
        }
    }
}