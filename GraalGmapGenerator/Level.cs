using System;
using GraalGmapGenerator.Enums;

namespace GraalGmapGenerator
{
    public class Level
    {
        public string FileName { get; }
        public int Index { get; }
        public LevelType LevelType { get; }

        public Level(Gmap gmap, int index, LevelType levelType)
        {
            FileName = $"{gmap.Name}_{index}{GetFileExtensionForLevelType(levelType)}";
            Index = index;
            LevelType = levelType;
        }

        private string GetFileExtensionForLevelType(LevelType levelType)
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
    }
}