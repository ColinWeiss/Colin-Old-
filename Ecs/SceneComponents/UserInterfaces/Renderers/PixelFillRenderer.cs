using Colin.Developments;
using Colin.Resources;
using System.Runtime.Serialization;

namespace Colin.Common.UserInterfaces.Renderers
{
    /// <summary>
    /// 以像素点填充 <see cref="LayoutInfo.RenderRectangle"/> 的渲染器.
    /// </summary>
    [Serializable]
    [DataContract( IsReference = true, Name = "PixelFillRenderer" )]
    public sealed class PixelFillRenderer : ContainerRenderer
    {
        public sealed override void RendererInit( ) { }

        public sealed override void RenderSelf( Container container )
        {
            EngineInfo.SpriteBatch.Draw(
                PreloadResource.Pixel.Source,
                            new Rectangle(
                    container.IsCanvas ?
                    Point.Zero : container.LayoutInfo.RenderLocation,
                    container.LayoutInfo.Size ),
                null,
                 container.IsCanvas ?
                 Color.White : container.DesignInfo.CurrentColor,
                0f,
                Vector2.Zero, SpriteEffects.None,
                PreloadResource.Pixel.Depth );
        }
    }
}