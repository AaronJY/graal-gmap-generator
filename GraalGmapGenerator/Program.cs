using GraalGmapGenerator.Enums;
using System;
using System.IO;

namespace GraalGmapGenerator
{
    class Program
    {
        const string TemplateFile = "template.nw";

        static void Main(string[] args)
        {
            var mapBuilder = new GmapBuilder();

            Console.WriteLine("Gmap name...");
            mapBuilder.SetName(Console.ReadLine());

            Console.WriteLine("Gmap width (in levels, for example: 8)...");
            // need to handle errors
            int width = 0;
            do
            {
                var isValid = int.TryParse(Console.ReadLine(), out width);
                if (!isValid)
                {
                    Console.WriteLine("Please enter a valid gmap width!");
                }
            } while (width == 0);

            Console.WriteLine("Gmap height (in levels, for example: 5)...");
            int height = 0;
            do
            {
                var isValid = int.TryParse(Console.ReadLine(), out height);
                if (!isValid)
                {
                    Console.WriteLine("Please enter a valid gmap height!");
                }
            } while (width == 0);

            mapBuilder.SetDimensions(width, height);

            Console.WriteLine("Load full map? (y/n)...");
            if (Console.ReadLine() == "y")
            {
                mapBuilder.LoadFullMap();
            }

            Console.WriteLine("No automapping? (y/n)...");
            if (Console.ReadLine() == "y")
            {
                mapBuilder.NoAutomapping();
            }

            Console.WriteLine("Generating gmap...");
            var gmap = mapBuilder.Build();

            Console.WriteLine("Saving gmap...");
            SaveGmap(gmap);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        static void SaveGmap(Gmap gmap)
        {
            const string OutputDirRoot = "gmaps";
            var outputDir = $"{OutputDirRoot}/{gmap.Name}";

            // Create output directory if it doesn't exist
            if (!Directory.Exists(OutputDirRoot))
            {
                Directory.CreateDirectory(OutputDirRoot);
            }

            // Create gmap output directory
            Directory.CreateDirectory(outputDir);

            var gmapContentGen = new GmapContentGenerator(LevelType.Nw);

            // Create a new level file for each level
            var levelNames = gmapContentGen.GetLevelNames(gmap);
            foreach (var level in levelNames)
            {
                File.Copy(TemplateFile, $"{outputDir}/{level}");
            }

            // Create the gmap file
            var gmapContent = gmapContentGen.Generate(gmap);
            File.AppendAllText($"{outputDir}/{gmap.Name}.gmap", gmapContent);
        }
    }
}
