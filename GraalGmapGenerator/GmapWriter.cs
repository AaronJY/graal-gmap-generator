using System.Collections.Generic;
using System.IO;
using GraalGmapGenerator.Enums;

namespace GraalGmapGenerator
{
    public static class GmapWriter
    {
        private const string TemplateFile = "template.nw";

        public static void SaveGmap(string destinationPath, Gmap gmap)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            var gmapContentGen = new GmapContentGenerator(LevelType.Nw);

            IEnumerable<string> levelNames = gmapContentGen.GetLevelNames(gmap);
            foreach (string levelName in levelNames)
            {
                File.Copy(TemplateFile, $"{destinationPath}/{levelName}");
            }

            string gmapContent = gmapContentGen.Generate(gmap);
            File.AppendAllText($"{destinationPath}/{gmap.Name}.gmap", gmapContent);
        }
    }
}