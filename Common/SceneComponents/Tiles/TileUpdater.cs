namespace Colin.Common.SceneComponents.Tiles
{
    /// <summary>
    /// 物块更新器.
    /// [!] 部分代码未使用.
    /// </summary>
    public class TileUpdater
    {
        public bool Enable = true;

        public bool Visiable = true;

        public Rectangle Region;

        /// <summary>
        /// 逻辑刷新.
        /// </summary>
        /// <param name="tileChunks">活跃区块列表.</param>
        public void Update( List<TileChunk> tileChunks )
        {
            TileChunk _chunk;
            for( int count = 0; count < tileChunks.Count; count++ )
            {
                _chunk = tileChunks[count];
                _chunk.UpdateChunk( );
            }
        }

        public void DoUpdate( Tiled tiled )
        {
            if( !Enable )
                return;
            Tile _Tile;
            Rectangle _uRec;
            _uRec = Region;
            int _width = _uRec.X + _uRec.Width;
            int _height = _uRec.Y + _uRec.Height;
            _width = Math.Clamp( _width, 0, tiled.Width );
            _height = Math.Clamp( _height, 0, tiled.Height );
            for( int x = _uRec.X; x < _width; x++ )
            {
                for( int y = _uRec.Y; y < _height; y++ )
                {
                    x = Math.Clamp( x, 0, tiled.Width );
                    y = Math.Clamp( y, 0, tiled.Height );
                    _Tile = tiled.Tiles[x + y * tiled.Width];
                    if( _Tile != null && _Tile.Updateable )
                        _Tile.Update( ref tiled.TileInfoMap[_Tile.TileIndex] );
                }
            }
        }

        public void DoRender( Tiled tiled )
        {
            if( !Visiable )
                return;
            Tile _Tile;
            Rectangle _uRec;
            _uRec = Region;
            int _width = _uRec.X + _uRec.Width;
            int _height = _uRec.Y + _uRec.Height;
            _width = Math.Clamp( _width, 0, tiled.Width );
            _height = Math.Clamp( _height, 0, tiled.Height );
            for( int x = _uRec.X; x < _width; x++ )
            {
                for( int y = _uRec.Y; y < _height; y++ )
                {
                    x = Math.Clamp( x, 0, tiled.Width );
                    y = Math.Clamp( y, 0, tiled.Height );
                    _Tile = tiled.Tiles[x + y * tiled.Width];
                    if( _Tile != null && _Tile.Renderable )
                        _Tile.Render( ref tiled.TileInfoMap[_Tile.TileIndex] );
                }
            }
        }

    }
}