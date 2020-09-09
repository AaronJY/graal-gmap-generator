using System;
using System.IO;

namespace GraalGmapGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
                "Welcome to the GMAP generator. You can use this tool to easily generate a GMAP file " +
                "\nwith the accompanying level files. Simply provide values for settings below, hitting enter " +
                "\nto move to the next setting."
            );
            Console.WriteLine();
            Console.WriteLine(
                "If you run into any problems, drop me an email at me@aaronjy.me and I " +
                "\nwill endeavour to respond as soon as possible. " +
                "\nThanks for using my software! - Aaron Yarborough"
            );
            Console.WriteLine("------------------------");

            var mapBuilder = new GmapBuilder();

            Console.WriteLine("Gmap name...");
            string gmapName = Console.ReadLine();
            mapBuilder.SetName(gmapName);

            Console.WriteLine("Gmap width (in levels, for example: 8)...");
            int width = int.Parse(
                GetInput(
                    inputFunc: () => Console.ReadLine(),
                    validator: (input) => 
                    {
                        if (!GmapPropertyValidators.IsValidDimension(input))
                        {
                            Console.WriteLine("Please enter a valid gmap width!");
                            return false;
                        }

                        return true;
                    }
                )
            );

            Console.WriteLine("Gmap height (in levels, for example: 5)...");
            int height = int.Parse(
                GetInput(
                    inputFunc: () => Console.ReadLine(),
                    validator: (input) => 
                    {
                        if (!GmapPropertyValidators.IsValidDimension(input))
                        {
                            Console.WriteLine("Please enter a valid gmap height!");
                            return false;
                        }

                        return true;
                    }
                )
            );

            mapBuilder.SetDimensions(width, height);

            Console.WriteLine("Load full map? (y/n)...");
            Console.WriteLine("INFO: Loads all map parts into memory on startup.");

            string loadFullMapStr = GetInput(
                inputFunc: () => Console.ReadLine(),
                validator: (input) => 
                {
                    if (!GmapPropertyValidators.IsValidYesNoInput(input))
                    {
                        Console.WriteLine("Please provide a valid \"y\" or \"n\" value!");
                        return false;
                    }

                    return true;
                }
            );

            mapBuilder.LoadFullMap(Helpers.YesNoToBool(loadFullMapStr));

            Console.WriteLine("No automapping? (y/n)...");
            Console.WriteLine("INFO: Disables the assembly of automagical screenshots into a map that is drawn over the MAPIMG image.");
            
            string noAutoMappingStr = GetInput(
                inputFunc: () => Console.ReadLine(),
                validator: (input) => 
                {
                    if (!GmapPropertyValidators.IsValidYesNoInput(input))
                    {
                        Console.WriteLine("Please provide a valid \"y\" or \"n\" value!");
                        return false;
                    }

                    return true;
                }
            );

            mapBuilder.NoAutomapping(Helpers.YesNoToBool(noAutoMappingStr));

            Console.WriteLine("Save directory...");
            Console.WriteLine($"INFO: If you do not wish to provide a save directory, you can leave this setting blank (hit ENTER) and the GMAP will be created under \"gmaps/\" in the application directory ({AppDomain.CurrentDomain.BaseDirectory}/gmaps/{gmapName}/)");

            string saveDirectory = GetInput(
                () => Console.ReadLine(),
                (input) =>
                {
                    if (input != "" && !GmapPropertyValidators.IsValidDirectory(input))
                    {
                        Console.WriteLine("Please provide a valid directory path.");
                        return false;
                    }

                    return true;
                }
            );

            if (saveDirectory == "")
            {
                saveDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gmaps", gmapName);
            }

            Console.WriteLine("Generating gmap...");
            var gmap = mapBuilder.Build();

            Console.WriteLine("Saving gmap...");
            GmapWriter.SaveGmap(saveDirectory, gmap);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static string GetInput(Func<string> inputFunc, Func<string, bool> validator)
        {
            do
            {
                string inputResolved = inputFunc();
                if (validator(inputResolved))
                {
                    return inputResolved;
                }
            } while (true);
        }
    }
}
