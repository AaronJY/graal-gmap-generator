using System;
using System.Collections.Generic;
using System.Text;

namespace GraalGmapGenerator
{
    public class GmapBuilder
    {
        private string _name;
        private int _width;
        private int _height;
        private bool _noAutomapping;
        private bool _loadFullMap;

        public GmapBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public GmapBuilder SetDimensions(int width, int height)
        {
            _width = width;
            _height = height;

            return this;
        }

        public GmapBuilder NoAutomapping()
        {
            _noAutomapping = true;
            return this;
        }

        public GmapBuilder LoadFullMap()
        {
            _loadFullMap = true;
            return this;
        }

        /// <summary>
        /// Builds the gmap
        /// </summary>
        public Gmap Build()
        {
            return new Gmap(_name, _width, _height, _noAutomapping, _loadFullMap);
        }
    }
}
