using System;
using System.Collections.Generic;
using System.Text;
using GraalGmapGenerator.Models;
using GraalGmapGenerator.Options;

namespace GraalGmapGenerator.Generators
{
    public class GmapContentGenerator
    {
        readonly GmapContentGeneratorOptions _options;

        public GmapContentGenerator(GmapContentGeneratorOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Returns the gmap file contents
        /// </summary>
        /// <returns></returns>
        public GmapContent Generate(Gmap gmap)
        {
            var stringBuilder = new StringBuilder();
            (string Content, IEnumerable<Level> Levels) data = GenerateLevelNamesList(gmap);

            stringBuilder.AppendLine("GRMAP001");
            stringBuilder.AppendLine($"WIDTH {gmap.Width}");
            stringBuilder.AppendLine($"HEIGHT {gmap.Height}");

            if (gmap.NoAutomapping)
                stringBuilder.AppendLine("NOAUTOMAPPING");

            if (gmap.LoadFullMap)
                stringBuilder.AppendLine("LOADFULLMAP");

            stringBuilder.AppendLine("LEVELNAMES");
            stringBuilder.AppendLine(data.Content);
            stringBuilder.Append("LEVELNAMESEND");

            string content = stringBuilder.ToString();

            return new GmapContent(content, data.Levels);
        }

        private (string content, IEnumerable<Level> levelNames) GenerateLevelNamesList(Gmap gmap)
        {
            // TODO: Split this method up - it's doing too much. But I want to be able to generate
            // the level names and also create a new instance of Level for each level within a single
            // loop, rather than having a loop for each.

            var stringBuilder = new StringBuilder();
            var levels = new List<Level>();
            int gmapArea = gmap.Width * gmap.Height;

            for (int i = 0; i < gmapArea; i++)
            {
                var level = new Level(gmap, i, _options.LevelType);
                levels.Add(level);

                // Start a new line once the current line has hit the width of the gmap
                if (i > 0 && i % gmap.Width == 0)
                    stringBuilder.AppendLine();

                stringBuilder.Append($"\"{level.FileName}\"");

                // Only append a comma if its NOT the end of the row
                if (i % gmap.Width < gmap.Width - 1)
                {
                    stringBuilder.Append(',');
                }
            }

            return (stringBuilder.ToString(), levels);
        }
    }
}
