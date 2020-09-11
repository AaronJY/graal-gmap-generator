namespace GraalGmapGenerator
{
    public class LevelLink
    {
        public string LevelFileName { get; }
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public string DestinationX { get; }
        public string DestinationY { get; }

        public LevelLink(string levelFileName, int x, int y, int width, int height, string destinationX, string destinationY)
        {
            LevelFileName = levelFileName;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            DestinationX = destinationX;
            DestinationY = destinationY;
        }

        public override string ToString()
        {
            return $"LINK {LevelFileName} {X} {Y} {Width} {Height} {DestinationX} {DestinationY}";
        }
    }
}