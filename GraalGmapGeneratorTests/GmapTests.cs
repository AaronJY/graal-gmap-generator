using GraalGmapGenerator;
using NUnit.Framework;

namespace GraalGmapGeneratorTests
{
    public class GmapTests
    {
        [Test]
        public void ConstructsAsExpected()
        {
            var expectedName = "My gmap name";
            var expectedWidth = 10;
            var expectedHeight = 22;
            var expectedNoAutomapping = true;
            var expectedLoadFullMap = true;

            var result = new Gmap(expectedName, expectedWidth, expectedHeight, expectedNoAutomapping, expectedLoadFullMap);

            Assert.AreEqual(expectedName, result.Name);
            Assert.AreEqual(expectedWidth, result.Width);
            Assert.AreEqual(expectedHeight, result.Height);
            Assert.AreEqual(expectedNoAutomapping, result.NoAutomapping);
            Assert.AreEqual(expectedLoadFullMap, result.LoadFullMap);
        }
    }
}
