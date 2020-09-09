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
            GmapContent gmapContent = gmapContentGen.Generate(gmap);

            foreach (Level level in gmapContent.Levels)
            {
                File.Copy(TemplateFile, $"{destinationPath}/{level.FileName}");
            }

            File.AppendAllText($"{destinationPath}/{gmap.Name}.gmap", gmapContent.Content);
        }
    }
}