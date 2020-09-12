namespace GraalGmapGenerator.Models
{
    public class Gmap
    {
        public string Name { get; }

        public int Width { get; }

        public int Height { get; set; }

        public bool NoAutomapping { get; }

        public bool LoadFullMap { get; }

        public bool AddLevelLinks { get; }

        public Gmap(
            string name,
            int width,
            int height,
            bool noAutomapping = false,
            bool loadFullMap = false,
            bool addLevelLinks = false)
        {
            Name = name;
            Width = width;
            Height = height;
            NoAutomapping = noAutomapping;
            LoadFullMap = loadFullMap;
            AddLevelLinks = addLevelLinks;
        }
    }
}
