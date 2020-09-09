using GraalGmapGenerator;

namespace GraalGmapGeneratorTests.Fake
{
    internal static class GmapFake
    {
        private const string DefaultName = "Test gmap";
        private const int DefaultWidth = 8;
        private const int DefaultHeight = 10;
        private const bool DefaultNoAutomapping = false;
        private const bool DefaultLoadFullMap = false;
        private const bool DefaultAddLevelLinks = false;

        internal static Gmap Get()
        {
            return new Gmap(
                DefaultName,
                DefaultWidth,
                DefaultHeight,
                noAutomapping: DefaultNoAutomapping,
                loadFullMap: DefaultLoadFullMap,
                addLevelLinks: DefaultAddLevelLinks);
        }
        
        internal static Gmap GetWithAutomappingTrue()
        {
            return new Gmap(
                DefaultName,
                DefaultWidth,
                DefaultHeight,
                noAutomapping: true,
                loadFullMap: DefaultLoadFullMap,
                addLevelLinks: DefaultAddLevelLinks);
        }

        internal static Gmap GetWithLoadFullMapTrue()
        {
            return new Gmap(
                DefaultName,
                DefaultWidth,
                DefaultHeight,
                noAutomapping: DefaultNoAutomapping,
                loadFullMap: true,
                addLevelLinks: DefaultAddLevelLinks);
        }

        internal static Gmap GetWithAddLevelLinksTrue()
        {
            return new Gmap(
                DefaultName,
                DefaultWidth,
                DefaultHeight,
                noAutomapping: DefaultNoAutomapping,
                loadFullMap: DefaultLoadFullMap,
                addLevelLinks: true);
        }
    }
}