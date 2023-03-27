using Colin.Developments;
using Colin.Extensions;

namespace Colin.Common.Tiles
{
    /// <summary>
    /// 物块渲染器.
    /// [!] 未使用.
    /// </summary>
    public class TileRenderer
    {
        public Tiled Tiled;

        public RenderTarget2D Tile;

        public RenderTarget2D Swap;

        /// <summary>
        /// 摄像机.
        /// </summary>
        public Camera Camera;

        private Vector2 _cameraLastPosition;

        private Vector2 _cameraPosition;

        /// <summary>
        /// 摄像机上一帧到这一帧的偏移量.
        /// </summary>
        public Vector2 Offset => _cameraPosition - _cameraLastPosition;

        public bool NeedRefresh { get; private set; } = true;

        public void Init( )
        {
            NeedRefresh = true;
            Tile = RenderTargetExtension.CreateDefault( EngineInfo.ViewWidth, EngineInfo.ViewHeight );
            Swap = RenderTargetExtension.CreateDefault( EngineInfo.ViewWidth, EngineInfo.ViewHeight );
            EngineInfo.Engine.Window.ClientSizeChanged += ( s, e ) =>
            {
                NeedRefresh = true;
                Tile = RenderTargetExtension.CreateDefault( EngineInfo.ViewWidth, EngineInfo.ViewHeight );
                Swap = RenderTargetExtension.CreateDefault( EngineInfo.ViewWidth, EngineInfo.ViewHeight );
            };
        }

        /// <summary>
        /// 首帧渲染, 此时需要将活跃区块尽数绘制至 <see cref="Tile"/> 上.
        /// <br>只读.</br>
        /// </summary>
        /// <param name="tileChunks">活跃区块列表.</param>
        public void RenderRefresh( List<TileChunk> tileChunks )
        {
            NeedRefresh = false;
            EngineInfo.SpriteBatch.End( );
            EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( Tile ); // 从这开始,
            (Tiled as IRenderableSceneMode).BatchBegin( );
            TileChunk _chunk;
            for( int count = 0; count < tileChunks.Count; count++ )
            {
                _chunk = tileChunks[count];
                _chunk.RenderChunk( );
            }
            EngineInfo.SpriteBatch.End( );  // 一直到这, 将物块尽数绘制至 Tile 上.
            EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( Tiled.RenderTarget ); // 再从这开始, 
            EngineInfo.SpriteBatch.Begin( SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp, Tiled.DepthStencilState, Tiled.RasterizerState, Tiled.Effect, Tiled.TransformMatrix );
            EngineInfo.SpriteBatch.Draw( Tile, _cameraPosition - EngineInfo.ViewCenter, Color.White );
            EngineInfo.SpriteBatch.End( );  // 一直到这, 把 Tile 画到 Tiled 的场景渲染上.
            (Tiled as IRenderableSceneMode).BatchBegin( ); //结束一下, 用默认设置继续画.

        }

        public void Render( List<TileChunk> tileChunks )
        {
            if( NeedRefresh )
                RenderRefresh( tileChunks );
            _cameraLastPosition = _cameraPosition;
            _cameraPosition = Camera.Position;

        }

        public void Render( TileChunk tileChunk, Vector2 position, Vector2 origin, float rotation, float scale )
        {

        }

    }
}