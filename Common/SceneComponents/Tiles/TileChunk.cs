using Colin.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Colin.Common.SceneComponents.Tiles
{
    /// <summary>
    /// 物块区块, 恒定 64x64.
    /// </summary>
    public sealed class TileChunk
    {
        /// <summary>
        /// 物块集合.
        /// </summary>
        public Tile[ ] Tile;

        /// <summary>
        /// 记录的信息.
        /// </summary>
        public TileInfoMap TileInfoMap;

        public int X { get; }

        public int Y { get; }

        public const int Width = 64;

        public const int Height = 64;

        public void UpdateChunk( )
        {
            Tile _Tile;
            for( int x = 0; x < Width; x++ )
            {
                for( int y = 0; y < Height; y++ )
                {
                    _Tile = Tile[x + y * Width];
                    if( _Tile != null && _Tile.Updateable )
                        _Tile.Update( ref TileInfoMap[_Tile.TileIndex] );
                }
            }
        }

        public void RenderChunk( )
        {
            Tile _Tile;
            for( int x = 0; x < Width; x++ )
            {
                for( int y = 0; y < Height; y++ )
                {
                    _Tile = Tile[x + y * Width];
                    if( _Tile != null && _Tile.Updateable )
                        _Tile.Render( ref TileInfoMap[_Tile.TileIndex] );
                }
            }
        }


    }
}