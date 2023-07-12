using Colin.Common;
using Colin.Extensions;
using Colin.Graphics;
using Colin.Resources;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Modulars.Tiles
{
    public class TileRenderer : ISceneComponent, IRenderableSceneComponent
    {
        public RenderTarget2D cacheRt;

        public RenderTarget2D cacheRtSwap;

        private Camera _camera;
        public Camera Camera => _camera;
        public void BindCamera( Camera camera ) => _camera = camera;

        public Tile Tile { get; }

        public Scene Scene { get; set; }

        public bool Enable { get; set; }

        public bool Visible { get; set; }

        public RenderTarget2D SceneRt { get; set; }

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

        }

        public void DoRender( SpriteBatch batch )
        {
            Point frameOffset = Camera.PositionLast.ToPoint( ) - Camera.Position.ToPoint( );

        }
        public TileRenderer( Tile tile, SceneCamera camera )
        {
            Tile = tile;
            _camera = camera;
        }
    }
}