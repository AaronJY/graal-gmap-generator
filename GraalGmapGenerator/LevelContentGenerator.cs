using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GraalGmapGenerator.Options;

namespace GraalGmapGenerator
{
    public class LevelContentGenerator
    {
        private string _templateContent;
        private LevelContentGenerationOptions _options;

        public LevelContentGenerator(LevelContentGenerationOptions options)
        {
            _options = options;
            _templateContent = GetTemplateFileContent(options.TemplateFilePath);
        }

        public string GenerateLevelContent(Gmap gmap, Level level)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(_templateContent);

            if (_options.AddLevelLinks)
            {
                IEnumerable<LevelLink> links = GetLevelLinks(gmap, level);
                foreach(LevelLink link in links)
                {
                    stringBuilder.AppendLine(link.ToString());
                }
            }

            return stringBuilder.ToString();
        }

        private IEnumerable<LevelLink> GetLevelLinks(Gmap gmap, Level level)
        {
            // Level is not on the first row
            if (level.Index > gmap.Width)
            {
                int linkLevelIndex = level.Index - gmap.Width;
                string levelFileName = Level.GetFileName(gmap.Name, linkLevelIndex, level.LevelType);

                yield return new LevelLink(
                    levelFileName, 0, 0, Level.Width, 1, "playerx", "61"
                );
            }

            // If level is not the left-most on its row
            if (level.Index % gmap.Width > 1)
            {
                int linkLevelIndex = level.Index - 1;
                string levelFileName = Level.GetFileName(gmap.Name, linkLevelIndex, level.LevelType);

                yield return new LevelLink(
                    levelFileName, 0, 0, 1, Level.Height, "63", "playery"
                );
            }

            // If level is not on the bottom row
            if (level.Index < (gmap.Width * gmap.Height) - gmap.Width - 1)
            {
                int linkLevelIndex = level.Index + gmap.Width;
                string levelFileName = Level.GetFileName(gmap.Name, linkLevelIndex, level.LevelType);

                yield return new LevelLink(
                    levelFileName, 0, Level.Height - 1, Level.Width, 1, "playerx", "3"
                );
            }

            // If level is not the right-most on its row
            if (level.Index % gmap.Width > 0)
            {
                int linkLevelIndex = level.Index - 1;
                string levelFileName = Level.GetFileName(gmap.Name, linkLevelIndex, level.LevelType);

                yield return new LevelLink(
                    levelFileName, 0, 0, 1, Level.Height, "63", "playery"
                );
            }
        }

        private string GetTemplateFileContent(string templateFilePath)
        {
            if (!File.Exists(templateFilePath))
            {
                throw new FileNotFoundException($"Template file not found", templateFilePath);
            }

            return File.ReadAllText(templateFilePath);
        }
    }
}