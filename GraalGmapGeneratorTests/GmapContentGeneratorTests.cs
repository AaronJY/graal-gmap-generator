using GraalGmapGenerator.Enums;
using GraalGmapGenerator.Generators;
using GraalGmapGenerator.Models;
using GraalGmapGenerator.Options;
using GraalGmapGeneratorTests.Fake;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GraalGmapGeneratorTests
{
    [TestFixture]
    public class GmapContentGeneratorTests
    {
        [Test]
        public void Generate_SavesCorrectDimensions()
        {
            Gmap gmap = GmapFake.Get();

            var generator = new GmapContentGenerator(GmapContentGenerationOptionsFake.Get());
            string result = generator.Generate(gmap).Content;
            string[] lines = result.Split("\n\r".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual($"WIDTH {gmap.Width}", lines[1]);
            Assert.AreEqual($"HEIGHT {gmap.Height}", lines[2]);
        }

        [Test]
        public void Generate_SavesHeader()
        {
            Gmap gmap = GmapFake.Get();

            var generator = new GmapContentGenerator(GmapContentGenerationOptionsFake.Get());
            string result = generator.Generate(gmap).Content;
            List<string> lines = SplitContentByLines(result);

            Assert.AreEqual("GRMAP001", lines[0]);
        }

        [Test]
        public void Generate_SaveNoAutomappingLine_WhenNoAutomappingIsTrue()
        {
            Gmap gmap = GmapFake.GetWithAutomappingTrue();

            var generator = new GmapContentGenerator(GmapContentGenerationOptionsFake.Get());
            string result = generator.Generate(gmap).Content;
            List<string> lines = SplitContentByLines(result);

            Assert.IsTrue(lines.Contains("NOAUTOMAPPING"));
        }

        [Test]
        public void Generate_DoesntSaveNoAutomappingLine_WhenNoAutomappingIsFalse()
        {
            Gmap gmap = GmapFake.Get();

            var generator = new GmapContentGenerator(GmapContentGenerationOptionsFake.Get());
            string result = generator.Generate(gmap).Content;
            List<string> lines = SplitContentByLines(result);

            Assert.IsFalse(lines.Contains("NOAUTOMAPPING"));
        }

        [Test]
        public void Generate_SaveLoadFullMapLine_WhenLoadFullMapIsTrue()
        {
            Gmap gmap = GmapFake.GetWithLoadFullMapTrue();

            var generator = new GmapContentGenerator(GmapContentGenerationOptionsFake.Get());
            string result = generator.Generate(gmap).Content;
            List<string> lines = SplitContentByLines(result);

            Assert.IsTrue(lines.Contains("LOADFULLMAP"));
        }

        [Test]
        public void Generate_DoesntSaveLoadFullMapLine_WhenLoadFullMapIsFalse()
        {
            Gmap gmap = GmapFake.Get();

            var generator = new GmapContentGenerator(GmapContentGenerationOptionsFake.Get());
            string result = generator.Generate(gmap).Content;
            List<string> lines = SplitContentByLines(result);

            Assert.IsFalse(lines.Contains("LOADFULLMAP"));
        }

        [Test]
        [TestCase(LevelType.Nw, ".nw")]
        [TestCase(LevelType.Graal, ".graal")]
        public void Generate_SavesValidLevels_ForLevelType(LevelType levelType, string expectedFileExtension)
        {
            Gmap gmap = GmapFake.Get();
            var generator = new GmapContentGenerator(new GmapContentGeneratorOptions
            {
                LevelType = levelType
            });

            string content = generator.Generate(gmap).Content;

            IEnumerable<string> levelNames = GetLevelNamesFromContent(content);
            bool isAllCorrectFileExtension = levelNames.All(levelName => levelName.EndsWith(expectedFileExtension));

            Assert.IsTrue(isAllCorrectFileExtension);
        }

        [Test]
        public void Generate_DoesSaveLevelNamesTags()
        {
            Gmap gmap = GmapFake.Get();
            var generator = new GmapContentGenerator(GmapContentGenerationOptionsFake.Get());
            string result = generator.Generate(gmap).Content;

            Assert.IsTrue(result.Contains("LEVELNAMES", System.StringComparison.Ordinal));
            Assert.IsTrue(result.Contains("LEVELNAMESEND", System.StringComparison.Ordinal));
        }

        private IEnumerable<string> GetLevelNamesFromContent(string content)
        {
            var levelNamePattern = new Regex("\"(.*?)\"");
            return levelNamePattern.Matches(content).Select(x => x.Groups[1].Value);
        }

        private List<string> SplitContentByLines(string content)
        {
            return content.Split("\n\r".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
