using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraalGmapGenerator
{
    public class Gmap
    {
        public string Name { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool NoAutomapping { get; set; }

        public bool LoadFullMap { get; set; }

        #region Constructors

        public Gmap()
        {
        }

        public Gmap(
            string name,
            int width,
            int height,
            bool noAutomapping = false,
            bool loadFullMap = false)
            : this()
        {
            Name = name;
            Width = width;
            Height = height;
            NoAutomapping = noAutomapping;
            LoadFullMap = loadFullMap;
        }

        #endregion

       
    }
}
