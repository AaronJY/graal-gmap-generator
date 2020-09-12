using System.Collections.Generic;

namespace GraalGmapGenerator.Models
{
    public class GmapContent
    {
        public string Content { get; }
        public IEnumerable<Level> Levels { get; }

        public GmapContent(string content, IEnumerable<Level> levels)
        {
            Content = content;
            Levels = levels;
        }
    }
}