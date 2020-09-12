using System;
using GraalGmapGenerator.Enums;

namespace GraalGmapGenerator.Models
{
    public class Level
    {
        public const int Width = 64;
        public const int Height = 64;

        public string FileName { get; }
        public int Index { get; }
        public LevelType LevelType { get; }

        public Level(Gmap gmap, int index, LevelType levelType)
        {
            FileName = GetFileName(gmap.Name, index, levelType);
            Index = index;
            LevelType = levelType;
        }

        private static string GetFileExtensionForLevelType(LevelType levelType)
        {
            switch (levelType)
            {
                default:
                    throw new NotImplementedException($"{levelType} has not been implemented.");

                case LevelType.Nw:
                    return ".nw";

                case LevelType.Graal:
                    return ".graal";
            }
        }

        public static string GetFileName(string gmapName, int index, LevelType levelType)
        {
            return $"{gmapName}_{index}{GetFileExtensionForLevelType(levelType)}";
        }
    }
}