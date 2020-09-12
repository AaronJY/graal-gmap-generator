using GraalGmapGenerator.Options;

namespace GraalGmapGeneratorTests.Fake
{
    internal static class GmapContentGenerationOptionsFake
    {
        internal static GmapContentGeneratorOptions Get()
        {
            return new GmapContentGeneratorOptions
            {
                LevelType = GraalGmapGenerator.Enums.LevelType.Graal
            };
        }
    }
}