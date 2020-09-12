﻿using GraalGmapGenerator.Models;

namespace GraalGmapGenerator
{
    public class GmapBuilder
    {
        private string _name;
        private int _width;
        private int _height;
        private bool _noAutomapping;
        private bool _loadFullMap;
        private bool _addLevelLinks;

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

        public GmapBuilder NoAutomapping(bool value)
        {
            _noAutomapping = value;
            return this;
        }

        public GmapBuilder LoadFullMap(bool value)
        {
            _loadFullMap = value;
            return this;
        }

        public GmapBuilder AddLevelLinks(bool value)
        {
            _addLevelLinks = value;
            return this;
        }
        
        public Gmap Build()
        {
            return new Gmap(
                _name,
                _width,
                _height,
                _noAutomapping,
                _loadFullMap,
                _addLevelLinks);
        }
    }
}
