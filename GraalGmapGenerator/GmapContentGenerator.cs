using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraalGmapGenerator.Enums;

namespace GraalGmapGenerator
{
    public class GmapContentGenerator
    {
        readonly LevelType _levelType;

        public GmapContentGenerator(LevelType levelType)
        {
            _levelType = levelType;
        }

        /// <summary>
        /// Returns the gmap file contents
        /// </summary>
        /// <returns></returns>
        public string Generate(Gmap gmap)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("GRMAP001");
            stringBuilder.AppendLine($"WIDTH {gmap.Width}");
            stringBuilder.AppendLine($"HEIGHT {gmap.Height}");

            if (gmap.NoAutomapping)
                stringBuilder.AppendLine("NOAUTOMAPPING");

            if (gmap.LoadFullMap)
                stringBuilder.AppendLine("LOADFULLMAP");

            stringBuilder.AppendLine("LEVELNAMES");

            var levelNames = GetLevelNames(gmap).ToList();
            for (var i = 0; i < levelNames.Count; i++)
            {
                // Start a new line once the current line has hit the width of the gmap
                if (i > 0 && i % gmap.Width == 0)
                    stringBuilder.AppendLine();

                var levelName = GetLevelName(i, gmap.Name, _levelType);
                stringBuilder.Append($"\"{levelName}\"");

                // Only append a comma if its NOT the end of the row
                if (i % gmap.Width < (gmap.Width - 1))
                {
                    stringBuilder.Append(',');
                }
            }

            stringBuilder.AppendLine();
            stringBuilder.Append("LEVELNAMESEND");

            return stringBuilder.ToString();
        }

        public IEnumerable<string> GetLevelNames(Gmap gmap)
        {
            var levelNames = new List<string>();

            for (var i = 0; i < (gmap.Width * gmap.Height); i++)
            {
                levelNames.Add(GetLevelName(i, gmap.Name, _levelType));
            }

            return levelNames;
        }

        private string GetLevelName(int index, string name, LevelType levelType)
        {
            string extension;

            switch (levelType)
            {
                default:
                case LevelType.Nw:
                    extension = ".nw"; 
                break;

                case LevelType.Graal:
                    extension = ".graal"; 
                    break;
            }

            return $"{name}_{index}{extension}";
        }
    }
}
