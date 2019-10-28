using GraalGmapGenerator;
using NUnit.Framework;

namespace GraalGmapGeneratorTests
{
    public class GmapBuilderTests
    {
        GmapBuilder gmapBuilder;

        [SetUp]
        public void Setup()
        {
            gmapBuilder = new GmapBuilder();
        }

        [Test]
        public void SetName_SetsTheName()
        {
            var expectedName = "My gmap test name";

            gmapBuilder.SetName(expectedName);

            var gmap = gmapBuilder.Build();
            Assert.AreEqual(expectedName, gmap.Name);
        }

        [Test]
        public void SetDimensions_SetsTheDimensions()
        {
            var expectedWidth = 10;
            var expectedHeight = 20;

            gmapBuilder.SetDimensions(expectedWidth, expectedHeight);

            var gmap = gmapBuilder.Build();
            Assert.AreEqual(expectedWidth, gmap.Width);
            Assert.AreEqual(expectedHeight, gmap.Height);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void NoAutomapping_SetsNoAutomapping(bool expectedNoAutomappingValue)
        {
            gmapBuilder.NoAutomapping(expectedNoAutomappingValue);

            var gmap = gmapBuilder.Build();
            Assert.AreEqual(expectedNoAutomappingValue, gmap.NoAutomapping);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void loadFullMap_SetsLoadFullMap(bool expectedLoadFullMapValue)
        {
            gmapBuilder.LoadFullMap(expectedLoadFullMapValue);

            var gmap = gmapBuilder.Build();
            Assert.AreEqual(expectedLoadFullMapValue, gmap.LoadFullMap);
        }
    }
}
