using GraalGmapGenerator;
using GraalGmapGenerator.Enums;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GraalGmapGeneratorTests
{
    public class GmapContentGeneratorTests
    {
        [Test]
        public void Generate_SavesCorrectDimensions()
        {
            var expectedWidth = 5;
            var expectedHeight = 6;

            var gmap = GetTestGmap();
            gmap.Width = expectedWidth;
            gmap.Height = expectedHeight;

            var generator = new GmapContentGenerator(LevelType.Graal);
            var result = generator.Generate(gmap);
            var lines = result.Split("\n\r".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual($"WIDTH {expectedWidth}", lines[1]);
            Assert.AreEqual($"HEIGHT {expectedHeight}", lines[2]);
        }

        [Test]
        public void Generate_SavesHeader()
        {
            var gmap = GetTestGmap();

            var generator = new GmapContentGenerator(LevelType.Graal);
            var result = generator.Generate(gmap);
            var lines = SplitContentByLines(result);

            Assert.AreEqual("GRMAP001", lines[0]);
        }

        [Test]
        public void Generate_SaveNoAutomappingLine_WhenNoAutomappingIsTrue()
        {
            var gmap = GetTestGmap();
            gmap.NoAutomapping = true;

            var generator = new GmapContentGenerator(LevelType.Graal);
            var result = generator.Generate(gmap);
            var lines = SplitContentByLines(result);

            Assert.IsTrue(lines.Contains("NOAUTOMAPPING"));
        }

        [Test]
        public void Generate_DoesntSaveNoAutomappingLine_WhenNoAutomappingIsFalse()
        {
            var gmap = GetTestGmap();
            gmap.NoAutomapping = false;

            var generator = new GmapContentGenerator(LevelType.Graal);
            var result = generator.Generate(gmap);
            var lines = SplitContentByLines(result);

            Assert.IsFalse(lines.Contains("NOAUTOMAPPING"));
        }

        [Test]
        public void Generate_SaveLoadFullMapLine_WhenLoadFullMapIsTrue()
        {
            var gmap = GetTestGmap();
            gmap.LoadFullMap = true;

            var generator = new GmapContentGenerator(LevelType.Graal);
            var result = generator.Generate(gmap);
            var lines = SplitContentByLines(result);

            Assert.IsTrue(lines.Contains("LOADFULLMAP"));
        }

        [Test]
        public void Generate_DoesntSaveLoadFullMapLine_WhenLoadFullMapIsFalse()
        {
            var gmap = GetTestGmap();
            gmap.LoadFullMap = false;

            var generator = new GmapContentGenerator(LevelType.Graal);
            var result = generator.Generate(gmap);
            var lines = SplitContentByLines(result);

            Assert.IsFalse(lines.Contains("LOADFULLMAP"));
        }

        [Test]
        [TestCase(LevelType.Nw, ".nw")]
        [TestCase(LevelType.Graal, ".graal")]
        public void Generate_SavesValidLevels_ForLevelType(LevelType levelType, string expectedFileExtension)
        {
            var gmap = GetTestGmap();
            var generator = new GmapContentGenerator(levelType);

            var content = generator.Generate(gmap);

            var levelNames = GetLevelNamesFromContent(content);
            var isAllCorrectFileExtension = levelNames.All(levelName => levelName.EndsWith(expectedFileExtension));

            Assert.IsTrue(isAllCorrectFileExtension);
        }

        [Test]
        public void Generate_DoesSaveLevelNamesTags()
        {
            var gmap = GetTestGmap();
            var generator = new GmapContentGenerator(LevelType.Graal);
            var result = generator.Generate(gmap);

            Assert.IsTrue(result.Contains("LEVELNAMES", System.StringComparison.Ordinal));
            Assert.IsTrue(result.Contains("LEVELNAMESEND", System.StringComparison.Ordinal));
        }

        private IEnumerable<string> GetLevelNamesFromContent(string content)
        {
            var levelNamePattern = new Regex("\"(.*?)\"");
            return levelNamePattern.Matches(content).Select(x => x.Groups[1].Value);
        }

        private Gmap GetTestGmap()
        {
            return new Gmap("My test gmap", 10, 11, true, true);
        }

        private List<string> SplitContentByLines(string content)
        {
            return content.Split("\n\r".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
