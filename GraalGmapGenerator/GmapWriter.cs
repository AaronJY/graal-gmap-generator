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

            // Create a new level file for each level
            var levelNames = gmapContentGen.GetLevelNames(gmap);
            foreach (string levelName in levelNames)
            {
                File.Copy(TemplateFile, $"{destinationPath}/{levelName}");
            }

            // Create the gmap file
            var gmapContent = gmapContentGen.Generate(gmap);
            File.AppendAllText($"{destinationPath}/{gmap.Name}.gmap", gmapContent);
        }
    }
}