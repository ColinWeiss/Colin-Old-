using Colin.Common.SceneComponents.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.SceneComponents.Tiles
{
    public class Tile
    {
        private int _width = 0;
        public int Width => _width;

        private int _height = 0;
        public int Height => _height;

        public TileInfocollection infos;

        public TileBehaviorCollection behaviors;

        public void Create( int width, int height )
        {
            _width = width;
            _height = height;
            infos = new TileInfocollection( width , height );
            behaviors = new TileBehaviorCollection( width, height );
        }

    }
}