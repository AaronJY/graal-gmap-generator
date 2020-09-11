using System.IO;
using GraalGmapGenerator.Enums;

namespace GraalGmapGenerator
{
    public static class GmapWriter
    {
        private const string DefaultTemplateFilePath = "template.nw";

        public static void Write(string destinationPath, Gmap gmap)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            var gmapContentGen = new GmapContentGenerator(new Options.GmapContentGenerationOptions
            {
                LevelType = LevelType.Nw
            });
            
            GmapContent gmapContent = gmapContentGen.Generate(gmap);

            var levelContentGen = new LevelContentGenerator(new Options.LevelContentGenerationOptions
            {
                AddLevelLinks = gmap.AddLevelLinks,
                TemplateFilePath = DefaultTemplateFilePath
            });

            foreach (Level level in gmapContent.Levels)
            {
                File.Copy(DefaultTemplateFilePath, $"{destinationPath}/{level.FileName}");

                string filePath = $"{destinationPath}/{level.FileName}";
                string fileContent = levelContentGen.GenerateLevelContent(gmap, level);

                File.WriteAllText(filePath, fileContent);
            }

            File.AppendAllText($"{destinationPath}/{gmap.Name}.gmap", gmapContent.Content);
        }
    }
}