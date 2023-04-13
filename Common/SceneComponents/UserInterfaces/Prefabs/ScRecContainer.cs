using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Common.SceneComponents.UserInterfaces.Prefabs
{
    public class ScRecContainer : Container
    {
        public sealed override bool IsCanvas => false;

        public readonly RasterizerState RasterizerState;

        public override void PreRender( )
        {
            if( CanvasParent != null )
                EngineInfo.Engine.GraphicsDevice.ScissorRectangle = CanvasParent.Canvas.Bounds;
            else
                EngineInfo.Engine.GraphicsDevice.ScissorRectangle = LayoutInfo.RenderRectangle;

            EngineInfo.SpriteBatch.End( );
            EngineInfo.SpriteBatch.Begin( SpriteSortMode.Immediate , BlendState.AlphaBlend , SamplerState.PointClamp , null , RasterizerState , null , null );

            base.PreRender( );
        }
        public override void PostRender( )
        {
            if( CanvasParent != null )
                EngineInfo.Engine.GraphicsDevice.ScissorRectangle = CanvasParent.Canvas.Bounds;
            else
                EngineInfo.Engine.GraphicsDevice.ScissorRectangle = EngineInfo.ViewRectangle;
            EngineInfo.SpriteBatch.End( );
            (UserInterface as IRenderableSceneComponent).BatchBegin( );
            base.PostRender( );
        }

        public ScRecContainer()
        {
            RasterizerState = new RasterizerState( );
            RasterizerState.CullMode = CullMode.None;
            RasterizerState.ScissorTestEnable = true;
        }
    }
}