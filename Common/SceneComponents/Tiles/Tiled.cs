using Colin.Common.Graphics;
using Colin.Common.Physics.Dynamics;
using Colin.Common.Tiles.Events;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Colin.Common.Tiles
{
    /// <summary>
    /// 瓦片处理核心.
    /// </summary>
    public sealed class Tiled : ISceneComponent, IRenderableSceneComponent
    {
        public bool Enable { get; set; }

        public bool Visiable { get; set; }
        public SpriteSortMode SpriteSortMode => SpriteSortMode.Deferred;
        public Material Material => Material.DefaultMaterial;

        private Camera _camera;
        public Camera Camera => _camera;
        public Matrix? TransformMatrix => _camera.View;

        public RenderTarget2D RenderTarget { get; set; }
        public RenderTarget2D RenderTargetSwap { get; set; }

        private Scene _scene;
        public Scene Scene { get { return _scene; } set { _scene = value; } }

        /// <summary>
        /// 事件: 发生于物块被放置时.
        /// </summary>
        public EventHandler<TileEvent> OnTilePlace = ( s, e ) => { };

        /// <summary>
        /// 事件: 发生于物块被破坏时.
        /// </summary>
        public EventHandler<TileEvent> OnTileDestruction = ( s, e ) => { };

        /// <summary>
        /// 记录的信息.
        /// </summary>
        public TileInfoMap TileInfoMap;

        /// <summary>
        /// 物块集合.
        /// </summary>
        public Tile[ ] Tiles;

        private World World;

        /// <summary>
        /// 屏幕区块.
        /// </summary>
        public TileUpdater ScreenChunk = new TileUpdater( );

        /// <summary>
        /// 屏幕外更新区块.
        /// </summary>
        public List<TileUpdater> WeakChunk = new List<TileUpdater>( );

        /// <summary>
        /// 设置该场景所使用的摄像机.
        /// </summary>
        /// <param name="camera">摄像机.</param>
        public void SetCamera( Camera camera )
        {
            _camera = camera;
        }

        public void DoInitialize( )
        {
        }

        /// <summary>
        /// 初始化 Tiled.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void DoInitialize( int width, int height )
        {
            TileInfoMap = new TileInfoMap( width, height );
            Tiles = new Tile[width * height];
            ScreenChunk.Region = EngineInfo.ViewRectangle;
        }

        /// <summary>
        /// 从指定数据文件异步保存 Tiled .
        /// </summary>
        /// <param name="path">数据文件存储路径.</param>
        public async void SaveTiledAsync( string path ) => await Task.Run( ( ) => SaveTiled( path ) );

        /// <summary>
        /// 保存 Tiled 至数据文件.
        /// </summary>
        /// <param name="path">数据文件存储路径.</param>
        public void SaveTiled( string path )
        {
            FileStream _fileStream;
            using( _fileStream = File.Create( path ) )
            {
                XmlSerializer xmlSerializer = new XmlSerializer( typeof( TileInfoMap ) );
                xmlSerializer.Serialize( _fileStream, TileInfoMap );
            }
        }

        /// <summary>
        /// 从指定数据文件异步加载 Tiled .
        /// </summary>
        /// <param name="path">数据文件存储路径.</param>
        public async void LoadTiledAsync( string path ) => await Task.Run( ( ) => LoadTiled( path ) );

        /// <summary>
        /// 从指定数据文件加载 Tiled .
        /// </summary>
        /// <param name="path">文件路径.</param>
        public void LoadTiled( string path )
        {
            FileStream _fileStream;
            using( _fileStream = File.OpenRead( path ) )
            {
                XmlSerializer xmlSerializer = new XmlSerializer( typeof( TileInfoMap ) );
                TileInfoMap = (TileInfoMap)xmlSerializer.Deserialize( _fileStream );
            }
        }

        /// <summary>
        /// 获取指定坐标的物块.
        /// </summary>
        /// <param name="x">横坐标.</param>
        /// <param name="y">纵坐标.</param>
        /// <returns>指定坐标的物块.</returns>
        public Tile GetBehavior( int x, int y ) => Tiles[x + y * Width];

        public void Place<T>( int x, int y, object eventSender ) where T : Tile, new()
        {
            int id = x + y * Width;
            if( id <= Width * Height )
            {
                Tiles[id] = new T( );
                Tiles[id].TileIndex = id;
                TileEvent tileEvent;
                TileInfoMap.SetTileDefaultInfo( x, y );
                tileEvent = new TileEvent( TileInfoMap[id] );
                OnTilePlace.Invoke( eventSender, tileEvent );
                Tiles[id].Place( ref TileInfoMap[id] );
                //此处应编写物理相关代码.
            }
        }

        public void Place<T>( Point coordinate, object eventSender ) where T : Tile, new()
        {
            Place<T>( coordinate.X, coordinate.Y, eventSender );
        }

        public void Destruction( int x, int y, object sender )
        {
            int id = x + y * Width;
            if( id <= Width * Height )
            {
                TileEvent tileEvent;
                TileInfoMap.RemoveTileInfo( x, y );
                tileEvent = new TileEvent( TileInfoMap[id] );
                OnTileDestruction.Invoke( sender, tileEvent );
                if( Tiles[id] != null )
                {
                    Tiles[id].Destruction( ref TileInfoMap[id] );
                    //此处应编写物理相关代码.
                }
            }
        }

        public void Destruction( Point coordinate, object eventSender )
        {
            Destruction( coordinate.X, coordinate.Y, eventSender );
        }

        public void DoUpdate( GameTime time )
        {
            if( !Enable )
                return;

            ScreenChunk.DoUpdate( this );
            TileUpdater chunk;
            for( int count = 0; count < WeakChunk.Count; count++ )
            {
                chunk = WeakChunk[count];
                chunk.DoUpdate( this );
            }
        }

        public void DoRender( )
        {
            if( !Visiable )
                return;
            ScreenChunk.DoRender( this );
            TileUpdater chunk;
            for( int count = 0; count < WeakChunk.Count; count++ )
            {
                chunk = WeakChunk[count];
                chunk.DoRender( this );
            }
        }

        public Tiled( World world )
        {
            World = world;
        }

        public int Width => TileInfoMap.Width;

        public int Height => TileInfoMap.Height;

        public Point Size => new Point( TileInfoMap.Width, TileInfoMap.Height );

        public Vector2 SizeF => new Vector2( TileInfoMap.Width, TileInfoMap.Height );

    }
}
