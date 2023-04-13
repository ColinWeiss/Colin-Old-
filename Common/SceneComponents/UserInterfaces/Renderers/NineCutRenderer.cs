using Colin.Extensions;
using Colin.Graphics;

namespace Colin.Common.SceneComponents.UserInterfaces.Renderers
{
    public class NineCutRenderer : ContainerRenderer
    {
        public Sprite Picture;

        public int BorderSize;

        public override void RendererInit( ) { }

        public override void Render( Container container )
        {
            if( container.LayoutInfo.Width == 0 || container.LayoutInfo.Height == 0 )
                return;
            EngineInfo.SpriteBatch.DrawNineCut(
                Picture.Source,
                container.DesignInfo.CurrentColor, new Rectangle(
                    container.IsCanvas ?
                    Point.Zero : container.LayoutInfo.RenderLocation,
                    container.LayoutInfo.Size
                    ),
                BorderSize,
                Picture.Depth );
        }
        public NineCutRenderer( Texture2D texture, int borderSize )
        {
            Picture = new Sprite( texture );
            BorderSize = borderSize;
        }
        public NineCutRenderer( Sprite picture, int borderSize )
        {
            Picture = picture;
            BorderSize = borderSize;
        }
    }
}
