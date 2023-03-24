using Colin.Common.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Colin.Common.Tiles
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

        /// <summary>
        /// 区块文件的路径.
        /// </summary>
        public string Path => string.Concat( GameDirPhonebook.ArchiveChunkDir, "\\", X, "_", Y, ".chunk" );

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

        /// <summary>
        /// 从指定数据文件异步保存区块.
        /// </summary>
        public async void SaveChunkAsync( ) => await Task.Run( ( ) => SaveChunk( ) );

        /// <summary>
        /// 保存区块至 <see cref="DirPhonebook.ArchiveChunkDir"/>.
        /// </summary>
        public void SaveChunk( )
        {
            FileStream _fileStream;
            using( _fileStream = File.Create( Path ) )
            {
                XmlSerializer xmlSerializer = new XmlSerializer( typeof( TileInfoMap ) );
                xmlSerializer.Serialize( _fileStream, TileInfoMap );
            }
        }

        /// <summary>
        /// 从指定数据文件异步加载区块.
        /// </summary>
        public async void LoadChunkAsync( ) => await Task.Run( ( ) => LoadChunk( ) );

        /// <summary>
        /// 从指定数据文件加载区块.
        /// </summary>
        public void LoadChunk( )
        {
            FileStream _fileStream;
            using( _fileStream = File.OpenRead( Path ) )
            {
                XmlSerializer xmlSerializer = new XmlSerializer( typeof( TileInfoMap ) );
                TileInfoMap = (TileInfoMap)xmlSerializer.Deserialize( _fileStream );
            }
        }

    }
}