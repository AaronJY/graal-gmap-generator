using GraalGmapGenerator.Options;

namespace GraalGmapGeneratorTests.Fake
{
    internal static class GmapContentGenerationOptionsFake
    {
        internal static GmapContentGenerationOptions Get()
        {
            return new GmapContentGenerationOptions
            {
                LevelType = GraalGmapGenerator.Enums.LevelType.Graal
            };
        }
    }
}