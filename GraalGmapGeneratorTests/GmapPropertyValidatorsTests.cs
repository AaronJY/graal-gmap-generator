using System;
using System.Collections.Generic;
using System.Linq;
using GraalGmapGenerator;
using NUnit.Framework;

namespace GraalGmapGeneratorTests
{
    [TestFixture]
    public class GmapPropertyValidatorsTests
    {
        internal static IEnumerable<string> GetInvalidPaths()
        {
            const string dummyPath = "dir/";
            char[] invalidChars = System.IO.Path.GetInvalidPathChars();
            foreach (char invalidChar in invalidChars)
            {
                yield return dummyPath + invalidChar;
            }
        }
        
        [TestCase("-1")]
        [TestCase("0")]
        [TestCase("abc")]
        [TestCase("£$%^&*")]
        [TestCase("123a")]
        public void IsValidDimension_IfInvalidDimension_ReturnsFalse(string invalidDimension)
        {
            Assert.False(GmapPropertyValidators.IsValidDimension(invalidDimension));
        }

        [TestCase("1")]
        [TestCase("12345")]
        [TestCase("10")]
        [TestCase("2147483647")]
        public void IsValidDimension_IfValidDimension_ReturnsTrue(string validDimension)
        {
            Assert.True(GmapPropertyValidators.IsValidDimension(validDimension));
        }

        [TestCase("")]
        [TestCase("oopsie")]
        [TestCase("ye")]
        [TestCase("yess")]
        [TestCase("noo")]
        [TestCase("12345")]
        public void IsValidYesNoInput_IfIsInvalidValidInput_ReturnsFalse(string invalidInput)
        {
            Assert.False(GmapPropertyValidators.IsValidYesNoInput(invalidInput));
        }

        [TestCase("y")]
        [TestCase("yes")]
        [TestCase("n")]
        [TestCase("no")]
        public void IsValidYesNoInput_IfIsValidInput_ReturnsTrue(string validInput)
        {
            Assert.True(GmapPropertyValidators.IsValidYesNoInput(validInput));
        }

        [TestCaseSource(nameof(GetInvalidPaths))]
        public void IsValidDirectory_IfIsInvalidDirectory_ReturnsFalse(string invalidPath)
        {
            Assert.False(GmapPropertyValidators.IsValidDirectory(invalidPath));
        }

        [TestCase("my/path")]
        [TestCase("my/path/")]
        [TestCase("directory")]
        [TestCase("C:/users/Aaron/gmaps")]
        public void IsValidDirectory_IfIsValidDirectory_ReturnsTrue(string validPath)
        {
            Assert.True(GmapPropertyValidators.IsValidDirectory(validPath));
        }

        
    }
}