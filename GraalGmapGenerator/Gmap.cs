namespace GraalGmapGenerator
{
    public class Gmap
    {
        public string Name { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool NoAutomapping { get; set; }

        public bool LoadFullMap { get; set; }

        public Gmap(
            string name,
            int width,
            int height,
            bool noAutomapping = false,
            bool loadFullMap = false)
        {
            Name = name;
            Width = width;
            Height = height;
            NoAutomapping = noAutomapping;
            LoadFullMap = loadFullMap;
        }
    }
}
