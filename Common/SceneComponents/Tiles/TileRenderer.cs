using Colin.Common.SceneComponents.Tiled;
using Colin.Extensions;
using Colin.Graphics;
using Colin.Resources;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.SceneComponents.Tiles
{
    public class TileRenderer : ISceneComponent , IRenderableSceneComponent
    {
        public RenderTarget2D cacheRt;

        public RenderTarget2D cacheRtSwap;

        private Camera _camera;
        public Camera Camera => _camera;
        public bool CameraMoved => _camera.position != _camera.positionLast;

        private Point _cameraPosConvTileCoordinateLast = Point.Zero;
        public Point CameraPosConvTileCoordinateLast => _cameraPosConvTileCoordinateLast;

        private Point _cameraPosConvTileCoordinate = Point.Zero;
        public Point CameraPosConvTileCoordinate => _cameraPosConvTileCoordinate;

        public bool NeedsRefreshRenderDirty => _cameraPosConvTileCoordinate != _cameraPosConvTileCoordinateLast;
        public Point RefreshRenderOffset
        {
            get
            {
                Point result = Point.Zero;
                if( NeedsRefreshRenderDirty )
                {
                    result.X = -(_cameraPosConvTileCoordinate.X - _cameraPosConvTileCoordinateLast.X) * TileOption.TileSize.X;
                    result.Y = -(_cameraPosConvTileCoordinate.Y - _cameraPosConvTileCoordinateLast.Y) * TileOption.TileSize.Y;
                    return result;
                }
                return result;
            }
        }
        public Vector2 RefreshRenderOffsetF => RefreshRenderOffset.ToVector2( );

        private bool _first = false;
        public bool First => _first;

        public Tile Tile { get; }

        public Scene Scene { get; set; }

        public bool Enable { get; set; }

        public RenderTarget2D RenderTarget { get; set; }

        public bool Visiable { get; set; }

        public SpriteSortMode SpriteSortMode { get; }

        public Material Material => Material.DefaultMaterial;

        public Matrix? TransformMatrix { get; }

        public void DoInitialize( )
        {
            cacheRt = RenderTargetExt.CreateDefault( );
            cacheRtSwap = RenderTargetExt.CreateDefault( );
            EngineInfo.Engine.Window.ClientSizeChanged += CacheRenderTargetInit;
        }
        private void CacheRenderTargetInit( object sender, EventArgs e )
        {
            cacheRt?.Dispose( );
            cacheRt = RenderTargetExt.CreateDefault( );
            cacheRtSwap?.Dispose( );
            cacheRtSwap = RenderTargetExt.CreateDefault( );
        }

        public void DoUpdate( GameTime time )
        {
            _cameraPosConvTileCoordinateLast = _cameraPosConvTileCoordinate;
            _cameraPosConvTileCoordinate = (_camera.position / TileOption.TileSizeF).ToPoint( );
        }

        /// <summary>
        /// 在此步骤, 渲染器将会根据摄像机位置批量绘制一个屏幕大小的物块至 <see cref="cacheRt"/> 上.
        /// </summary>
        public void FirstRender( )
        {
            Point _loopOrigin = _cameraPosConvTileCoordinate;
            _loopOrigin.X = Math.Clamp( _loopOrigin.X , 0 , Tile.Width );
            _loopOrigin.Y = Math.Clamp( _loopOrigin.Y, 0, Tile.Height );

            Point _loopTarget = _loopOrigin + _camera.Size / TileOption.TileSize;
            _loopTarget.X = Math.Clamp( _loopTarget.X, 0, Tile.Width );
            _loopTarget.Y = Math.Clamp( _loopTarget.Y, 0, Tile.Height );

            EngineInfo.SpriteBatch.End( );
            EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( cacheRt );
            EngineInfo.SpriteBatch.Begin( SpriteSortMode.Deferred , BlendState.AlphaBlend , SamplerState.PointClamp , transformMatrix: _camera.view );

            EngineInfo.Graphics.GraphicsDevice.Clear( Color.Gray );

            TileBehavior _tileBehavior;
            for( int x = _loopOrigin.X; x < _loopTarget.X; x++ )
            {
                for( int y = _loopOrigin.Y; y < _loopTarget.Y; y++ )
                {
                    _tileBehavior = Tile.behaviors[x, y];
                    if( !Tile.infos[x, y].Empty )
                        _tileBehavior.RenderTexture( x, y );
                }
            }
        }

        /// <summary>
        /// 在此步骤, 渲染器会将 <see cref="cacheRt"/> 绘制在 <see cref="cacheRtSwap"/> 上,
        /// <br>然后, 渲染器会根据摄像机转换物块坐标后的偏移量将 <see cref="cacheRtSwap"/> 再画回 <see cref="cacheRt"/>.</br>
        /// </summary>
        public void RefreshRender( )
        {
            EngineInfo.SpriteBatch.End( );
            EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( cacheRtSwap );
            EngineInfo.Graphics.GraphicsDevice.Clear( Color.Transparent );
            EngineInfo.SpriteBatch.Begin( SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp );
            EngineInfo.SpriteBatch.Draw( cacheRt , RefreshRenderOffsetF , Color.White );
            EngineInfo.SpriteBatch.End( );

            EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( cacheRt );
            EngineInfo.SpriteBatch.Begin( SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp );
            EngineInfo.SpriteBatch.Draw( cacheRtSwap, Vector2.Zero, Color.White );

            RefreshDirtyRecRender( );
        }

        /// <summary>
        /// 在此步骤, 渲染器将会获取偏移后的脏矩形, 并对这些脏矩形进行清理绘制.
        /// </summary>
        public void RefreshDirtyRecRender( )
        {
            int indexX = _cameraPosConvTileCoordinate.X + _camera.Size.X / TileOption.TileSize.X + RefreshRenderOffset.X / 16;
            int indexY = _cameraPosConvTileCoordinate.Y + _camera.Size.Y / TileOption.TileSize.Y - RefreshRenderOffset.Y / 16;

            EngineInfo.SpriteBatch.End( );
            EngineInfo.SpriteBatch.Begin( SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp );

            for( int x = indexX; x < indexX - RefreshRenderOffset.X / 16; x++ )
            {
                  EngineInfo.SpriteBatch.Draw( PreloadResource.Pixel.Source, new Rectangle( x * 16, 16, 16, 16 ), Color.White );
            }
        }

        public void DoRender( )
        {
            if( !_first )
            {
                FirstRender( );
                _first = true;
            }
            if( NeedsRefreshRenderDirty )
            {
                RefreshRender( );
            }

            Vector2 offset = Vector2.Zero;
            offset.X = _camera.position.X % TileOption.TileSizeF.X;
            offset.Y = _camera.position.Y % TileOption.TileSizeF.Y;


            EngineInfo.SpriteBatch.End( );
            EngineInfo.Graphics.GraphicsDevice.SetRenderTarget( Scene.SceneRenderTarget );
            EngineInfo.SpriteBatch.Begin( SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp );
            EngineInfo.SpriteBatch.Draw( cacheRt, Vector2.Zero, Color.White );

        }

        public double CeilingOrFloor( float value )
        {
            if( value < 0 )
                return Math.Floor( value );
            else if( value > 0 )
                return Math.Ceiling( value );
            else
                return value;
        }

        public TileRenderer( Tile tile, SceneCamera camera )
        {
            Tile = tile;
            _camera = camera;
        }
    }
}