namespace Colin.Common.SceneComponents.Tiles
{
    /// <summary>
    /// 瓦片信息记录集合.
    /// </summary>
    [Serializable]
    public struct TileInfoMap
    {
        public int Width { get; }

        public int Height { get; }

        public int Length => _tiles.Length;

        private TileInfo[ ] _tiles;

        public ref TileInfo this[int index] => ref _tiles[index];

        public TileInfoMap( int width, int height )
        {
            Width = width;
            Height = height;
            _tiles = new TileInfo[Width * Height];
            TileInfo _emptyTile = new TileInfo( );
            _emptyTile.Empty = true;
            Span<TileInfo> _map = _tiles;
            _map.Fill( _emptyTile );
        }

        internal void SetTileDefaultInfo( int coordinateX, int coordinateY )
        {
            int id = coordinateX + coordinateY * Width;
            if( _tiles[id].Empty )
            {
                _tiles[id].CoordinateX = coordinateX;
                _tiles[id].CoordinateY = coordinateY;
                _tiles[id].ID = id;
                _tiles[id].Empty = true;
            }
        }

        internal void RemoveTileInfo( int coordinateX, int coordinateY )
        {
            int id = coordinateX + coordinateY * Width;
            if( !_tiles[id].Empty )
            {
                _tiles[id].ID = 0;
                _tiles[id].Empty = false;
            }
        }

    }
}